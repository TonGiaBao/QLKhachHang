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
    public partial class frmGDWU : DevExpress.XtraEditors.XtraForm
    {
        Connect cn = new Connect();
        private readonly frmKhachHang frm1;
        private readonly frmGDWUAll frm2;
        int tt1;
        int khid1;
        string cmnd1;
        string tekh1;

        public frmGDWU(frmKhachHang frm,int khid,string cmnd,string tenkh,int tt)
        {
            tt1 = tt;
            frm1 = frm;
            khid1 = khid;
            cmnd1 = cmnd;
            tekh1 = tenkh;           
            InitializeComponent();
          //  frm2 = f2;
        }

        public delegate void GoiDuLieuXuLy();

        Thread th_GoiDuLieu;
        public void start_thearing()
        {
            th_GoiDuLieu = new Thread(XuLyGoiDuLieu);
            th_GoiDuLieu.IsBackground = true;
            th_GoiDuLieu.Start();
        }

        bool f;
        public void XuLyGoiDuLieu()
        {
            while (true)
            {
                if (f == true)
                {
                    this.BeginInvoke(new GoiDuLieuXuLy(GoiDuLieu));
                }
                Thread.Sleep(300);
                return;
            }
        }

        public void GoiDuLieu()
        {
            cn.start = true;
        }
        private void loadtxt()
        {
            txtCmnd.Text = cmnd1;
            txtTenkh.Text = tekh1;
            txtDiemchitra.Text = "Tiệm vàng Khải Hằng";
            txtMskh.Focus();
        }
        private void frmGDWU_Load(object sender, EventArgs e)
        {
            loadtxt();            
        }

        void loadbtn()
        {
          //  frm2.btnreload_Click(frm2,null);
        }

        private void frmChild_Closing(object sender, FormClosedEventArgs e)
        {
            frmGDWUAll frm3 = new frmGDWUAll();
            //frm3.frmGDWUAll_Load(sender, e);
          //  frm3.frmChild_Closing(sender, e);
        }
        private void StartForm()
        {
            Application.Run(new frmWait());
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            frmKhachHang f = new frmKhachHang();
            try
            {
              //  Thread t = new Thread(new ThreadStart(StartForm));
             //   t.Start();                
                if(txtTongtien.Text == "")
                {
                    XtraMessageBox.Show("Bạn chưa nhập số tiền");
                    return;
                }
                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                string sql = "insert into giaodich(khachhang_KHID,NgayGD,GioGD,SoLanGD,TongTien,GiaoDichWU,MSNT,QuocGiaGui,DiemChiTra,NgayGiaoDich,IsXoa) " +
                                         " values(@khid,@ngaygd,@giogd,@solangd,@tongtien,@giaodichwu,@msnt,@quocgiagui,@diemchitra,@ngaygiaodich,@isxoa) ";
                cmd.CommandText = sql;
                cmd.Connection = cn.conn;
                cmd.Parameters.Add("@khid", MySqlDbType.Int32).Value = khid1;
                cmd.Parameters.Add("@ngaygd", MySqlDbType.Datetime).Value = DateTime.Now;
                cmd.Parameters.Add("@giogd", MySqlDbType.VarChar).Value = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
                cmd.Parameters.Add("@solangd", MySqlDbType.Int32).Value = 1;
                cmd.Parameters.Add("@tongtien", MySqlDbType.VarChar).Value = txtTongtien.Text;
                if (checkEdit1.Checked == true)
                    cmd.Parameters.Add("@giaodichwu", MySqlDbType.Bit).Value = 1;
                else
                    cmd.Parameters.Add("@giaodichwu", MySqlDbType.Bit).Value = 0;
                cmd.Parameters.Add("@msnt", MySqlDbType.VarChar).Value = txtMskh.Text;
                cmd.Parameters.Add("@quocgiagui", MySqlDbType.VarChar).Value = txtQuocgia.Text;
                cmd.Parameters.Add("@diemchitra", MySqlDbType.VarChar).Value = txtDiemchitra.Text;
                cmd.Parameters.Add("@ngaygiaodich", MySqlDbType.Datetime).Value = dtNgaygiaodich.EditValue;
                cmd.Parameters.Add("@isxoa", MySqlDbType.Bit).Value = 1;
                if (checkEdit1.Checked == true)
                {
                    if(tt1 == 0)
                    {
                        XtraMessageBox.Show("Không phải khách hàng WU");
                        return;
                    }
                }
                if (cmd.ExecuteNonQuery() == 1)
                {
                    try
                    {
                   //     t.Abort();
                        XtraMessageBox.Show("Giao dịch thành công");
                        this.FormClosed += frmChild_Closing;
                        
                    }
                    catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                /*    frm2.start_thearing();
                    f = true;
                    start_thearing();*/
                }
                else
                {
                    XtraMessageBox.Show("Giao dịch thất bại");
                }
            }
            catch(Exception ex)
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
}