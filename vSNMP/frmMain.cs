using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vSNMP
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            
            cmbVersion.Items.Add("v1");
            cmbVersion.Items.Add("v2");
            cmbVersion.Items.Add("v3");
            cmbVersion.SelectedIndex = 1;
            
            txtIP.Text = "66.253.22.193";
            txtCommunity.Text  = "public";

            cmbOID.Items.Add("1.3.6.1.2.1.1.1.0");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.Write(cmbVersion.SelectedItem);
        }
    }
}
