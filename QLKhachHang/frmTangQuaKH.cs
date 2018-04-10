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
using System.Collections;
using DevExpress.XtraSplashScreen;
using System.Threading;

namespace QLKhachHang
{
    public partial class frmTangQuaKH : DevExpress.XtraEditors.XtraUserControl
    {
        Connect cn = new Connect();
        MySqlDataAdapter adpter = null;
        DataSet ds = new DataSet();
        bool a = false;
        public frmTangQuaKH()
        {
            InitializeComponent();
        }

        void loadlookup()
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
            catch
            {

            }
        }
        private void frmTangQuaKH_Load(object sender, EventArgs e)
        {
            // gridView1.Columns[1].Visible = false;
            loadlookup();
        }
        private void StartForm()
        {
            Application.Run(new frmWait());
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            /*SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
            gvTangKH.DataSource = null;
            adpter = new MySqlDataAdapter("select ql.khachhang.KHID,ql.khachhang.CMND,ql.khachhang.TenKH,count(ql.giaodich.SoLanGD) as SoLanGiaoDich ,sum(CAST(replace(ql.giaodich.TongTien,'.','') AS DECIMAL(10,0))) as TongTienGiaoDich from ql.khachhang left join ql.giaodich on ql.khachhang.KHID = ql.giaodich.khachhang_KHID where ql.khachhang.KHID = ql.giaodich.khachhang_KHID and ql.giaodich.NgayGD between '" + dtNgaybatdau.Text + "' and '" + dtNgayketthuc.Text + "' group by ql.khachhang.KHID,ql.khachhang.CMND,ql.khachhang.TenKH;", cn.conn);
            ds = new DataSet();
            adpter.Fill(ds);
            gvTangKH.DataSource = ds.Tables[0];
            SplashScreenManager.CloseForm();*/
            try
            {
                gvTangKH.DataSource = null;
                SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
                a = true;
                // Thread t = new Thread(new ThreadStart(StartForm));
                //  t.Start();
                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
              //  cmd.CommandText = "select khachhang.KHID,khachhang.CMND,khachhang.TenKH,count(giaodich.SoLanGD) as SoLanGiaoDich ,sum(CAST(replace(giaodich.TongTien,'.','') AS DECIMAL(20,0))) as TongTienGiaoDich from khachhang left join giaodich on khachhang.KHID = giaodich.khachhang_KHID where khachhang.KHID = giaodich.khachhang_KHID and giaodich.NgayGD between '" + dtNgaybatdau.Text + "' and '" + dtNgayketthuc.Text + "' group by khachhang.KHID,khachhang.CMND,khachhang.TenKH;";
             //   cmd.CommandText = "select khachhang.KHID,khachhang.CMND,khachhang.TenKH,count(giaodich.SoLanGD) as SoLanGiaoDich ,sum(CAST(replace(giaodich.TongTien,'.','') AS DECIMAL(20,0))) as TongTienGiaoDich from khachhang left join giaodich on khachhang.KHID = giaodich.khachhang_KHID where khachhang.KHID = giaodich.khachhang_KHID and giaodich.NgayGD >= STR_TO_DATE('" + dtNgaybatdau.EditValue + "','%d/%m/%Y %H:%i:%s') and giaodich.NgayGD <= STR_TO_DATE('" + dtNgayketthuc.EditValue + "','%d/%m/%Y %H:%i:%s') group by khachhang.KHID,khachhang.CMND,khachhang.TenKH ORDER BY giaodich.NgayGD DESC;";
                cmd.CommandText = "select khachhang.KHID,khachhang.CMND,khachhang.TenKH,count(giaodich.SoLanGD) as SoLanGiaoDich ,sum(CAST(REPLACE(REPLACE(REPLACE(TongTien,'.','@'),'@',''),',','.') AS DECIMAL(20,2))) as TongTienGiaoDich from khachhang left join giaodich on khachhang.KHID = giaodich.khachhang_KHID where giaodich.IsXoa = 1 and khachhang.KHID = giaodich.khachhang_KHID and giaodich.NgayGiaoDich >= STR_TO_DATE('" + dtNgaybatdau.EditValue + "','%d/%m/%Y %H:%i:%s') and giaodich.NgayGiaoDich <= STR_TO_DATE('" + dtNgayketthuc.EditValue + "','%d/%m/%Y %H:%i:%s') group by khachhang.KHID,khachhang.CMND,khachhang.TenKH ORDER BY giaodich.NgayGiaoDich DESC;";
                //     cmd.CommandText = "select khachhang.KHID,khachhang.CMND,khachhang.TenKH,count(giaodich.SoLanGD) as SoLanGiaoDich ,sum(CAST(replace(giaodich.TongTien,'.','') AS DECIMAL(20,0))) as TongTienGiaoDich from khachhang left join giaodich on khachhang.KHID = giaodich.khachhang_KHID where khachhang.KHID = giaodich.khachhang_KHID and giaodich.NgayGD between STR_TO_DATE('" + dtNgaybatdau.Text + "','%d/%m/%Y 00:00:00') and STR_TO_DATE('" + dtNgayketthuc.Text + "','%d/%m/%Y 23:59:59') group by khachhang.KHID,khachhang.CMND,khachhang.TenKH;";
                cmd.Connection = cn.conn;
                DataTable dt = new DataTable();
                dt.Columns.Add("KHID");
                dt.Columns.Add("CMND");
                dt.Columns.Add("TenKH");
                dt.Columns.Add("SoLanGiaoDich");
                dt.Columns.Add("TongTienGiaoDich", typeof(decimal));
                dt.Columns.Add("A");
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DataRow row = dt.NewRow();
                    row["KHID"] = rd["KHID"];
                    row["CMND"] = rd["CMND"];
                    row["TenKH"] = rd["TenKH"];
                    row["SoLanGiaoDich"] = rd["SoLanGiaoDich"];
                    row["TongTienGiaoDich"] = rd["TongTienGiaoDich"];
                    dt.Rows.Add(row);
                }

                rd.Close();
                gvTangKH.DataSource = dt;
                //      gridView1.Columns[0].Visible = false;
                SplashScreenManager.CloseForm();
                a = false;
                
                //     t.Abort();
                
            }
            catch(Exception ex)
            {
                if(a == true)
                {
                    SplashScreenManager.CloseForm();
                    a = false;
                }
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                cn.closeconnection();
                cn.conn.Dispose();
            }
            //  gridView1.Columns[1].Visible = false;
            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    //object tt = gridView1.GetRowCellValue(i, "0").ToString();
            //    string ht = gridView1.GetRowCellValue(i, "TenKH").ToString();
            //    string slgd = gridView1.GetRowCellValue(i, "SoLanGiaoDich").ToString();
            //    string ttgd = gridView1.GetRowCellValue(i, "TongTienGiaoDich").ToString();
            //}
            //
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (lookUpEdit1.Text == "Mời chọn")
            {
                XtraMessageBox.Show("Bạn chưa chọn đợt tặng quà");
                return;
            }
            else
            {
                SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
                a = true;
            //    Thread th = new Thread(new ThreadStart(StartForm));
            //    th.Start();
                frmTangQuaKH f = new frmTangQuaKH();
                try
                {
                    string ten = "";
                    string text = "";
                    int selected = new int();
                    bool check = false;
                    ArrayList rows = new ArrayList();
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        
                        if (gridView1.GetSelectedRows()[i] >= 0)
                        {
                            rows.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));
                        }
                    }
                    for (int i = 0; i < rows.Count; i++)
                    {
                        DataRow r = (DataRow)rows[i];
                        object t = r["KHID"];
                        object q = r["A"];
                        try
                        {
                            cn.openconnection();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.CommandType = CommandType.Text;
                            string sql = "insert into tinhtrangtang(TinhTrang,tangqua_TangQuaID,khachhang_KHID) values(@tinhtrang,@tangquaid,@khid); ";
                            cmd.CommandText = sql;
                            cmd.Connection = cn.conn;
                            cmd.Parameters.Add("@tinhtrang", MySqlDbType.VarChar).Value = q;
                            cmd.Parameters.Add("@tangquaid", MySqlDbType.Int32).Value = lookUpEdit1.EditValue.ToString();
                            cmd.Parameters.Add("@khid", MySqlDbType.Int32).Value = t;
                            //      cmd.Parameters.Add("@tenkh", MySqlDbType.VarChar).Value = txtHoten.Text;
                            MySqlCommand cmd2 = new MySqlCommand();
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = "select khachhang.TenKH from khachhang where khachhang.KHID = '"+t+"' ";
                            cmd2.Connection = cn.conn;               
                            MySqlDataReader rd2 = cmd2.ExecuteReader();
                            if(rd2.Read())
                            {
                                ten = rd2.GetString(0);
                            }
                            rd2.Close();
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                //  XtraMessageBox.Show("Data Inserted");
                            }
                            else
                            {
                                XtraMessageBox.Show("Lưu thất bại");
                            }
                        }
                        catch
                        {
                            text += "Khách hàng: " + ten + " đã nhận quà đợt " + lookUpEdit1.Text + Environment.NewLine;
                            check = true;
                            continue;
                        }

                    }
                    SplashScreenManager.CloseForm();
                    a = false;
                    selected = rows.Count;
                    if (selected == 0)
                    {
                        XtraMessageBox.Show("Bạn chưa chọn khách hàng");
                        return;
                    }
                 //   th.Abort();
                    if (check == true)
                    {
                    /*    if(text == "")
                        {
                            XtraMessageBox.Show("Đã lưu");
                            return;
                        }*/
                        string s = text;
                        XtraMessageBox.Show(text);
                    }
                    else
                    {
                        XtraMessageBox.Show("Đã lưu");
                    }
                }
                catch (Exception ex) 
                { 
                    if(a == true)
                    {
                        SplashScreenManager.CloseForm();
                        a = false;
                    }
                    XtraMessageBox.Show(ex.Message); 
                }
                finally { cn.closeconnection();
                cn.conn.Dispose();
                }
            }
            //}
            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    //object tt = gridView1.GetRowCellValue(i, "0").ToString();
            //    string ht = gridView1.GetRowCellValue(i, "TenKH").ToString();
            //    string slgd = gridView1.GetRowCellValue(i, "SoLanGiaoDich").ToString();
            //    string ttgd = gridView1.GetRowCellValue(i, "TongTienGiaoDich").ToString();


            //}
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
            loadlookup();
        }
    }
}