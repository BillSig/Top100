using ExcelEditorLibrary.Models;
using NPOI.SS.UserModel;
using Serilog;
using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Web;

namespace ExcelEditor
{
    public partial class FrmMain : Form
    {
        private string excelPath;
        private DataTable table;
        private List<GreatestHitModel> greatestHits = new();
        private bool hasUnsavedChanges = false;
        private AppConfig appConfig;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Read version from Assembly (setup in project file)
            ShowVersion();

            // Read appsettings from file
            ReadConfiguration();

            if (appConfig != null)
            {
                UpdateButtons(false, true);
                UpdateArrows(false);
            }
            else
            {
                Close(); // Close app if config cannot be loaded
            }
        }

        private void ShowVersion()
        {
            this.Text = $"Excel Editor - {GetDefaultApplicationVersion()}";
            //this.Text = $"Excel Editor - {GetApplicationVersionFromGithub()}";
        }

        private string GetDefaultApplicationVersion()
        {
            // Add the following line in project file in version's PropertyGroup:
            // <IncludeSourceRevisionInInformationalVersion>false</IncludeSourceRevisionInInformationalVersion>

            // For release versions, edit the version in project file in version's PropertyGroup:
            // <InformationalVersion>$(Version)</InformationalVersion>

            // For development versions, edit the version in project file in version's PropertyGroup:
            // <InformationalVersion>$(Version)-dev-$([System.DateTime]::Now.ToString("yyyyMMdd.HHmmss"))</InformationalVersion>
            return Application.ProductVersion;
        }

        private string GetApplicationVersionFromGithub()
        {
            // Include last GitHub commit SHA in version:

            // Add the following line in project file in version's PropertyGroup:
            // <IncludeSourceRevisionInInformationalVersion>true</IncludeSourceRevisionInInformationalVersion>

            string versionStr = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "unknown";
            return versionStr;
        }

