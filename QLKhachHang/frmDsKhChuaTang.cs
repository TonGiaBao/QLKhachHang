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
    public partial class frmDsKhChuaTang : DevExpress.XtraEditors.XtraUserControl
    {
        bool a = false;
        Connect cn = new Connect();
        MySqlDataAdapter adpter = null;
        DataSet ds = new DataSet();
        public frmDsKhChuaTang()
        {
            InitializeComponent();
        }

        private void frmDsKhChuaTang_Load(object sender, EventArgs e)
        {
            try
            {
                gridView1.Columns[0].Visible = false;
                adpter = new MySqlDataAdapter("SELECT * FROM tangqua;", cn.conn);
                DataSet dsNienKhoa = new DataSet();
                adpter.Fill(dsNienKhoa);

                lookUpEdit1.Properties.DataSource = dsNienKhoa.Tables[0];
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
            //adpter = new MySqlDataAdapter("select * from ql.khachhang where ql.khachhang.KHID not in( select ql.khachhang.KHID from ql.khachhang left outer join ql.tinhtrangtang on ql.khachhang.KHID = ql.tinhtrangtang.khachhang_KHID where ql.tinhtrangtang.tangqua_TangQuaID = '"+lookUpEdit1.EditValue.ToString()+"');", cn.conn);
            //ds = new DataSet();
            //adpter.Fill(ds);
            //gvDskh.DataSource = ds.Tables[0];
            if (lookUpEdit1.Text == "Mời chọn")
            {
                XtraMessageBox.Show("Bạn chưa chọn đợt tặng quà");
            }
            else
            {
                try
                {
                    SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
                  //  Thread t = new Thread(new ThreadStart(StartForm));
                  //  t.Start();
                    a = true;
                    cn.openconnection();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select khachhang.KHID,khachhang.CMND,khachhang.TenKH,khachhang.TenThuongGoi from khachhang where khachhang.KHID not in(select khachhang.KHID from khachhang left outer join tinhtrangtang on khachhang.KHID = tinhtrangtang.khachhang_KHID where tinhtrangtang.tangqua_TangQuaID = '" + lookUpEdit1.EditValue.ToString() + "');";
                    cmd.Connection = cn.conn;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("KHID");
                    dt.Columns.Add("CMND");
                    dt.Columns.Add("TenKH");
                    dt.Columns.Add("TenThuongGoi");
                    MySqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["KHID"] = rd["KHID"];
                        row["CMND"] = rd["CMND"];
                        row["TenKH"] = rd["TenKH"];
                        row["TenThuongGoi"] = rd["TenThuongGoi"];

                        dt.Rows.Add(row);
                    }

                    rd.Close();
                    gvDskh.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                    SplashScreenManager.CloseForm();
                    a = false;
                //    t.Abort();
                    
                    //  gridColumn3.BestFit();
                }
                catch (Exception ex) 
                { 
                    if(a == true)
                    {
                        SplashScreenManager.CloseForm();
                    }
                    a = false;
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    cn.closeconnection();
                    cn.conn.Dispose();
                }
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

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            frmDsKhChuaTang_Load(sender, e);
        }

        private void lookUpEdit1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                gridView1.Columns[0].Visible = false;
                adpter = new MySqlDataAdapter("SELECT * FROM tangqua;", cn.conn);
                DataSet dsNienKhoa = new DataSet();
                adpter.Fill(dsNienKhoa);

                lookUpEdit1.Properties.DataSource = dsNienKhoa.Tables[0];
                lookUpEdit1.Properties.DisplayMember = "QuyCach";
                lookUpEdit1.Properties.ValueMember = "TangQuaID";
            }
            catch { }
        }
    }
}