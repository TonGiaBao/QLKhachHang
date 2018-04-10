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
using DevExpress.XtraSplashScreen;
using System.Threading;

namespace QLKhachHang
{
    public partial class frmKhachHangAdd : DevExpress.XtraEditors.XtraForm
    {
        Connect cn = new Connect();
        private readonly frmKhachHang frm1;
        bool a = false;
        public frmKhachHangAdd(frmKhachHang frm)
        {
            InitializeComponent();
            frm1 = frm;
        }

        void xoatxt()
        {
            txtCmnd.Text = "";
            txtDantoc.Text = "";
            txtGhichu.Text = "";
            txtGiaodich.Text = "";
            txtHokhau.Text = "";
            txtHoten.Text = "";
            txtNguyenquan.Text = "";
            txtSdt.Text = "";
            txtTenkhac.Text = "";
            txtTongiao.Text = "";
            dtNgaysinh.Text = "";
            ptMatsau.EditValue = null;
            ptMattruoc.EditValue = null;
            ckWu.Checked = false;
            ckNam.Checked = true;
            dtNgaycap.Text = "";
            txtCmnd.Focus();
        }
        private void frmKhachHangAdd_Load(object sender, EventArgs e)
        {
            txtTongiao.Visible = false;
            lblTongiao.Visible = false;
        }
        private void StartForm()
        {
            Application.Run(new frmWait());
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {            
          //  Thread t = new Thread(new ThreadStart(StartForm));
          //  t.Start();
         //   Thread.Sleep(3000);
            try
            {
                if (txtHoten.Text == "")
                {
                    XtraMessageBox.Show("Tên khách hàng không được để rỗng");
                    return;
                }
                else
                {
                    SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
                    a = true;
                    cn.openconnection();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.Text;
                    string sql = "insert into khachhang(CMND,TenKH,NgaySinh,GioiTinh,HoKhauThuongTru,MatTruocCMND,MatSauCMND,SDT,TenThuongGoi,KHWU,GiaoDich,GhiChu,NgayCap) " +
                                             " values(@cmnd,@tenkh,@ngaysinh,@gioitinh,@hokhauthuongtru,@mattruoccmnd,@matsaucmnd,@sdt,@tenthuonggoi,@khwu,@giaodich,@ghichu,@ngaycap) ";
                    cmd.CommandText = sql;
                    cmd.Connection = cn.conn;

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
                        txtHoten.Text.TrimEnd();
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

                  //  cmd.Parameters.Add("@nguyenquan", MySqlDbType.VarChar).Value = txtNguyenquan.Text;
                    cmd.Parameters.Add("@hokhauthuongtru", MySqlDbType.VarChar).Value = txtHokhau.Text;
               //     cmd.Parameters.Add("@dantoc", MySqlDbType.VarChar).Value = txtDantoc.Text;
               //     cmd.Parameters.Add("@tongiao", MySqlDbType.VarChar).Value = txtTongiao.Text;
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
                    if (dtNgaycap.Text == "")
                    {
                        cmd.Parameters.Add("@ngaycap", MySqlDbType.Date).Value = "01/01/0001";
                    }
                    else
                    {
                        cmd.Parameters.Add("@ngaycap", MySqlDbType.Date).Value = dtNgaycap.EditValue;
                    }
                    if (cmd.ExecuteNonQuery() == 1)
                    {                              
                        frm1.frmKhachHang_Load(sender, e);
                      //  frm1.loadmaxid();
                        SplashScreenManager.CloseForm();
                        a = false;
                   //     t.Abort();
                        XtraMessageBox.Show("Đã lưu");
                        xoatxt();
                        frm1.countkh();
                    }
                    else
                    {
                        XtraMessageBox.Show("Lưu thất bại");
                    }
                }
            }
            catch (Exception ex)
            {
                if(a==true)
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
        }

    }
}