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
using System.Data.SqlClient;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Base;
using System.Globalization;

namespace QLKhachHang
{
    public partial class frmKhachHang : DevExpress.XtraEditors.XtraUserControl
    {
        // frmGDWUAll f1 = new frmGDWUAll(this);
        SplashScreenManager splash = new SplashScreenManager();
        Connect cn = new Connect();
        DataSet ds = new DataSet();
        MySqlDataAdapter adpter = null;
        public int khidsuaload;
        public int khid;
        public string cmnd;
        public string tenkh;
        string ngaysinh;
        bool gt;
        string nguyenquan;
        string hktt;
        string dt;
        string tg;
        byte[] mtcmnd = null;
        byte[] mscmnd = null;
        string sdt;
        string ttg;
        bool khwu;
        string gd;
        string ghichu;
        DateTime ngaycap;
        DateTime ngaycap2;
        int vt = -1;
        int n = 10;
        private readonly frmGDWUAll f2;
        bool state = false;

        public frmKhachHang()
        {           
            InitializeComponent();           
        }

        //private void SortGroupsBySummary(GridColumn column)
        //{
        //    //GridGroupSummarySortInfo sortInfo = new GridGroupSummarySortInfo(grid.GroupSummary[0],
        //    //    column.FieldName, System.ComponentModel.ListSortDirection.Ascending);
        //    //grid.GroupSummarySortInfo.Add(sortInfo);

