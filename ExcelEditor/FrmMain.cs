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
            LoadExcelToGrid(excelPath);
        }

        private void LoadExcelToGrid(string excelPath)
        {
            throw new NotImplementedException();
        }
    }
}
