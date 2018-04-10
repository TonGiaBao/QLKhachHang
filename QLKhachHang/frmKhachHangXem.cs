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
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using DevExpress.XtraSplashScreen;


namespace QLKhachHang
{
    public partial class frmKhachHangXem : DevExpress.XtraEditors.XtraForm
    {
        Connect cn = new Connect();
        private readonly frmKhachHang frm1;
        int khid1;
        string cmnd1;
        string hoten1;
        string ngaysinh1;
        bool gt1;
        string nguyenquan1;
        string hktt1;
        string dt1;
        string tg1;
        byte[] mtcmnd1;
        byte[] mscmnd1;
        string sdt1;
        string ttg1;
        bool khwu1;
        string gd1;
        string ghichu1;
        DateTime ngaycap1;

        public frmKhachHangXem(frmKhachHang frm, int khid, string cmnd, string hoten, string ngaysinh, bool gioitinh, string nguyenquan, string hktt, string dt, string tg, byte[] mtcmnd, byte[] mscmnd, string sdt, string ttg, bool khwu, string gd, string ghichu,DateTime ngaycap)
        {
            try
            {
                khid1 = khid;
                cmnd1 = cmnd;
                hoten1 = hoten;
                ngaysinh1 = ngaysinh;
                gt1 = gioitinh;
                nguyenquan1 = nguyenquan;
                hktt1 = hktt;
                dt1 = dt;
                tg1 = tg;
                mtcmnd1 = mtcmnd;
                mscmnd1 = mscmnd;
                sdt1 = sdt;
                ttg1 = ttg;
                khwu1 = khwu;
                gd1 = gd;
                ghichu1 = ghichu;
                ngaycap1 = ngaycap;
                frm1 = frm;
                InitializeComponent();
              //  mtcmnd1 = mtcmnd;            
            }
            catch { }
        }

        static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        public BitmapImage ToImage(byte[] array)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new System.IO.MemoryStream(array);
            image.EndInit();
            return image;
        }

