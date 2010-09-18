using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Security;
using Lextm.SharpSnmpLib.Messaging;
using Mono.Options;

namespace vSNMP
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            
            cmbVersion.Items.Add("v1");
            cmbVersion.Items.Add("v2");
            cmbVersion.SelectedIndex = 1;
            
            txtIP.Text = "66.253.22.193";
            txtCommunity.Text  = "public";

            cmbOID.Items.Add("1.3.6.1.2.1.1.1.0");
            cmbOID.Items.Add("1.3.6.1.2.1.2.2.1.20.1");
        }


        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.Write(cmbVersion.SelectedItem);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            IPAddress ip;
            VersionCode version = VersionCode.V1;
            string community = "public";
            string user = string.Empty;
            string authentication = string.Empty;
            string authPhrase = string.Empty;
            string privacy = string.Empty;
            string privPhrase = string.Empty;
            int timeout = 1000;


            switch(cmbVersion.SelectedIndex+1)
            {
                case 1:
                    version = VersionCode.V1;
                    break;
                case 2:
                    version = VersionCode.V2;
                    break;
                case 3:
                    version = VersionCode.V3;
                    break;
                default:
                    MessageBox.Show("Invalid SNMP version selected.");
                    return;
            }

            community = txtCommunity.Text;
            
            bool parsed = IPAddress.TryParse(txtIP.Text , out ip);
            if (!parsed)
            {
                foreach (IPAddress address in Dns.GetHostAddresses(txtIP.Text))
                {
                    if (address.AddressFamily != AddressFamily.InterNetwork)
                    {
                        continue;
                    }

                    ip = address;
                    break;
                }
                
                if (ip == null)
                {
                    MessageBox.Show("Invalid IP: " + txtIP.Text);
                    return;
                }
            }

            try
            {
                List<Variable> vList = new List<Variable>();
                Variable test = new Variable(new ObjectIdentifier(cmbOID.Text));
                vList.Add(test);

                IPEndPoint receiver = new IPEndPoint(ip, 161);

                foreach (Variable variable in
                         Messenger.Get(version, receiver, new OctetString(community), vList, timeout))
                    {
                        Console.WriteLine(variable);
                        rtxOutput.Text = Convert.ToString(variable.Data);
                    }
                return;
            }

            catch (SnmpException ex)
            {
                MessageBox.Show("SNMP Exception: " + ex);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Socket Exception: " + ex);
            }
        }
    }
}
