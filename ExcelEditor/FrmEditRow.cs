using ExcelEditorLibrary.Models;
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
    public partial class FrmEditRow : Form
    {
        GreatestHitModel _CurrentGreatestHit;
        bool _UpdatePosition;
        public FrmEditRow(GreatestHitModel currentGreatestHit, bool updatePosition, int maxPosition)
        {
            InitializeComponent();

            _CurrentGreatestHit = currentGreatestHit;
            _UpdatePosition = updatePosition;

            nudPosition.Minimum = 1;
            nudPosition.Maximum = maxPosition;

            if (!_UpdatePosition)
            {
                nudPosition.Enabled = false;
            }

            if (currentGreatestHit != null) 
            {
                ShowCurrentGreatestHit();
            }
        }

        private void ShowCurrentGreatestHit()
        {
            txtPosition.Text = _CurrentGreatestHit.Position.ToString();
            nudPosition.Value = _CurrentGreatestHit.Position;
            txtBandName.Text = _CurrentGreatestHit.BandName;
            txtSongTitle.Text = _CurrentGreatestHit.SongTitle;
            txtVideoLink.Text = _CurrentGreatestHit.VideoLink;
            chkIsViewed.Checked = _CurrentGreatestHit.IsViewed;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            UpdateCurrentGreatestHit();
            Close();
        }

        private void UpdateCurrentGreatestHit()
        {
            if (_UpdatePosition)
            {
                _CurrentGreatestHit.Position = (int)nudPosition.Value;
            }

            _CurrentGreatestHit.BandName = txtBandName.Text;
            _CurrentGreatestHit.SongTitle = txtSongTitle.Text;
            _CurrentGreatestHit.VideoLink = txtVideoLink.Text;
            _CurrentGreatestHit.IsViewed = chkIsViewed.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel; 
            Close();
        }
    }
}