        private void loadcmnd()
        {
            try
            {
                if(mtcmnd1 == null)
                {
                    mtcmnd1 = null;
                }
                else
                {
                    byte[] img = mtcmnd1;
                    MemoryStream mt = new MemoryStream(img);
                    ptMattruoc.Image = Image.FromStream(mt);
                }
                if(mscmnd1 == null)
                {
                    mscmnd1 = null;
                }
                else
                {
                    byte[] img2 = mscmnd1;
                    MemoryStream ms = new MemoryStream(img2);
                    ptMatsau.Image = Image.FromStream(ms);
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void loadsua()
        {
            txtCmnd.Text = cmnd1;
            txtHoten.Text = hoten1;
            dtNgaysinh.Text = ngaysinh1;
            if(gt1 == true)
            {
                ckNam.Checked = true;
            }
            else
            {
                ckNu.Checked = true;
            }
            txtNguyenquan.Text = nguyenquan1;
            txtHokhau.Text = hktt1;
            txtDantoc.Text = dt1;
            txtTongiao.Text = tg1;
            txtSdt.Text = sdt1;
            txtTenkhac.Text = ttg1;
            if(khwu1 == true)
            {
                ckWu.Checked = true;
            }
            else
            {
                ckWu.Checked = false;
            }
            txtGiaodich.Text = gd1;
            txtGhichu.Text = ghichu1;
            if(ngaycap1.ToString() == "01/01/0001 0:00:00 AM")
            {
                dtNgaycap.EditValue = "";
            }
            else
            {
                dtNgaycap.EditValue = ngaycap1;
            }            
        }
        private void frmKhachHangSua_Load(object sender, EventArgs e)
        {
            loadcmnd();
            loadsua();
           // ptMattruoc.Image = LoadImage(mtcmnd1);
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
            try
            {
                if (txtHoten.Text == "")
                {
                    XtraMessageBox.Show("Tên khách hàng không được để rỗng");
                    return;
                }
                else
                {
                    cn.openconnection();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.Text;
                    string sql = "update khachhang set CMND=@cmnd,TenKH=@tenkh,NgaySinh=@ngaysinh,GioiTinh=@gioitinh,NguyenQuan=@nguyenquan,HoKhauThuongTru=@hokhauthuongtru,DanToc=@dantoc,TonGiao=@tongiao,MatTruocCMND=@mattruoccmnd,MatSauCMND=@matsaucmnd,SDT=@sdt,TenThuongGoi=@tenthuonggoi,KHWU=@khwu,GiaoDich=@giaodich,GhiChu=@ghichu where KHID=@khid";
                                             
                    cmd.CommandText = sql;
                    cmd.Connection = cn.conn;
                    cmd.Parameters.Add("@khid", MySqlDbType.Int32).Value = khid1;
                    cmd.Parameters.Add("@cmnd", MySqlDbType.VarChar).Value = txtCmnd.Text;

                    string s = txtHoten.Text.Trim();
                    string[] arr = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    txtHoten.Text = "";

                    for (int i = 0; i < arr.Length; i++)
                    {
                        string word = arr[i];
                        word = word.ToLower();
                        char[] arrWord = word.ToCharArray();
                        arrWord[0] = char.ToUpper(arrWord[0]);
                        string newword = new string(arrWord);
                        txtHoten.Text += newword + " ";
                    }

                    cmd.Parameters.Add("@tenkh", MySqlDbType.VarChar).Value = txtHoten.Text;



                    cmd.Parameters.Add("@ngaysinh", MySqlDbType.VarChar).Value = dtNgaysinh.Text;
                    if (ckNam.Checked == true)
                    {
                        // ckNu.CheckState = unchecked;
                        cmd.Parameters.Add("@gioitinh", MySqlDbType.Bit).Value = 1;
                    }
                    else
                        cmd.Parameters.Add("@gioitinh", MySqlDbType.Bit).Value = 0;

                    cmd.Parameters.Add("@nguyenquan", MySqlDbType.VarChar).Value = txtNguyenquan.Text;
                    cmd.Parameters.Add("@hokhauthuongtru", MySqlDbType.VarChar).Value = txtHokhau.Text;
                    cmd.Parameters.Add("@dantoc", MySqlDbType.VarChar).Value = txtDantoc.Text;
                    cmd.Parameters.Add("@tongiao", MySqlDbType.VarChar).Value = txtTongiao.Text;
                    byte[] img3 = null;
                    byte[] img4 = null;
                    try
                    {
                        if(ptMattruoc.Image == null)
                        {
                            ptMattruoc.Image = null;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            ptMattruoc.Image.Save(ms, ptMattruoc.Image.RawFormat);
                            byte[] img1 = ms.ToArray();
                            img3 = img1;
                        }
                        
                    }
                    catch { }

                    try
                    {
                        if(ptMatsau.Image == null)
                        {
                            ptMatsau.Image = null;
                        }
                        else
                        {
                            MemoryStream ms2 = new MemoryStream();
                            ptMatsau.Image.Save(ms2, ptMatsau.Image.RawFormat);
                            byte[] img2 = ms2.ToArray();
                            img4 = img2;
                        }

                    }
                    catch { }


                    cmd.Parameters.Add("mattruoccmnd", MySqlDbType.MediumBlob).Value = img3;
                    cmd.Parameters.Add("matsaucmnd", MySqlDbType.MediumBlob).Value = img4;
                    cmd.Parameters.Add("sdt", MySqlDbType.VarChar).Value = txtSdt.Text;
                    cmd.Parameters.Add("tenthuonggoi", MySqlDbType.VarChar).Value = txtTenkhac.Text;
                    if (ckWu.Checked == true)
                        cmd.Parameters.Add("@khwu", MySqlDbType.Bit).Value = 1;
                    else
                        cmd.Parameters.Add("@khwu", MySqlDbType.Bit).Value = 0;
                    cmd.Parameters.Add("giaodich", MySqlDbType.VarChar).Value = txtGiaodich.Text;
                    cmd.Parameters.Add("ghichu", MySqlDbType.VarChar).Value = txtGhichu.Text;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        
                        frm1.frmKhachHang_Load(sender, e);
                        SplashScreenManager.CloseForm();
                        XtraMessageBox.Show("Đã lưu");                  
                        // frmtest_Load(sender, e);
                    }
                    else
                    {
                        XtraMessageBox.Show("Lưu thất bại");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }   
    }
}