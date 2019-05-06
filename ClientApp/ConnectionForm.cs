using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class ConnectionForm : Form
    {
        private const string CONFIG_FILE = "connection.config";
        public ConnectionForm()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;
        }

        private async void btTest_Click(object sender, EventArgs e)
        {
            await Test();
        }

        private async Task Test()
        {
            btTest.Enabled = false;
            btTest.Text = "Testing";
            tbServer.Enabled = false;
            try
            {
                var res = await ApiBoundary.Ping(tbServer.Text);
                textBox1.Text = res;
                
                btTest.Text = "OK";
                btConnect.Enabled = true;
            }
            catch (Exception exception)
            {
                textBox1.Text = exception.ToString();
                btTest.Text = "Test";
                btTest.Enabled = true;
                tbServer.Enabled = true;
            }
        }

        private void btConnect_Click(object sender, EventArgs e)
        {

            try
            {
                File.WriteAllText(CONFIG_FILE, tbServer.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to write settings");
            }

            ApiBoundary.SERVER_PATH = tbServer.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void ConnectionForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(CONFIG_FILE))
            {
                tbServer.Text = File.ReadAllText(CONFIG_FILE).Trim('\r', '\n');
                await Test();
                if(btConnect.Enabled)
                    btConnect_Click(null,null);
            }
            else
            {
                tbServer.Text = ApiBoundary.SERVER_PATH;
            }
        }
    }
}
