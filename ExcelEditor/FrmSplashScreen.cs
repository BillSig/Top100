using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelEditor
{
    public partial class FrmSplashScreen : Form
    {
        public FrmSplashScreen()
        {
            InitializeComponent();
        }

        private void tmrSplashScreen_Tick(object sender, EventArgs e)
        {
            tmrSplashScreen.Enabled = true;
            pgbSplashScreen.Increment(2);
            if (pgbSplashScreen.Value == 50)
            {
                tmrSplashScreen.Stop();
                this.Close();
            }
        }
    }
}