        //}
        private void btnThem_Click(object sender, EventArgs e)
        {
            frmKhachHangAdd f = new frmKhachHangAdd(this);
            f.ShowDialog();
        }

        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        public void countkh()
        {
            try
            {
                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select count(TenKH) from khachhang";
                cmd.Connection = cn.conn;
                object kq = cmd.ExecuteScalar();
                string op = kq.ToString();
                lblCount.Text = op;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

    /*    public void loadidsua()
        {
            try
            {
                string sql = string.Format(@"SELECT KHID ,CMND , TenKH , NgaySinh , IF(GioiTinh = 1, 'Nam', N'Nữ') AS 'GioiTinh' , NguyenQuan , HoKhauThuongTru , DanToc ,SDT , TenThuongGoi , case KHWU when 1 then 'Có' else N'Không' end as `KHWU`, GiaoDich , GhiChu,NgayCap FROM `khachhang` WHERE KHID = '" + khidsuaload + "'");
                adpter = new MySqlDataAdapter(sql, cn.conn);
                ds = new DataSet();
                adpter.Fill(ds);
                gvKhachhang.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public void loadmaxid()
        {
            //cn.openconnection();
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * FROM `khachhang` WHERE KHID = (SELECT max(KHID) FROM khachhang)";
            //cmd.Connection = cn.conn;
            try
            {
                string sql = string.Format(@"SELECT KHID ,CMND , TenKH , NgaySinh , IF(GioiTinh = 1, 'Nam', N'Nữ') AS 'GioiTinh' , NguyenQuan , HoKhauThuongTru , DanToc ,SDT , TenThuongGoi , case KHWU when 1 then 'Có' else N'Không' end as `KHWU`, GiaoDich , GhiChu,NgayCap FROM `khachhang` WHERE KHID = (SELECT max(KHID) FROM khachhang)");
                adpter = new MySqlDataAdapter(sql, cn.conn);
                ds = new DataSet();
                adpter.Fill(ds);
                gvKhachhang.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }*/

        private void loadsearch()
        {
            try
            {
                //adpter = new MySqlDataAdapter("select KHID ,CMND , TenKH 'Tên khách hàng', NgaySinh 'Ngày sinh', case GioiTinh when 1 then 'Nam' else N'Nữ' end as 'Giới tính', NguyenQuan 'Nguyên quán', HoKhauThuongTru 'Hktt', DanToc 'Dân tộc', TonGiao 'Tôn giáo', MatTruocCMND 'Mặt trước cmnd', MatSauCMND 'Mặt sau cmnd', SDT 'Sđt', TenThuongGoi 'Tên khác', case KHWU when 1 then 'Có' else N'Không' end as 'WU', GiaoDich 'Giao dịch', GhiChu 'Ghi chú' " +
                //                         "from khachhang", cn.conn);
                // ds = new DataSet();
                // adpter.Fill(ds);
                // gvKhachhang.DataSource = ds.Tables[0];
                // gridView1.Columns[0].Visible = false;
                //   gridView1.Columns[2].BestFit = true;

                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                string sql = string.Format(@"select KHID ,CMND , TenKH , NgaySinh , IF(GioiTinh = 1, 'Nam', N'Nữ') AS 'GioiTinh' , HoKhauThuongTru ,SDT , TenThuongGoi , case KHWU when 1 then 'Có' else N'Không' end as `KHWU`, GiaoDich , GhiChu, NgayCap from khachhang where TenKH LIKE N'%{0}%' or CMND LIKE N'%{0}%'", textEdit1.Text);
                cmd.CommandText = sql;
                cmd.Connection = cn.conn;
                DataTable dt = new DataTable();
                dt.Columns.Add("KHID");
                dt.Columns.Add("CMND");
                dt.Columns.Add("TenKH");
                dt.Columns.Add("NgaySinh");
                dt.Columns.Add("GioiTinh");
          //      dt.Columns.Add("NguyenQuan");
                dt.Columns.Add("HoKhauThuongTru");
           //     dt.Columns.Add("DanToc");
           //     dt.Columns.Add("TonGiao");
                //     dt.Columns.Add("MatTruocCMND", typeof(byte[]));
                //     dt.Columns.Add("MatSauCMND", typeof(byte[]));
                dt.Columns.Add("SDT");
                dt.Columns.Add("TenThuongGoi");
                dt.Columns.Add("KHWU");
                dt.Columns.Add("GiaoDich");
                dt.Columns.Add("GhiChu");
                dt.Columns.Add("NgayCap");
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DataRow row = dt.NewRow();
                    row["KHID"] = rd["KHID"];
                    row["CMND"] = rd["CMND"];
                    row["TenKH"] = rd["TenKH"];
                    row["NgaySinh"] = rd["NgaySinh"];
                    row["GioiTinh"] = rd["GioiTinh"];
                 //   row["NguyenQuan"] = rd["NguyenQuan"];
                    row["HoKhauThuongTru"] = rd["HoKhauThuongTru"];
                //    row["DanToc"] = rd["DanToc"];
                //    row["TonGiao"] = rd["TonGiao"];
                    //     row["MatTruocCMND"] = rd["MatTruocCMND"];
                    //      row["MatSauCMND"] = rd["MatSauCMND"];
                    row["SDT"] = rd["SDT"];
                    row["TenThuongGoi"] = rd["TenThuongGoi"];
                    row["KHWU"] = rd["KHWU"];
                    row["GiaoDich"] = rd["GiaoDich"];
                    row["GhiChu"] = rd["GhiChu"];
                    string s = rd["NgayCap"].ToString();
                    int r = s.Length;
                    if (r == 0)
                    {
                        row["NgayCap"] = s;
                    }
                    else
                    {
                        s = Left(s, 10);
                        row["NgayCap"] = s;
                    }
                    dt.Rows.Add(row);
                }
                rd.Close();
                gvKhachhang.DataSource = dt;
              //  gridColumn3.BestFit();

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
        private void load()
        {
            try
            {
                   //adpter = new MySqlDataAdapter("select KHID ,CMND , TenKH 'Tên khách hàng', NgaySinh 'Ngày sinh', case GioiTinh when 1 then 'Nam' else N'Nữ' end as 'Giới tính', NguyenQuan 'Nguyên quán', HoKhauThuongTru 'Hktt', DanToc 'Dân tộc', TonGiao 'Tôn giáo', MatTruocCMND 'Mặt trước cmnd', MatSauCMND 'Mặt sau cmnd', SDT 'Sđt', TenThuongGoi 'Tên khác', case KHWU when 1 then 'Có' else N'Không' end as 'WU', GiaoDich 'Giao dịch', GhiChu 'Ghi chú' " +
                   //                         "from khachhang", cn.conn);
                   // ds = new DataSet();
                   // adpter.Fill(ds);
                   // gvKhachhang.DataSource = ds.Tables[0];
                   // gridView1.Columns[0].Visible = false;
                 //   gridView1.Columns[2].BestFit = true;

                    cn.openconnection();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select KHID ,CMND , TenKH , NgaySinh , IF(GioiTinh = 1, 'Nam', N'Nữ') AS 'GioiTinh' , HoKhauThuongTru ,SDT , TenThuongGoi , case KHWU when 1 then 'Có' else N'Không' end as `KHWU`, GiaoDich , GhiChu, NgayCap from khachhang";
                    cmd.Connection = cn.conn;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("KHID");
                    dt.Columns.Add("CMND");
                    dt.Columns.Add("TenKH");
                    dt.Columns.Add("NgaySinh");
                    dt.Columns.Add("GioiTinh");
                 //   dt.Columns.Add("NguyenQuan");
                    dt.Columns.Add("HoKhauThuongTru");
                 //   dt.Columns.Add("DanToc");
                 //   dt.Columns.Add("TonGiao");
               //     dt.Columns.Add("MatTruocCMND", typeof(byte[]));
               //     dt.Columns.Add("MatSauCMND", typeof(byte[]));
                    dt.Columns.Add("SDT");
                    dt.Columns.Add("TenThuongGoi");
                    dt.Columns.Add("KHWU");
                    dt.Columns.Add("GiaoDich");
                    dt.Columns.Add("GhiChu");
                    dt.Columns.Add("NgayCap");
                    MySqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        DataRow row = dt.NewRow();
                        int a = Int32.Parse(rd["KHID"].ToString());
                        row["KHID"] = rd["KHID"];
                        row["CMND"] = rd["CMND"];
                        row["TenKH"] = rd["TenKH"];
                        row["NgaySinh"] = rd["NgaySinh"];
                        row["GioiTinh"] = rd["GioiTinh"];
                    //    row["NguyenQuan"] = rd["NguyenQuan"];
                        row["HoKhauThuongTru"] = rd["HoKhauThuongTru"];
                   //     row["DanToc"] = rd["DanToc"];
                    //    row["TonGiao"] = rd["TonGiao"];
                   //     row["MatTruocCMND"] = rd["MatTruocCMND"];
                  //      row["MatSauCMND"] = rd["MatSauCMND"];
                        row["SDT"] = rd["SDT"];
                        row["TenThuongGoi"] = rd["TenThuongGoi"];
                        row["KHWU"] = rd["KHWU"];
                        row["GiaoDich"] = rd["GiaoDich"];
                        row["GhiChu"] = rd["GhiChu"];
                        string s = rd["NgayCap"].ToString();
                        int r = s.Length;
                        if(r == 0)
                        {
                            row["NgayCap"] = s;
                        }
                        else
                        {
                            s = Left(s, 10);
                            row["NgayCap"] = s;
                        }                        
                        dt.Rows.Add(row);
                    }
                    rd.Close();
                    gvKhachhang.DataSource = dt;
               //     gridColumn3.BestFit();

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
        public void frmKhachHang_Load(object sender, EventArgs e)
        {
            countkh();
            load();
            //gridColumnGioitinh.BestFit();
            //gridColumnWu.BestFit();
            //gridColumn17.BestFit();
            //gridColumn4.BestFit();
            //gridColumn12.BestFit();
            //gridColumn15.BestFit();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {

                if (DialogResult.Yes == XtraMessageBox.Show("Bạn có muốn xóa? Tên khách hàng " + tenkh + "", "Thông báo", MessageBoxButtons.YesNo))
                {
                    
                    if (!splash.IsSplashFormVisible)
                    {
                      //  splashScreenManager.ShowWaitForm();
                        SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
                    }
                    state = true;
                    cn.openconnection();
                    MySqlCommand cm = new MySqlCommand();
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "delete from khachhang where KHID = @khid";
                    cm.Connection = cn.conn;
                    cm.Parameters.Add("@khid", SqlDbType.Int).Value = khid;
                    int kq = cm.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        if (splash.IsSplashFormVisible || state == true)
                        {
                            SplashScreenManager.CloseForm();
                        }
                        state = false;
                        XtraMessageBox.Show("Đã xóa khách hàng");
                        frmKhachHang_Load(sender, e);
                    //    gvKhachhang.DataSource = null;
                        countkh();
                    }
                    else
                    {
                        XtraMessageBox.Show("Xóa thất bại");
                    }
                    // Load_gird_view();
                    //    DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(0, 0);

                }
            }
            catch (Exception ex) 
            {
                if (splash.IsSplashFormVisible || state == true)
                {
                    SplashScreenManager.CloseForm();
                    state = false;
                }   
                XtraMessageBox.Show(ex.Message); 
            }
            finally
            {
                cn.closeconnection();
                cn.conn.Dispose();
            }
        }
        bool c = false;
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public static byte[] RawSerializeEx(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            byte[] rawdatas = new byte[rawsize];
            GCHandle handle = GCHandle.Alloc(rawdatas, GCHandleType.Pinned);
            IntPtr buffer = handle.AddrOfPinnedObject();
            Marshal.StructureToPtr(anything, buffer, false);
            handle.Free();
            return rawdatas;
        }
        private void gridView1_RowClick(object sender, RowClickEventArgs e)
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
                    object khid1 = view.GetRowCellValue(view.FocusedRowHandle, "KHID");                   
                    khid = Int32.Parse(khid1.ToString());
                    try
                    {
                        cn.openconnection();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select KHID ,CMND , TenKH , NgaySinh , IF(GioiTinh = 1, 'Nam', N'Nữ') AS 'GioiTinh' , HoKhauThuongTru ,MatTruocCMND,MatSauCMND ,SDT , TenThuongGoi , case KHWU when 1 then 'Có' else N'Không' end as `KHWU`, GiaoDich , GhiChu, NgayCap from khachhang where KHID = '"+khid+"'";
                        cmd.Connection = cn.conn;
                        MySqlDataReader rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            khid = Int32.Parse(rd["KHID"].ToString());
                            cmnd = rd["CMND"].ToString();
                            tenkh = rd["TenKH"].ToString();
                            ngaysinh = rd["NgaySinh"].ToString();
                            string h = rd["GioiTinh"].ToString();
                            if (h == "Nam")
                            {
                                gt = true;
                            }
                            else
                            {
                                gt = false;
                            }
                          //  nguyenquan = rd["NguyenQuan"].ToString();
                            hktt = rd["HoKhauThuongTru"].ToString();
                       //     dt = rd["DanToc"].ToString();
                        //    tg = rd["TonGiao"].ToString();
                            object mt = rd["MatTruocCMND"];
                            object ms = rd["MatSauCMND"];
                            if (Convert.IsDBNull(mt))
                            {
                                mtcmnd = null;
                            }
                            else
                            {
                                mtcmnd = (byte[])mt;
                            }
                            if (Convert.IsDBNull(ms))
                            {
                                mscmnd = null;
                            }
                            else
                            {
                                mscmnd = (byte[])ms;
                            }
                            sdt = rd["SDT"].ToString();
                            ttg = rd["TenThuongGoi"].ToString();
                            string y = rd["KHWU"].ToString();
                            if (y == "Có")
                            {
                                khwu = true;
                            }
                            else
                            {
                                khwu = false;
                            }
                            gd = rd["GiaoDich"].ToString();
                            ghichu = rd["GhiChu"].ToString();
                            string s = rd["NgayCap"].ToString();
                            int r = s.Length;
                            if (r == 0)
                            {
                                ngaycap = ngaycap2;
                            }
                            else
                            {
                                s = Left(s, 10);
                                ngaycap = Convert.ToDateTime(rd["NgayCap"].ToString());
                            }
                        }
                        rd.Close();
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

            }
            catch { }


          /*  try
            {
                GridView view = (GridView)sender;
                if (view.FocusedRowHandle == vt)
                {
                    return;
                }
                else
                {
                    c = true;
                    object khid1 = view.GetRowCellValue(view.FocusedRowHandle, "KHID");
                    object cmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "CMND");
                    object tenkh1 = view.GetRowCellValue(view.FocusedRowHandle, "TenKH");
                    object ngaysinh1 = view.GetRowCellValue(view.FocusedRowHandle, "NgaySinh");
                    object gioitinh1 = view.GetRowCellValue(view.FocusedRowHandle, "GioiTinh");
                    object nguyenquan1 = view.GetRowCellValue(view.FocusedRowHandle, "NguyenQuan");
                    object hktt1 = view.GetRowCellValue(view.FocusedRowHandle, "HoKhauThuongTru");
                    object dantoc1 = view.GetRowCellValue(view.FocusedRowHandle, "DanToc");
                    object tongiao1 = view.GetRowCellValue(view.FocusedRowHandle, "TonGiao");
                    object mtcmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "MatTruocCMND");
                    object mscmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "MatSauCMND");
                    object sdt1 = view.GetRowCellValue(view.FocusedRowHandle, "SDT");
                    object tentg = view.GetRowCellValue(view.FocusedRowHandle, "TenThuongGoi");
                    object khwu1 = view.GetRowCellValue(view.FocusedRowHandle, "KHWU");
                    object giaodich1 = view.GetRowCellValue(view.FocusedRowHandle, "GiaoDich");
                    object ghichu1 = view.GetRowCellValue(view.FocusedRowHandle, "GhiChu");
                    object ngaycap1 = view.GetRowCellValue(view.FocusedRowHandle, "NgayCap");
                    khid = Int32.Parse(khid1.ToString());
                    cmnd = cmnd1.ToString();
                    tenkh = tenkh1.ToString();
                    ngaysinh = ngaysinh1.ToString();
                    string s = gioitinh1.ToString();
                    if (s == "Nam")
                    {
                        gt = true;
                    }
                    else
                    {
                        gt = false;
                    }
                    nguyenquan = nguyenquan1.ToString();
                    hktt = hktt1.ToString();
                    dt = dantoc1.ToString();
                    tg = tongiao1.ToString();
                    if (Convert.IsDBNull(mtcmnd1))
                    {
                        mtcmnd = null;
                    }
                    else
                    {
                        mtcmnd = (byte[])mtcmnd1;
                    }
                    if (Convert.IsDBNull(mscmnd1))
                    {
                        mscmnd = null;
                    }
                    else
                    {
                        mscmnd = (byte[])mscmnd1;
                    }

                    sdt = sdt1.ToString();
                    ttg = tentg.ToString();
                    string h = khwu1.ToString();
                    if (h == "Có")
                    {
                        khwu = true;
                    }
                    else
                    {
                        khwu = false;
                    }
                    gd = giaodich1.ToString();
                    ghichu = ghichu1.ToString();
                    string nc = ngaycap1.ToString();
                    if (ngaycap1.ToString() == "" || ngaycap1 == null)
                    {
                        ngaycap = ngaycap2;
                    }
                    else
                    {
                        ngaycap = Convert.ToDateTime(ngaycap1);
                    }
                    //ngaycap = DateTime.Parse(nc);
                    //    MemoryStream ms = new MemoryStream(img);

                     //mtcmnd = (ObjectToByteArray(mtcmnd1));
                     //if (mtcmnd != ObjectToByteArray(mtcmnd1))
                     //{
                     //    var s = ObjectToByteArray(mtcmnd1);
                     //}
                }

            }
            catch { }*/

        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (c == false)
            {
                XtraMessageBox.Show("Bạn chưa chọn dòng");
                return;
            }
            else
            {
                try
                {

                    if (DialogResult.Yes == XtraMessageBox.Show("Bạn có muốn xóa? Tên khách hàng " + tenkh + "", "Thông báo", MessageBoxButtons.YesNo))
                    {

                        if (!splash.IsSplashFormVisible)
                        {
                            //  splashScreenManager.ShowWaitForm();
                            SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
                        }
                        state = true;
                        cn.openconnection();
                        MySqlCommand cm = new MySqlCommand();
                        cm.CommandType = CommandType.Text;
                        cm.CommandText = "delete from khachhang where KHID = @khid";
                        cm.Connection = cn.conn;
                        cm.Parameters.Add("@khid", SqlDbType.Int).Value = khid;
                        int kq = cm.ExecuteNonQuery();
                        if (kq > 0)
                        {
                            if (splash.IsSplashFormVisible || state == true)
                            {
                                SplashScreenManager.CloseForm();
                            }                           
                            state = false;
                            XtraMessageBox.Show("Đã xóa khách hàng");
                            frmKhachHang_Load(sender, e);
                            //    gvKhachhang.DataSource = null;
                            countkh();
                        }
                        else
                        {
                            XtraMessageBox.Show("Xóa thất bại");
                        }
                        // Load_gird_view();
                        //    DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(0, 0);

                    }
                }
                catch (Exception ex)
                {
                    if (splash.IsSplashFormVisible || state == true)
                    {
                        SplashScreenManager.CloseForm();
                        state = false;
                    }
                    
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    cn.closeconnection();
                    cn.conn.Dispose();
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            btnThem_Click(sender, e);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (c == false)
            {
                XtraMessageBox.Show("Bạn chưa chọn khách hàng");
                return;
            }
            else
            {
                frmKhachHangSua f = new frmKhachHangSua(this, khid, cmnd, tenkh, ngaysinh, gt, nguyenquan, hktt, dt, tg, mtcmnd, mscmnd, sdt, ttg, khwu, gd, ghichu,ngaycap);
            //    khidsuaload = khid;
                f.ShowDialog();
                c = false;
            }
        }
        private void frmChild_Closing(object sender, FormClosedEventArgs e)
        {

            // f.frmGDWUAll_Load(f,e);
        }
        public int tt = 0;
        private void giaoDichWUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (c == false)
            {
                XtraMessageBox.Show("Bạn chưa chọn khách hàng");
                return;
            }
            //if (khwu == false)
            //{
            //    XtraMessageBox.Show("Khách hàng không thuộc WU");
            //}
            else
            {
                try
                {
                    cn.openconnection();
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "SELECT khachhang.KHWU FROM khachhang WHERE khachhang.KHID = '"+khid+"'";
                    cmd2.Connection = cn.conn;
                    object kq = cmd2.ExecuteScalar();
                    string op = kq.ToString();
                    int sa = Int16.Parse(op);
                    if(sa == 1)
                    {
                        tt = 1;
                    }
                    else
                    {
                        tt = 0;
                    }
                    frmGDWU f = new frmGDWU(this, khid, cmnd, tenkh,tt);
                    f.ShowDialog();
                    c = false;
                }
                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                finally
                {
                    cn.closeconnection();
                }
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    // ((DXMouseEventArgs)e).Handled = true;
                    GridView view = (GridView)sender;
                    GridHitInfo hi = view.CalcHitInfo(e.Location);
                    //   DataRow row = view.GetDataRow(hi.RowHandle);
                    //   view.GetFocusedValue();*/
                    if (hi.InRow)
                    {
                        view.FocusedRowHandle = hi.RowHandle;
                        c = true;
                        object khid1 = view.GetRowCellValue(view.FocusedRowHandle, "KHID");
                        khid = Int32.Parse(khid1.ToString());
                        try
                        {
                            cn.openconnection();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select KHID ,CMND , TenKH , NgaySinh , IF(GioiTinh = 1, 'Nam', N'Nữ') AS 'GioiTinh' , HoKhauThuongTru ,MatTruocCMND,MatSauCMND ,SDT , TenThuongGoi , case KHWU when 1 then 'Có' else N'Không' end as `KHWU`, GiaoDich , GhiChu, NgayCap from khachhang where KHID = '" + khid + "'";
                            cmd.Connection = cn.conn;
                            MySqlDataReader rd = cmd.ExecuteReader();
                            while (rd.Read())
                            {
                                khid = Int32.Parse(rd["KHID"].ToString());
                                cmnd = rd["CMND"].ToString();
                                tenkh = rd["TenKH"].ToString();
                                ngaysinh = rd["NgaySinh"].ToString();
                                string h = rd["GioiTinh"].ToString();
                                if (h == "Nam")
                                {
                                    gt = true;
                                }
                                else
                                {
                                    gt = false;
                                }
                           //     nguyenquan = rd["NguyenQuan"].ToString();
                                hktt = rd["HoKhauThuongTru"].ToString();
                          //      dt = rd["DanToc"].ToString();
                          //      tg = rd["TonGiao"].ToString();
                                object mt = rd["MatTruocCMND"];
                                object ms = rd["MatSauCMND"];
                                if (Convert.IsDBNull(mt))
                                {
                                    mtcmnd = null;
                                }
                                else
                                {
                                    mtcmnd = (byte[])mt;
                                }
                                if (Convert.IsDBNull(ms))
                                {
                                    mscmnd = null;
                                }
                                else
                                {
                                    mscmnd = (byte[])ms;
                                }
                                sdt = rd["SDT"].ToString();
                                ttg = rd["TenThuongGoi"].ToString();
                                string y = rd["KHWU"].ToString();
                                if (y == "Có")
                                {
                                    khwu = true;
                                }
                                else
                                {
                                    khwu = false;
                                }
                                gd = rd["GiaoDich"].ToString();
                                ghichu = rd["GhiChu"].ToString();
                                string s = rd["NgayCap"].ToString();
                                int r = s.Length;
                                if (r == 0)
                                {
                                    ngaycap = ngaycap2;
                                }
                                else
                                {
                                    s = Left(s, 10);
                                    ngaycap = Convert.ToDateTime(rd["NgayCap"].ToString());
                                }
                            }
                            rd.Close();
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
                       /* object khid1 = view.GetRowCellValue(view.FocusedRowHandle, "KHID");
                        object cmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "CMND");
                        object tenkh1 = view.GetRowCellValue(view.FocusedRowHandle, "TenKH");
                        object ngaysinh1 = view.GetRowCellValue(view.FocusedRowHandle, "NgaySinh");
                        object gioitinh1 = view.GetRowCellValue(view.FocusedRowHandle, "GioiTinh");
                        object nguyenquan1 = view.GetRowCellValue(view.FocusedRowHandle, "NguyenQuan");
                        object hktt1 = view.GetRowCellValue(view.FocusedRowHandle, "HoKhauThuongTru");
                        object dantoc1 = view.GetRowCellValue(view.FocusedRowHandle, "DanToc");
                        object tongiao1 = view.GetRowCellValue(view.FocusedRowHandle, "TonGiao");
                        object mtcmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "MatTruocCMND");
                        object mscmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "MatSauCMND");
                        object sdt1 = view.GetRowCellValue(view.FocusedRowHandle, "SDT");
                        object tentg = view.GetRowCellValue(view.FocusedRowHandle, "TenThuongGoi");
                        object khwu1 = view.GetRowCellValue(view.FocusedRowHandle, "KHWU");
                        object giaodich1 = view.GetRowCellValue(view.FocusedRowHandle, "GiaoDich");
                        object ghichu1 = view.GetRowCellValue(view.FocusedRowHandle, "GhiChu");
                        object ngaycap1 = view.GetRowCellValue(view.FocusedRowHandle, "NgayCap");
                        khid = Int32.Parse(khid1.ToString());
                        cmnd = cmnd1.ToString();
                        tenkh = tenkh1.ToString();
                        ngaysinh = ngaysinh1.ToString();
                        string s = gioitinh1.ToString();
                        if (s == "Nam")
                        {
                            gt = true;
                        }
                        else
                        {
                            gt = false;
                        }
                        nguyenquan = nguyenquan1.ToString();
                        hktt = hktt1.ToString();
                        dt = dantoc1.ToString();
                        tg = tongiao1.ToString();
                        if (Convert.IsDBNull(mtcmnd1))
                        {
                            mtcmnd = null;
                        }
                        else
                        {
                            mtcmnd = (byte[])mtcmnd1;
                        }
                        if (Convert.IsDBNull(mscmnd1))
                        {
                            mscmnd = null;
                        }
                        else
                        {
                            mscmnd = (byte[])mscmnd1;
                        }

                        sdt = sdt1.ToString();
                        ttg = tentg.ToString();
                        string h = khwu1.ToString();
                        if (h == "Có")
                        {
                            khwu = true;
                        }
                        else
                        {
                            khwu = false;
                        }
                        gd = giaodich1.ToString();
                        ghichu = ghichu1.ToString();
                        //string nc = ngaycap1.ToString();
                        //ngaycap = DateTime.Parse(nc);
                        if(ngaycap1.ToString() == "" || ngaycap1 == null)
                        {                            
                            ngaycap = ngaycap2;
                        }
                        else
                        {
                            ngaycap = Convert.ToDateTime(ngaycap1);
                        }                      
                        view.ClearSelection();*/
                    }
                }
                catch(Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {

            if (e.SelectedControl != gvKhachhang) return;

            ToolTipControlInfo info = null;
            //Get the view at the current mouse position
            GridView view = gvKhachhang.GetViewAt(e.ControlMousePosition) as GridView;
            if (view == null) return;
            //Get the view's element information that resides at the current position
            GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
            //Display a hint for row indicator cells
            if (hi.InRowCell && hi.Column.FieldName == "TenKH")
            {
                //An object that uniquely identifies a row indicator cell
                object o = hi.Column.FieldName + hi.RowHandle.ToString();
                object tkh = view.GetRowCellValue(hi.RowHandle, "TenKH");
                object ns = view.GetRowCellValue(hi.RowHandle, "NgaySinh");
                object tk = view.GetRowCellValue(hi.RowHandle, "TenThuongGoi");
                if (tkh != null)
                {
                    string text = "";
                    text += "Tên kh: " + tkh + Environment.NewLine + "Ngày sinh: " + ns + Environment.NewLine + "Tên khác: " + tk;
                    info = new ToolTipControlInfo(o, text);
                }
            }
            //Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
            if (info != null)
                e.Info = info;
            /*  if (e.Info == null && object.ReferenceEquals(e.SelectedControl, gvKhachhang))
                {
                    GridView view = gvKhachhang.FocusedView as GridView;
                    GridHitInfo info = view.CalcHitInfo(e.ControlMousePosition);
                    if (view == null)
                    {
                        return;
                    }
                    if (info.InRowCell)
                    {
                        if (info.Column.Caption == "Tên khách hàng")
                        {
                            string text = "";
                 
                            object khid1 = view.GetRowCellValue(view.FocusedRowHandle, "KHID");
                            object cmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "CMND");
                            object tenkh1 = view.GetRowCellValue(view.FocusedRowHandle, "Tên khách hàng");
                            object ngaysinh1 = view.GetRowCellValue(view.FocusedRowHandle, "Ngày sinh");
                            object gioitinh1 = view.GetRowCellValue(view.FocusedRowHandle, "Giới tính");
                            object nguyenquan1 = view.GetRowCellValue(view.FocusedRowHandle, "Nguyên quán");
                            object hktt1 = view.GetRowCellValue(view.FocusedRowHandle, "Hktt");
                            object dantoc1 = view.GetRowCellValue(view.FocusedRowHandle, "Dân tộc");
                            object tongiao1 = view.GetRowCellValue(view.FocusedRowHandle, "Tôn giáo");
                            object mtcmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "Mặt trước cmnd");
                            object mscmnd1 = view.GetRowCellValue(view.FocusedRowHandle, "Mặt sau cmnd");
                            object sdt1 = view.GetRowCellValue(view.FocusedRowHandle, "Sđt");
                            object tentg = view.GetRowCellValue(view.FocusedRowHandle, "Tên khác");
                            object khwu1 = view.GetRowCellValue(view.FocusedRowHandle, "WU");
                            object giaodich1 = view.GetRowCellValue(view.FocusedRowHandle, "Giao dịch");
                            object ghichu1 = view.GetRowCellValue(view.FocusedRowHandle, "Ghi chú");

                            khid = Int32.Parse(khid1.ToString());
                            cmnd = cmnd1.ToString();
                            tenkh = tenkh1.ToString();
                            ngaysinh = ngaysinh1.ToString();
                            string s = gioitinh1.ToString();
                            if (s == "Nam")
                            {
                                gt = true;
                            }
                            else
                            {
                                gt = false;
                            }
                            nguyenquan = nguyenquan1.ToString();
                            hktt = hktt1.ToString();
                            dt = dantoc1.ToString();
                            tg = tongiao1.ToString();
                            if (Convert.IsDBNull(mtcmnd1))
                            {
                                mtcmnd = null;
                            }
                            else
                            {
                                mtcmnd = (byte[])mtcmnd1;
                            }
                            if (Convert.IsDBNull(mscmnd1))
                            {
                                mscmnd = null;
                            }
                            else
                            {
                                mscmnd = (byte[])mscmnd1;
                            }

                            sdt = sdt1.ToString();
                            ttg = tentg.ToString();
                            string h = khwu1.ToString();
                            if (h == "Có")
                            {
                                khwu = true;
                            }
                            else
                            {
                                khwu = false;
                            }
                            gd = giaodich1.ToString();
                            ghichu = ghichu1.ToString();

                        //    text += "<b>Tên sinh viên: " + tenkh + "</b><br>Ngày sinh: " + ngaysinh + "<br>VB.NET: " + gioitinh1 + "<br>CSHARP: " + nguyenquan + "<br>PHP: " + dt + "<br>ASP.NET: " + tg + "<br>SQLSERVER: " + ghichu;
                            text += "<b> Teenn kh: " + tenkh+ "</b><br>Ngày sinh: "+ngaysinh;
                            string cellKey = info.RowHandle.ToString() + " - " + info.Column.ToString();
                            e.Info = new DevExpress.Utils.ToolTipControlInfo(cellKey, text);
                        }

                    }
                }*/
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (c == false)
            {
                XtraMessageBox.Show("Bạn chưa chọn khách hàng");
                return;
            }
            else
            {
                frmKhachHangSua f = new frmKhachHangSua(this, khid, cmnd, tenkh, ngaysinh, gt, nguyenquan, hktt, dt, tg, mtcmnd, mscmnd, sdt, ttg, khwu, gd, ghichu,ngaycap);
             //   khidsuaload = khid;
                f.ShowDialog();
                c = false;
            }
        }

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //adpter = new MySqlDataAdapter("select KHID ,CMND , TenKH 'Tên khách hàng', NgaySinh 'Ngày sinh', case GioiTinh when 1 then 'Nam' else N'Nữ' end as 'Giới tính', NguyenQuan 'Nguyên quán', HoKhauThuongTru 'Hktt', DanToc 'Dân tộc', TonGiao 'Tôn giáo', MatTruocCMND 'Mặt trước cmnd', MatSauCMND 'Mặt sau cmnd', SDT 'Sđt', TenThuongGoi 'Tên khác', case KHWU when 1 then 'Có' else N'Không' end as 'WU', GiaoDich 'Giao dịch', GhiChu 'Ghi chú' " +
                //                         "from khachhang", cn.conn);
                // ds = new DataSet();
                // adpter.Fill(ds);
                // gvKhachhang.DataSource = ds.Tables[0];
                // gridView1.Columns[0].Visible = false;
                //   gridView1.Columns[2].BestFit = true;

                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT MatTruocCMND,MatSauCMND FROM khachhang WHERE khachhang.KHID = '"+khid+"'";
                cmd.Connection = cn.conn;
                DataTable dt = new DataTable();
                dt.Columns.Add("MatTruocCMND", typeof(byte[]));
                dt.Columns.Add("MatSauCMND", typeof(byte[]));
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                   // DataRow row = dt.NewRow();
                   // row["MatTruocCMND"] = rd["MatTruocCMND"];
                  //  row["MatSauCMND"] = rd["MatSauCMND"];
                 //   dt.Rows.Add(row);
                    if (Convert.IsDBNull(rd["MatTruocCMND"]))
                    {
                        mtcmnd = null;
                    }
                    else
                    {
                        mtcmnd = (byte[])rd["MatTruocCMND"];
                    }
                    if (Convert.IsDBNull(rd["MatSauCMND"]))
                    {
                        mscmnd = null;
                    }
                    else
                    {
                        mscmnd = (byte[])rd["MatSauCMND"];
                    }
                 //   mtcmnd = (byte[])rd["MatTruocCMND"];
                 //   mscmnd = (byte[])rd["MatSauCMND"];
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                frmKhachHangXem f = new frmKhachHangXem(this, khid, cmnd, tenkh, ngaysinh, gt, nguyenquan, hktt, dt, tg, mtcmnd, mscmnd, sdt, ttg, khwu, gd, ghichu,ngaycap);
                f.ShowDialog();
                cn.closeconnection();
                cn.conn.Dispose();
            }           
        }
        
        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            int rowIndex = e.RowHandle;
            if (rowIndex >= 0)
            {
                rowIndex++;
                e.Info.DisplayText = rowIndex.ToString();
            }
        }

        void search()
         {
             string sql = string.Format(@"select KHID ,CMND , TenKH , NgaySinh , IF(GioiTinh = 1, 'Nam', N'Nữ') AS 'GioiTinh' , HoKhauThuongTru ,SDT , TenThuongGoi , case KHWU when 1 then 'Có' else N'Không' end as `KHWU`, GiaoDich , GhiChu, NgayCap from khachhang where TenKH LIKE N'%{0}%' or CMND LIKE N'%{0}%'", textEdit1.Text);
             adpter = new MySqlDataAdapter(sql, cn.conn);
             ds = new DataSet();
             adpter.Fill(ds);
             gvKhachhang.DataSource = ds.Tables[0];
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
           // search();
            loadsearch();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            load();
        }
   
    }
}