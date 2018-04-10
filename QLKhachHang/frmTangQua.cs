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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;

namespace QLKhachHang
{
    public partial class frmTangQua : DevExpress.XtraEditors.XtraUserControl
    {
        Connect cn = new Connect();
        int vt = -1;
        public int tqid1;
        public string quycach1;
        public string ghichu1;
        bool c = false;
        public frmTangQua()
        {          
            InitializeComponent();            
        }

        private void frmTangQua_Load(object sender, EventArgs e)
        {
            try
            {
                loadgv();
            }
            catch { }
        }

        public void loadgv()
        {
            try
            {
                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from tangqua";
                cmd.Connection = cn.conn;
                DataTable dt = new DataTable();
                dt.Columns.Add("TangQuaID");
                dt.Columns.Add("QuyCach");
                dt.Columns.Add("GhiChu");
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DataRow row = dt.NewRow();
                    row["TangQuaID"] = rd["TangQuaID"];
                    row["QuyCach"] = rd["QuyCach"];
                    row["GhiChu"] = rd["GhiChu"];
                    dt.Rows.Add(row);
                }
                rd.Close();
                gvTangqua.DataSource = dt;

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

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmTangQuaAdd f = new frmTangQuaAdd(this);
            f.ShowDialog();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                if (view.FocusedRowHandle == vt)
                {
                    return;
                }
                else
                {
                    c = true;
                    object tqid = view.GetRowCellValue(view.FocusedRowHandle, "TangQuaID");
                    object quycach = view.GetRowCellValue(view.FocusedRowHandle, "QuyCach");
                    object ghichu = view.GetRowCellValue(view.FocusedRowHandle, "GhiChu");

                    tqid1 = Int32.Parse(tqid.ToString());
                    quycach1 = quycach.ToString();
                    ghichu1 = ghichu.ToString();                 
                }
            }
            catch { }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {

                if (DialogResult.Yes == XtraMessageBox.Show("Bạn có muốn xóa? " + quycach1 + "", "Thông báo", MessageBoxButtons.YesNo))
                {
                    cn.openconnection();
                    MySqlCommand cm = new MySqlCommand();
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "delete from tangqua where TangQuaID = @tqid";
                    cm.Connection = cn.conn;
                    cm.Parameters.Add("@tqid", SqlDbType.Int).Value = tqid1;
                    int kq = cm.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        frmTangQua_Load(sender, e);
                        XtraMessageBox.Show("Đã xóa tặng quà");

                    }
                    else
                    {
                        XtraMessageBox.Show("Xóa thất bại");
                    }
                    // Load_gird_view();
                    //    DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(0, 0);

                }
            }
            catch { }
            finally
            {
                cn.closeconnection();
                cn.conn.Dispose();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmTangQuaSua f = new frmTangQuaSua(this,tqid1,quycach1,ghichu1);
            f.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmTangQuaAdd f = new frmTangQuaAdd(this);
            f.ShowDialog();
        }
    }
}