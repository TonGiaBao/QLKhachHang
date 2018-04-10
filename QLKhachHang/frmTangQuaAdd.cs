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
using System.Threading;

namespace QLKhachHang
{
    public partial class frmTangQuaAdd : DevExpress.XtraEditors.XtraForm
    {
        Connect cn = new Connect();
        private readonly frmTangQua f1;
        public frmTangQuaAdd(frmTangQua f)
        {
            InitializeComponent();
            f1 = f;
        }
        private void StartForm()
        {
            Application.Run(new frmWait());
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
             //   Thread t = new Thread(new ThreadStart(StartForm));
             //   t.Start();
                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                string sql = "insert into tangqua(QuyCach,GhiChu) " +
                                         " values(@quycach,@ghichu) ";
                cmd.CommandText = sql;
                cmd.Connection = cn.conn;
                cmd.Parameters.Add("@quycach", MySqlDbType.VarChar).Value = txtQuycach.Text;           
                cmd.Parameters.Add("@ghichu", MySqlDbType.VarChar).Value = txtGhichu.Text;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    f1.loadgv();
                //    t.Abort();
                    XtraMessageBox.Show("Đã lưu");
                }
                else
                {
                    XtraMessageBox.Show("Lưu thất bại");
                }
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                cn.closeconnection();
                cn.conn.Dispose();
            }
        }

        private void frmTangQuaAdd_Load(object sender, EventArgs e)
        {

        }
    }
}