        private void ReadConfiguration()
        {
            try
            {
                if (!LoadConfig("appsettings.json", out appConfig))
                {
                    string msg = "Configuration file not found. Using default settings.";
                    Log.Warning(msg);
                    MessageBox.Show(msg,
                        "Configuration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                string msg = $"Error loading configuration: {ex.Message}";
                Log.Error(msg);
                MessageBox.Show(msg,
                    "Configuration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool LoadConfig(string path, out AppConfig appConfig)
        {
            if (!File.Exists(path))
            {
                appConfig = new AppConfig();
                return false; // return default config if file is missing
            }

            var json = File.ReadAllText(path);
            appConfig = JsonSerializer.Deserialize<AppConfig>(json);

            return true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Save file first!",
                    "Unsaved Changes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            using var dlg = new OpenFileDialog()
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Select an Excel file"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            excelPath = dlg.FileName;
            txtFileName.Text = excelPath;
            if (LoadExcelToGrid(excelPath))
            {
                LoadGreatestHits();
                UpdateButtons(hasUnsavedChanges, false);
            }
        }

        private void LoadGreatestHits()
        {
            greatestHits.Clear();

            foreach (DataRow dr in table.Rows)
            {
                try
                {
                    greatestHits.Add(new GreatestHitModel
                    {
                        Position = dr[0] == DBNull.Value || string.IsNullOrWhiteSpace(dr[0].ToString()) ? 0 : Convert.ToInt32(dr[0]),
                        BandName = dr[1].ToString(),
                        SongTitle = dr[2].ToString(),
                        VideoLink = dr[3].ToString(),
                        IsViewed = dr[4] != DBNull.Value && Convert.ToInt32(dr[4]) == 1
                    });
                }
                catch (Exception ex)
                {
                    string msg = $"There was an error while parsing excel file in line {dr[0].ToString()}. Fix the file and reopen! Error: {ex.Message}";
                    Log.Error(msg, ex);
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private bool LoadExcelToGrid(string excelPath)
        {
            IWorkbook wb;
            using (var fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // WorkbookFactory auto-detects .xls vs .xlsx
                wb = WorkbookFactory.Create(fs);
            }

            var sheet = wb.GetSheetAt(0);
            if (!ValidateSheet(sheet, out var errors))
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var dt = new DataTable(sheet.SheetName);

            // Header row
            var headerRow = sheet.GetRow(sheet.FirstRowNum);
            for (int c = headerRow.FirstCellNum; c < headerRow.LastCellNum && !headerRow.GetCell(c).StringCellValue.Contains("Column"); c++)
            {
                // Set the first column ("Position") as int, others as string
                if (c == 0)
                    dt.Columns.Add(headerRow.GetCell(c).ToString(), typeof(int));
                else
                    dt.Columns.Add(headerRow.GetCell(c).ToString(), typeof(string));
            }

            // Data rows
            for (int r = sheet.FirstRowNum + 1; r <= sheet.LastRowNum; r++)
            {
                var row = sheet.GetRow(r);
                if (row == null)
                {
                    continue; // skip empty row object
                }

                bool isEmpty = true;
                var dr = dt.NewRow();
                for (int c = headerRow.FirstCellNum; c < headerRow.LastCellNum; c++)
                {
                    var cell = row.GetCell(c);
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        isEmpty = false;
                    }

                    if (cell != null)
                    {
                        if (c == 0)
                        {
                            // Parse as int for the "Position" column
                            if (int.TryParse(cell.ToString(), out int pos))
                                dr[c] = pos;
                            else
                                dr[c] = DBNull.Value;
                        }
                        else
                        {
                            dr[c] = cell.ToString();
                        }
                    }
                }

                if (!isEmpty)
                {
                    dt.Rows.Add(dr);
                }
            }

            table = dt;
            grdMain.DataSource = table;
            return true;
        }

        private bool ValidateSheet(ISheet sheet, out string[] errors)
        {
            var errorList = new List<string>();

            if (sheet == null)
            {
                string msg = "The selected Excel file does not contain any sheets.";
                Log.Warning("Operation: {function} {msg}", nameof(ValidateSheet), msg);
                errorList.Add(msg);
            }
            else
            {
                var headerRow = sheet.GetRow(sheet.FirstRowNum);

                if (headerRow == null)
                {
                    string msg = "The selected Excel file does not contain a header row. Please select a valid file.";
                    Log.Warning("Operation: {function} {msg}", nameof(ValidateSheet), msg);
                    errorList.Add(msg);
                }
                else
                {
                    if (headerRow.LastCellNum < appConfig.ColumnsCounter)
                    {
                        string msg = $"The header row must contain at least {appConfig.ColumnsCounter} columns.";
                        Log.Warning("Operation: {function} {msg}", nameof(ValidateSheet), msg);
                        errorList.Add(msg);
                    }
                }

                if (sheet.Count() > appConfig.MaxGreatestHitsCounter)
                {
                    string msg = $"The selected Excel file contains more than {appConfig.MaxGreatestHitsCounter} rows.";
                    Log.Warning("Operation: {function} {msg}", nameof(ValidateSheet), msg);
                    errorList.Add(msg);
                }
            }

            errors = errorList.ToArray();
            return errors.Length == 0;
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditRow(sender, e);
        }

        private void EditRow(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // header

            ///// Get selected row directly from in-memory excel file
            //GreatestHitModel currentGreatestHit = GetSelectedGreatestHit(e.RowIndex);

            //// Get selected row from the list (no filter)
            ////GreatestHitModel currentGreatestHit = greatestHits[e.RowIndex];
            
            // Get the actual DataRow from either DataTable or DataView
            DataRow dataRow = GetDataRowFromGrid(e.RowIndex);
            if (dataRow == null) return;

            // Get position to find the correct item in greatestHits list
            int position = dataRow[0] == DBNull.Value || string.IsNullOrWhiteSpace(dataRow[0].ToString())
                ? 0
                : Convert.ToInt32(dataRow[0]);

            if (position <= 0 || position > greatestHits.Count)
            {
                MessageBox.Show("Invalid position value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GreatestHitModel currentGreatestHit = greatestHits[position - 1];

            // show edit form
            using var editForm = new FrmEditRow(currentGreatestHit);
            var result = editForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Update the underlying DataTable row (not the filtered view index)
                int tableRowIndex = table.Rows.IndexOf(dataRow);
                table.Rows[tableRowIndex][1] = currentGreatestHit.BandName;
                table.Rows[tableRowIndex][2] = currentGreatestHit.SongTitle;
                table.Rows[tableRowIndex][3] = currentGreatestHit.VideoLink;
                table.Rows[tableRowIndex][4] = currentGreatestHit.IsViewed ? 1 : 0;

                hasUnsavedChanges = true;

                // Save Excel file depending on global parameter
                if (appConfig.SaveToExcelInstantly)
                {
                    if (SaveRow(tableRowIndex, out var error))
                    {
                        hasUnsavedChanges = false;
                    }
                }

                UpdateUIRow(e.RowIndex, hasUnsavedChanges);
            }
        }

        private DataRow GetDataRowFromGrid(int gridRowIndex)
        {
            if (grdMain.DataSource is DataView dataView)
            {
                // When filtered, DataSource is a DataView
                return dataView[gridRowIndex].Row;
            }
            else if (grdMain.DataSource is DataTable dataTable)
            {
                // When not filtered, DataSource is a DataTable
                return dataTable.Rows[gridRowIndex];
            }

            return null;
        }

        private void UpdateUIRow(int rowIndex, bool recordhasChanged)
        {
            hasUnsavedChanges = recordhasChanged;
            UpdateButtons(recordhasChanged, false);
            if (recordhasChanged)
            {
                grdMain.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else
            {
                grdMain.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private GreatestHitModel GetSelectedGreatestHit(int rowIndex)
        {
            // grab current values
            var selectedRow = table.Rows[rowIndex]
                                   .ItemArray
                                   .Select(o => o?.ToString() ?? "")
                                   .ToList();
            GreatestHitModel currentGreatestHit = new GreatestHitModel()
            {
                Position = int.Parse(selectedRow[0]),
                BandName = selectedRow[1],
                SongTitle = selectedRow[2],
                VideoLink = selectedRow[3],
                IsViewed = int.Parse(selectedRow[4]) == 1
            };

            return currentGreatestHit;
        }

        private bool SaveRow(int rowIndex, out string error)
        {
            bool result = false;
            error = string.Empty;

            try
            {
                IWorkbook wb;
                using (var fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    wb = WorkbookFactory.Create(fs);

                var sheet = wb.GetSheetAt(0);
                int hdrRow = sheet.FirstRowNum;
                int sheetRow = hdrRow + 1 + rowIndex;

                // Get row from sheet or create a new one
                var row = sheet.GetRow(sheetRow) ?? sheet.CreateRow(sheetRow);

                // Update cells of the row.
                for (int c = 0; c < table.Columns.Count; c++)
                {
                    // Set the cell of the row with the value of the in-memory cell value.
                    // If the cell doesn't exist, nothing happens.
                    row.GetCell(c)?.SetCellValue(table.Rows[rowIndex][c]?.ToString() ?? "");

                    //// If you want to include creation of cells that didn't exist, replace above line of code with the following:
                    //var cell = row.GetCell(c) ?? row.CreateCell(c);
                    //cell.SetCellValue(table.Rows[rowIndex][c]?.ToString() ?? "");
                }

                // overwrite file
                using var outFs = new FileStream(excelPath, FileMode.Create, FileAccess.Write);
                wb.Write(outFs);

                result = true;
            }
            catch (Exception ex)
            {
                string msg = $"File was not saved: {ex.Message}";
                Log.Error(msg, ex);
                error = msg;
            }

            return result;
        }

        private bool SaveGridToExcel(string path)
        {
            bool result = false;

            try
            {
                IWorkbook wb;
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    wb = WorkbookFactory.Create(fs);

                var sheet = wb.GetSheetAt(0);
                if (!ValidateSheet(sheet, out var errors))
                {
                    MessageBox.Show(string.Join(Environment.NewLine, errors), "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int rowCount = table.Rows.Count;

                // Clear out existing data rows (but keep header)
                for (int r = sheet.LastRowNum; r > sheet.FirstRowNum; r--)
                    sheet.RemoveRow(sheet.GetRow(r));

                // Write updated rows
                for (int i = 0; i < rowCount; i++)
                {
                    var dr = table.Rows[i];
                    var row = sheet.CreateRow(i + sheet.FirstRowNum + 1);
                    for (int c = 0; c < table.Columns.Count; c++)
                        row.CreateCell(c).SetCellValue(dr[c]?.ToString() ?? "");
                }

                // Overwrite file
                using var outFs = new FileStream(path, FileMode.Create, FileAccess.Write);
                wb.Write(outFs);
                result = true;
            }
            catch (Exception ex)
            {
                string msg = $"Error while saving excel file: {ex.Message}";
                Log.Error(msg, ex);
                MessageBox.Show(msg);
            }

            return result;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveGridToExcel(excelPath))
            {
                MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK);

                UpdateButtons(false, false);
                foreach (DataGridViewRow row in grdMain.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Do you want to save before exiting?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (!SaveGridToExcel(excelPath))
                    {
                        // If save failed, cancel closing
                        e.Cancel = true;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

                // If No, just close without saving
            }
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                    "Are you sure that you want to discard unsaved changes? This action cannot be undone.",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                LoadExcelToGrid(excelPath);
                UpdateButtons(false, false);
            }
        }

        private void UpdateButtons(bool hasChanges, bool isFirstRun)
        {
            hasUnsavedChanges = hasChanges;
            btnSave.Enabled = hasChanges;
            btnDiscard.Enabled = hasChanges;

            txtBand.Enabled = !isFirstRun;
            txtSong.Enabled = !isFirstRun;
            chkIsViewed.Enabled = !isFirstRun;
            btnFilter.Enabled = !isFirstRun;
            btnClear.Enabled = !isFirstRun;
        }

        private void UpdateArrows(bool isRowSelected)
        {
            btnMoveDown.Enabled = isRowSelected;
            btnMoveUp.Enabled = isRowSelected;
        }

        private void grdMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateArrows(e.RowIndex >= 0);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (IsFiltered())
            {
                MessageBox.Show("Cannot move rows while filter is active. Please clear filters first.",
                    "Filter Active", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (grdMain.CurrentCell == null) return;
            int rowIndex = grdMain.CurrentCell.RowIndex;

            // Only allow moving if not header and not the first data row
            if (rowIndex < 1) return;

            SwapRowData(rowIndex, rowIndex - 1);

            // Swap in greatestHits except Position
            SwapGreatestHitData(rowIndex, rowIndex - 1);

            UpdateUIRow(rowIndex, true);
            UpdateUIRow(rowIndex - 1, true);

            grdMain.ClearSelection();
            grdMain.Rows[rowIndex - 1].Selected = true;
            grdMain.CurrentCell = grdMain.Rows[rowIndex - 1].Cells[0];

            UpdateButtons(true, false);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (IsFiltered())
            {
                MessageBox.Show("Cannot move rows while filter is active. Please clear filters first.",
                    "Filter Active", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (grdMain.CurrentRow == null) return;
            int rowIndex = grdMain.CurrentRow.Index;

            // Only allow moving if not header and not the last row
            if (rowIndex < 0 || rowIndex >= table.Rows.Count - 1) return;

            SwapRowData(rowIndex, rowIndex + 1);

            // Swap in greatestHits except Position
            SwapGreatestHitData(rowIndex, rowIndex + 1);

            UpdateUIRow(rowIndex, true);
            UpdateUIRow(rowIndex + 1, true);

            grdMain.ClearSelection();
            grdMain.Rows[rowIndex + 1].Selected = true;
            grdMain.CurrentCell = grdMain.Rows[rowIndex + 1].Cells[0];

            UpdateButtons(true, false);
        }

        // Swap all columns except the first (Position)
        private void SwapRowData(int rowA, int rowB)
        {
            for (int col = 1; col <= 4; col++)
            {
                var temp = table.Rows[rowA][col];
                table.Rows[rowA][col] = table.Rows[rowB][col];
                table.Rows[rowB][col] = temp;
            }
        }

        // Swap GreatestHitModel data except Position
        private void SwapGreatestHitData(int indexA, int indexB)
        {
            var tempBand = greatestHits[indexA].BandName;
            var tempSong = greatestHits[indexA].SongTitle;
            var tempVideo = greatestHits[indexA].VideoLink;
            var tempViewed = greatestHits[indexA].IsViewed;

            greatestHits[indexA].BandName = greatestHits[indexB].BandName;
            greatestHits[indexA].SongTitle = greatestHits[indexB].SongTitle;
            greatestHits[indexA].VideoLink = greatestHits[indexB].VideoLink;
            greatestHits[indexA].IsViewed = greatestHits[indexB].IsViewed;

            greatestHits[indexB].BandName = tempBand;
            greatestHits[indexB].SongTitle = tempSong;
            greatestHits[indexB].VideoLink = tempVideo;
            greatestHits[indexB].IsViewed = tempViewed;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (!hasUnsavedChanges)
            {
                FilterTable(txtBand.Text, txtSong.Text, chkIsViewed.CheckState);
            }
            else
            {
                MessageBox.Show(
                    "You have unsaved changes. Save file first!",
                    "Unsaved Changes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!hasUnsavedChanges)
            {
                ClearFilters();
            }
            else
            {
                MessageBox.Show(
                    "You have unsaved changes. Save file first!",
                    "Unsaved Changes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearFilters()
        {
            txtBand.Text = string.Empty;
            txtSong.Text = string.Empty;
            chkIsViewed.CheckState = CheckState.Indeterminate;

            FilterTable(txtBand.Text, txtSong.Text, chkIsViewed.CheckState);
        }

        private void FilterTable(string band, string song, CheckState isViewedState)
        {
            var filters = new List<string>();

            if (!string.IsNullOrWhiteSpace(band))
                filters.Add($"Band LIKE '%{band.Replace("'", "''")}%'");

            if (!string.IsNullOrWhiteSpace(song))
                filters.Add($"Song LIKE '%{song.Replace("'", "''")}%'");

            if (isViewedState == CheckState.Checked)
                filters.Add("[Viewed Data] = 1");
            else if (isViewedState == CheckState.Unchecked)
                filters.Add("[Viewed Data] = 0");

            string filterString = string.Join(" AND ", filters);

            DataView view = table.DefaultView;
            view.RowFilter = filterString;

            grdMain.DataSource = view;
        }

        private bool IsFiltered()
        {
            return grdMain.DataSource is DataView dataView && !string.IsNullOrEmpty(dataView.RowFilter);
        }
    }
}
