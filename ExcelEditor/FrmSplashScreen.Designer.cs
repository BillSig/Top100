namespace ExcelEditor
{
    partial class FrmSplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pgbSplashScreen = new ProgressBar();
            tmrSplashScreen = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // pgbSplashScreen
            // 
            pgbSplashScreen.ForeColor = SystemColors.GradientActiveCaption;
            pgbSplashScreen.Location = new Point(558, 830);
            pgbSplashScreen.Name = "pgbSplashScreen";
            pgbSplashScreen.Size = new Size(418, 56);
            pgbSplashScreen.Style = ProgressBarStyle.Continuous;
            pgbSplashScreen.TabIndex = 0;
            // 
            // tmrSplashScreen
            // 
            tmrSplashScreen.Enabled = true;
            tmrSplashScreen.Tick += tmrSplashScreen_Tick;
            // 
            // FrmSplashScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = Properties.Resources.BillSig_Logo_Loading_1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(988, 942);
            ControlBox = false;
            Controls.Add(pgbSplashScreen);
            Cursor = Cursors.AppStarting;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSplashScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SplashScreen";
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar pgbSplashScreen;
        private System.Windows.Forms.Timer tmrSplashScreen;
    }
}