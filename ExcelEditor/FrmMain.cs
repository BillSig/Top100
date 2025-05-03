using NPOI.SS.UserModel;
using System.Data;

namespace ExcelEditor
{
    public partial class FrmMain : Form
    {
        private string excelPath;
        private DataTable table;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog()
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Select an Excel file"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            excelPath = dlg.FileName;
            txtFileName.Text = excelPath;
            LoadExcelToGrid(excelPath);
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
            for (int c = headerRow.FirstCellNum; c < headerRow.LastCellNum; c++)
            {
                dt.Columns.Add(headerRow.GetCell(c).ToString());
            }

            // Data rows
            for (int r = sheet.FirstRowNum + 1; r <= sheet.LastRowNum; r++)
            {
                var row = sheet.GetRow(r);
                if (row == null)
                {
                    continue; // skip empty
                }

                var dr = dt.NewRow();
                for (int c = headerRow.FirstCellNum; c < headerRow.LastCellNum; c++)
                {
                    dr[c] = row.GetCell(c)?.ToString() ?? string.Empty;
                }

                dt.Rows.Add(dr);
            }

            table = dt;
            grdMain.DataSource = table;
        }

        private void SaveGridToExcel(string path)
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
        }
    }
}
