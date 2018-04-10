using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;

namespace QLKhachHang
{
    public partial class frmConnect : DevExpress.XtraEditors.XtraUserControl
    {
        Connect cn = new Connect();
        public frmConnect()
        {
            InitializeComponent();
        }

        private void frmConnect_Load(object sender, EventArgs e)
        {
            txtServer.Text = System.Environment.MachineName;
            txtPort.Text = "3306";
            txtDb.Text = "dev_tv";
            txtUser.Text = "user";
            txtPassword.Text = "123456";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cn.server = txtServer.Text;
                cn.port = txtPort.Text;
                cn.database = txtDb.Text;
                cn.user_id = txtUser.Text;
                cn.password = txtPassword.Text;
                cn.connn();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //var host = Dns.GetHostEntry(Dns.GetHostName());
            //foreach (var ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        string i2 =  ip.ToString();
            //    }
            //}
            try
            {
                cn.server = txtServer.Text;
                cn.port = txtPort.Text;
                cn.database = txtDb.Text;
                cn.user_id = txtUser.Text;
                cn.password = txtPassword.Text;
                cn.connn();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}