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
using DevExpress.XtraSplashScreen;
using System.Threading;

namespace QLKhachHang
{
    public partial class frmDsKhNhanQua : DevExpress.XtraEditors.XtraUserControl
    {
        bool a = false;
        Connect cn = new Connect();
        MySqlDataAdapter adpter = null;
        DataSet ds = new DataSet();
        public frmDsKhNhanQua()
        {
            InitializeComponent();
        }

        private void frmDsKhNhanQua_Load(object sender, EventArgs e)
        {
            try
            {
                adpter = new MySqlDataAdapter("SELECT * FROM tangqua;", cn.conn);
                ds = new DataSet();
                adpter.Fill(ds);

                lookUpEdit1.Properties.DataSource = ds.Tables[0];
                lookUpEdit1.Properties.DisplayMember = "QuyCach";
                lookUpEdit1.Properties.ValueMember = "TangQuaID";
            }
            catch { }
        }
        private void StartForm()
        {
            Application.Run(new frmWait());
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (lookUpEdit1.Text == "Mời chọn")
            {
                XtraMessageBox.Show("Bạn chưa chọn đợt tặng quà");
            }
            else
            {
                 
               /* gvKhnhanqua.DataSource = null;
                adpter = new MySqlDataAdapter("select ql.khachhang.KHID,ql.khachhang.CMND,ql.khachhang.TenKH,ql.tinhtrangtang.tangqua_TangQuaID from ql.khachhang left outer join ql.tinhtrangtang on ql.khachhang.KHID = ql.tinhtrangtang.khachhang_KHID where ql.tinhtrangtang.tangqua_TangQuaID = '" + lookUpEdit1.EditValue.ToString() + "' and ql.khachhang.KHID is not null;", cn.conn);
                ds = new DataSet();
                adpter.Fill(ds);
                gvKhnhanqua.DataSource = ds.Tables[0];*/
                try
                {
                    SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
                    //     Thread t = new Thread(new ThreadStart(StartForm));
                    //    t.Start();
                    a = true;
                    cn.openconnection();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select khachhang.KHID,khachhang.CMND,khachhang.TenKH,khachhang.TenThuongGoi,tinhtrangtang.tangqua_TangQuaID as TangQuaID,tinhtrangtang.TinhTrang from khachhang left outer join tinhtrangtang on khachhang.KHID = tinhtrangtang.khachhang_KHID where tinhtrangtang.tangqua_TangQuaID = '" + lookUpEdit1.EditValue.ToString() + "' and khachhang.KHID is not null;";
                    cmd.Connection = cn.conn;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("KHID");
                    dt.Columns.Add("CMND");
                    dt.Columns.Add("TenKH");
                    dt.Columns.Add("TenThuongGoi");
                    dt.Columns.Add("TangQuaID");
                    dt.Columns.Add("TinhTrang");
                    MySqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["KHID"] = rd["KHID"];
                        row["CMND"] = rd["CMND"];
                        row["TenKH"] = rd["TenKH"];
                        row["TenThuongGoi"] = rd["TenThuongGoi"];
                        row["TangQuaID"] = rd["TangQuaID"];
                        row["TinhTrang"] = rd["TinhTrang"];
                        dt.Rows.Add(row);
                    }

                    rd.Close();
                    gvKhnhanqua.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                    SplashScreenManager.CloseForm();
                    a = false;
                }
                catch 
                {
                    if(a == true)
                    {
                        SplashScreenManager.CloseForm();
                    }
                    a = false;
                }
                finally
                {
                    cn.closeconnection();
                    cn.conn.Dispose();
                }
             //   t.Abort();
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            int rowIndex = e.RowHandle;
            if (rowIndex >= 0)
            {
                rowIndex++;
                e.Info.DisplayText = rowIndex.ToString();
            }
        }

        private void lookUpEdit1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                adpter = new MySqlDataAdapter("SELECT * FROM tangqua;", cn.conn);
                ds = new DataSet();
                adpter.Fill(ds);

                lookUpEdit1.Properties.DataSource = ds.Tables[0];
                lookUpEdit1.Properties.DisplayMember = "QuyCach";
                lookUpEdit1.Properties.ValueMember = "TangQuaID";
            }
            catch { }
        }
    }
}