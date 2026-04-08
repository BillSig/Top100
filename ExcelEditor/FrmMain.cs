using ExcelEditorLibrary.Models;
using NPOI.SS.UserModel;
using System.Data;
using System.Reflection;

namespace ExcelEditor
{
    public partial class FrmMain : Form
    {
        private string excelPath;
        private DataTable table;
        private List<GreatestHitModel> greatestHits = new();
        private bool hasUnsavedChanges = false;

        public FrmMain()
        {
            InitializeComponent();
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
            LoadExcelToGrid(excelPath);
            LoadGreatestHits();
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
                    MessageBox.Show($"There was an error while parsing excel file in line {dr[0].ToString()}. Fix the file and reopen! Error: {ex.Message}", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void LoadExcelToGrid(string excelPath)
        {
            IWorkbook wb;
            using (var fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // WorkbookFactory auto-detects .xls vs .xlsx
                wb = WorkbookFactory.Create(fs);
            }

            var sheet = wb.GetSheetAt(0);
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

            // Get selected row from the list
            GreatestHitModel currentGreatestHit = greatestHits[e.RowIndex];

            // show edit form
            using var editForm = new FrmEditRow(currentGreatestHit);
            var result = editForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Update DataTable from currentGreatestHit. It is passed by reference so it is already updated!
                table.Rows[e.RowIndex][1] = currentGreatestHit.BandName;
                table.Rows[e.RowIndex][2] = currentGreatestHit.SongTitle;
                table.Rows[e.RowIndex][3] = currentGreatestHit.VideoLink;
                table.Rows[e.RowIndex][4] = currentGreatestHit.IsViewed ? 1 : 0;

                UpdateUIRow(e.RowIndex, true);

                //var values = editForm.EditedValues;
                //foreach (DataColumn col in table.Columns)
                //{
                //    table.Rows[e.RowIndex][col.ColumnName] = values[col.ColumnName];
                //}


                // 2) write that row back to Excel
                //SaveRow(e.RowIndex);
            }
        }

        private void UpdateUIRow(int rowIndex, bool recordhasChanged)
        {
            hasUnsavedChanges = recordhasChanged;
            btnSave.Enabled = recordhasChanged;
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

        private void SaveRow(int rowIndex)
        {
            IWorkbook wb;
            using (var fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                wb = WorkbookFactory.Create(fs);

            var sheet = wb.GetSheetAt(0);
            int hdrRow = sheet.FirstRowNum;
            int sheetRow = hdrRow + 1 + rowIndex;
            var row = sheet.GetRow(sheetRow) ?? sheet.CreateRow(sheetRow);

            for (int c = 0; c < table.Columns.Count; c++)
            {
                //row.GetCell(c)?.SetCellValue(table.Rows[rowIndex][c]?.ToString() ?? "");

                //row.GetCell(c)?.SetCellValue(table.Rows[rowIndex][c]?.ToString() ?? "")
                //?? row.CreateCell(c).SetCellValue(table.Rows[rowIndex][c]?.ToString() ?? "");
            }

            // overwrite file
            using var outFs = new FileStream(excelPath, FileMode.Create, FileAccess.Write);
            wb.Write(outFs);
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
                MessageBox.Show($"Error while sving excel file: {ex.Message}");
            }

            return result;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Read version from Assembly (setup in project file)
            this.Text = $"Excel Editor - {Application.ProductVersion}";

            btnSave.Enabled = false;

            // Altrnatively, include last GitHub commit SHA in version:
            // Remove the following line in project file:
            // <IncludeSourceRevisionInInformationalVersion>false</IncludeSourceRevisionInInformationalVersion>

            //string versionStr = Assembly.GetExecutingAssembly()
            //    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "unknown";
            //this.Text = $"Excel Editor - {versionStr}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveGridToExcel(excelPath))
            {
                MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK);

                hasUnsavedChanges = false;
                btnSave.Enabled = false;
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
    }
}
