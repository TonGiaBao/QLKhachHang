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
using System.Threading;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using System.Collections;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;
using DevExpress.LookAndFeel;
using System.Drawing.Printing;
using System.Web.UI.WebControls;
using DevExpress.XtraGrid.Views.Grid;

namespace QLKhachHang
{
    public partial class frmGDWUAll : DevExpress.XtraEditors.XtraUserControl
    {
        Connect cn = new Connect();
        MySqlDataAdapter adpter = null;
        MySqlDataReader rd2;
        DataSet ds = new DataSet();
        private readonly frmGDWU f;
        int numberrow;
        int vt = -1;
        bool c = false;
        int idgd;
        int khid;
        DateTime ngaygd;
        string giogd;
        int slgd;
        string tongtien;
        bool gdwu;
        string msnt;
        string quocgia;
        string diemchitra;
        DateTime ngaygiaodich;
        bool isxoa;
        bool rowclick = false;
        public frmGDWUAll()
        {
            InitializeComponent();
        }

        public void loadgv()
        {
            try
            {
                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT a.CMND , a.TenKH , b.NgayGD , b.GioGD , b.TongTien  FROM khachhang a, giaodich b where a.KHID = b.khachhang_KHID";
                cmd.Connection = cn.conn;
                DataTable dt = new DataTable();
                dt.Columns.Add("CMND");
                dt.Columns.Add("TenKH");
                dt.Columns.Add("NgayGD");
                dt.Columns.Add("GioGD");
                dt.Columns.Add("TongTien");
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DataRow row = dt.NewRow();
                    row["CMND"] = rd["CMND"];
                    row["TenKH"] = rd["TenKH"];
                    row["NgayGD"] = rd["NgayGD"];
                    row["GioGD"] = rd["GioGD"];
                    row["TongTien"] = rd["TongTien"];
                    dt.Rows.Add(row);
                }
                rd.Close();
                gvGdwu.DataSource = dt;
                
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
        public void frmGDWUAll_Load(object sender, EventArgs e)
        {
            //gridColumn10.BestFit();
            //gridColumn5.BestFit();
        }
        private void StartForm()
        {
            Application.Run(new frmWait());
        }
        bool a = false;
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
        int nb = 1;
        string text = "";
        private void btnSearch_Click(object sender, EventArgs e)
        {           
         /*   gvGdwu.DataSource = null;
            adpter = new MySqlDataAdapter("SELECT a.KHID,a.CMND, a.TenKH, b.NgayGD, b.GioGD, b.TongTien FROM ql.khachhang a, ql.giaodich b where a.KHID = b.khachhang_KHID and b.NgayGD between '" + dtNgaybatdau.Text + "' and '" + dtNgayketthuc.Text + "'", cn.conn);
            ds = new DataSet();
            adpter.Fill(ds);
            gvGdwu.DataSource = ds.Tables[0];
            string text = "";
            text += "Giao dịch WU từ ngày: " + dtNgaybatdau.Text + " đến ngày: " + dtNgayketthuc.Text;
            gridView1.ViewCaption = text;*/
            try
            {
                nb = 1;
                gvGdwu.DataSource = null;
                SplashScreenManager.ShowForm(this, typeof(frmWait2), true, true);
                a = true;
                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
              //  cmd.CommandText = "SELECT a.KHID,a.CMND, a.TenKH, b.NgayGD, b.GioGD, b.TongTien,b.QuocGiaGui,b.DiemChiTra,IF(b.GiaoDichWU = 1, 'Có', N'Không') AS 'GiaoDichWU',b.MSNT FROM khachhang a, giaodich b where a.KHID = b.khachhang_KHID and b.NgayGD between '" + dtNgaybatdau.Text + "' and '" + dtNgayketthuc.Text + "'";
              //  cmd.CommandText = "SELECT a.KHID,a.CMND, a.TenKH, b.NgayGD, b.GioGD, b.TongTien,b.QuocGiaGui,b.DiemChiTra,IF(b.GiaoDichWU = 1, 'Có', N'Không') AS 'GiaoDichWU',b.MSNT FROM khachhang a, giaodich b where a.KHID = b.khachhang_KHID and b.NgayGD >= STR_TO_DATE('" + dtNgaybatdau.EditValue + "','%d/%m/%Y %H:%i:%s') and b.NgayGD <= STR_TO_DATE('" + dtNgayketthuc.EditValue + "','%d/%m/%Y %H:%i:%s') ORDER BY b.NgayGD DESC";
                cmd.CommandText = "SELECT b.idGiaoDich,a.KHID,a.CMND, a.TenKH, b.NgayGD, b.GioGD,b.NgayGiaoDich, b.TongTien,b.QuocGiaGui,b.DiemChiTra,IF(b.GiaoDichWU = 1, 'Có', N'Không') AS 'GiaoDichWU',b.MSNT FROM khachhang a, giaodich b where b.IsXoa = 1 and a.KHID = b.khachhang_KHID and b.NgayGiaoDich >= STR_TO_DATE('" + dtNgaybatdau.EditValue + "','%d/%m/%Y %H:%i:%s') and b.NgayGiaoDich <= STR_TO_DATE('" + dtNgayketthuc.EditValue + "','%d/%m/%Y %H:%i:%s') ORDER BY b.NgayGiaoDich DESC";
                cmd.Connection = cn.conn;
                DataTable dt = new DataTable();
                dt.Columns.Add("idGiaoDich");
                dt.Columns.Add("STT");
                dt.Columns.Add("KHID");
                dt.Columns.Add("CMND");
                dt.Columns.Add("TenKH");
                dt.Columns.Add("NgayGD");
                dt.Columns.Add("GioGD");
                dt.Columns.Add("NgayGiaoDich");
                dt.Columns.Add("TongTien");
                dt.Columns.Add("QuocGiaGui");
                dt.Columns.Add("DiemChiTra");
                dt.Columns.Add("GiaoDichWU");
                dt.Columns.Add("MSNT");

                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DataRow row = dt.NewRow();
                    row["idGiaoDich"] = rd["idGiaoDich"];
                    row["STT"] = nb;
                    nb = nb + 1;
                    row["KHID"] = rd["KHID"];
                    row["CMND"] = rd["CMND"];
                    row["TenKH"] = rd["TenKH"];
                    string s = rd["NgayGD"].ToString();
                    int r = s.Length;
                    s = Left(s, 10);
                    row["NgayGD"] = s;
                    row["GioGD"] = rd["GioGD"];
                    row["NgayGiaoDich"] = rd["NgayGiaoDich"];
                    row["TongTien"] = rd["TongTien"];
                    row["QuocGiaGui"] = rd["QuocGiaGui"];
                    row["DiemChiTra"] = rd["DiemChiTra"];
                    row["GiaoDichWU"] = rd["GiaoDichWU"];
                    row["MSNT"] = rd["MSNT"];
                    dt.Rows.Add(row);
                }

                rd.Close();
                gvGdwu.DataSource = dt;
          //      gridView1.Columns[0].Visible = false;
                
                text = "Giao dịch từ ngày: " + dtNgaybatdau.EditValue + " đến ngày: " + dtNgayketthuc.EditValue;
                
                gridView1.ViewCaption = text;
                SplashScreenManager.CloseForm();
                a = false;
                //gridColumn10.BestFit();
                //gridColumn5.BestFit();
                //gridColumn9.BestFit();
                textEdit1.Text = "";
              /*  gridView1.GroupSummary.Clear();
                GridSummaryItem summaryItemMaxOrderSum = gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "TongTien", null, "(Tổng tiền giao dịch: {0:c0})");
                GridSummaryItem summaryItemMaxOrderSum1 = gridView1.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, null, null, "Số lần giao dịch: {0}");*/
          //      t.Abort();
                
                //  gridColumn3.BestFit();
            }
            catch (Exception ex) 
            {
                if (a == true)
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

        private void link_CreateReportHeaderArea(object sender,CreateAreaEventArgs e)
        {
            try
            {
                string reportHeader = "Categories Report";
                e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
                e.Graph.Font = new Font("Tahoma", 14, FontStyle.Bold);
                RectangleF rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
                e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        
        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                /*GridColumn unboundColumn = new GridColumn();
                unboundColumn.FieldName = "stt";
                unboundColumn.UnboundType = DevExpress.Data.UnboundColumnType.Integer;

                gridView1.Columns.Add(unboundColumn);
                unboundColumn.VisibleIndex = 0;
                gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;*/
                
                gridColumn10.Visible = false;
                gridColumn11.Visible = false;
                gridView1.OptionsView.ShowViewCaption = false;
                gridView1.BestFitColumns();
                string leftColumn = "";
                string middleColumn = "TIỆM VÀNG KHẢI HẰNG" + Environment.NewLine + text;
                //   text += "Tên kh: " + tkh + Environment.NewLine + "Ngày sinh: " + ns + Environment.NewLine + "Tên khác: " + tk;
                string rightColumn = "";

                string leftfooter = "";
                string middlefooter = "";
                string rightFooter = "Trang [Page #]";
                PrintableComponentLink pcl = new PrintableComponentLink(new PrintingSystem());
                pcl.Component = gvGdwu;
                

                PageHeaderFooter phf = pcl.PageHeaderFooter as PageHeaderFooter;

                // Clear the PageHeaderFooter's contents.
                phf.Header.Content.Clear();
                phf.Footer.Content.Clear();
                phf.Header.Font = new Font("Times New Roman", 13, FontStyle.Bold);
                // Add custom information to the link's header.
                //  phf.Header.Content.AddRange(new string[] { string.Empty, "TITLE", "[Date Printed] [Time Printed]" });
                phf.Header.Content.AddRange(new string[] { leftColumn, middleColumn, rightColumn });
                phf.Header.LineAlignment = BrickAlignment.Far;
                phf.Footer.Content.AddRange(new string[] { leftfooter, middlefooter, rightFooter });


                //pcl.CreateDocument();
                //   pcl.ShowPreviewDialog();
                pcl.ShowRibbonPreviewDialog(gvGdwu.LookAndFeel);
                gridView1.OptionsView.ShowViewCaption = true;
                gridColumn11.Visible = true;
                //gridView1.Columns.Remove(unboundColumn);
                //gridView1.CustomUnboundColumnData -= gridView1_CustomUnboundColumnData;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

      /*  private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            int rowIndex = e.RowHandle;
            if (rowIndex >= 0)
            {
                rowIndex++;
                e.Info.DisplayText = rowIndex.ToString();
                
            }         
        }*/

   /*     private void gridView1_CustomDrawFilterPanel(object sender, CustomDrawObjectEventArgs e)
        {
            GridViewRow row2;
            gvGdwu.ForceInitialize();
            int count = gridView1.DataRowCount;
            DataTable dt = new DataTable();
            dt.Columns.Add("STT");
            for(int i= 1;i<=count;i++)
            {           
                DataRow row = dt.NewRow();
                row["STT"] = i;
                gridView1.GetRowCellValue(i, gridColumn10);
                gridView1.SetRowCellValue(i, gridColumn10, i);

            }
        }*/
        private void searchkey()
        {
            try
            {
                nb = 1;
                gvGdwu.DataSource = null;
                cn.openconnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                //  cmd.CommandText = "SELECT a.KHID,a.CMND, a.TenKH, b.NgayGD, b.GioGD, b.NgayGiaoDich, b.TongTien,b.QuocGiaGui,b.DiemChiTra,IF(b.GiaoDichWU = 1, 'Có', N'Không') AS 'GiaoDichWU',b.MSNT FROM khachhang a, giaodich b where a.KHID = b.khachhang_KHID and b.NgayGiaoDich >= STR_TO_DATE('" + dtNgaybatdau.EditValue + "','%d/%m/%Y %H:%i:%s') and b.NgayGiaoDich <= STR_TO_DATE('" + dtNgayketthuc.EditValue + "','%d/%m/%Y %H:%i:%s') and b.GiaoDichWU = '"+textEdit1.Text+"' or a.TenKH = '"+textEdit1.Text+"' ORDER BY b.NgayGiaoDich DESC";
                cmd.CommandText = "SELECT b.idGiaoDich,a.KHID,a.CMND, a.TenKH, b.NgayGD, b.GioGD,b.NgayGiaoDich, b.TongTien,b.QuocGiaGui,b.DiemChiTra,IF(b.GiaoDichWU = 1, 'Có', N'Không') AS 'GiaoDichWU',b.MSNT FROM khachhang a, giaodich b where b.IsXoa = 1 and a.KHID = b.khachhang_KHID and b.NgayGiaoDich >= STR_TO_DATE('" + dtNgaybatdau.EditValue + "','%d/%m/%Y %H:%i:%s') and b.NgayGiaoDich <= STR_TO_DATE('" + dtNgayketthuc.EditValue + "','%d/%m/%Y %H:%i:%s') ORDER BY b.NgayGiaoDich DESC";
                //       cmd.CommandText = "SELECT b.idGiaoDich,a.KHID,a.CMND, a.TenKH, b.NgayGD, b.GioGD,b.NgayGiaoDich, b.TongTien,b.QuocGiaGui,b.DiemChiTra,IF(b.GiaoDichWU = 1, 'Có', N'Không') AS 'GiaoDichWU',b.MSNT FROM khachhang a, giaodich b where b.IsXoa = 1 and a.KHID = b.khachhang_KHID and b.NgayGiaoDich >= STR_TO_DATE('" + dtNgaybatdau.EditValue + "','%d/%m/%Y %H:%i:%s') and b.NgayGiaoDich <= STR_TO_DATE('" + dtNgayketthuc.EditValue + "','%d/%m/%Y %H:%i:%s') ORDER BY b.NgayGiaoDich DESC";
                cmd.Connection = cn.conn;
                DataTable dt = new DataTable();
                dt.Columns.Add("STT");
                dt.Columns.Add("KHID");
                dt.Columns.Add("CMND");
                dt.Columns.Add("TenKH");
                dt.Columns.Add("NgayGD");
                dt.Columns.Add("GioGD");
                dt.Columns.Add("NgayGiaoDich");
                dt.Columns.Add("TongTien");
                dt.Columns.Add("QuocGiaGui");
                dt.Columns.Add("DiemChiTra");
                dt.Columns.Add("GiaoDichWU");
                dt.Columns.Add("MSNT");

                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DataRow row = dt.NewRow();
                    row["STT"] = nb;
                    nb = nb + 1;
                    row["KHID"] = rd["KHID"];
                    row["CMND"] = rd["CMND"];
                    row["TenKH"] = rd["TenKH"];
                    string s = rd["NgayGD"].ToString();
                    int r = s.Length;
                    s = Left(s, 10);
                    row["NgayGD"] = s;
                    row["GioGD"] = rd["GioGD"];
                    row["NgayGiaoDich"] = rd["NgayGiaoDich"];
                    row["TongTien"] = rd["TongTien"];
                    row["QuocGiaGui"] = rd["QuocGiaGui"];
                    row["DiemChiTra"] = rd["DiemChiTra"];
                    row["GiaoDichWU"] = rd["GiaoDichWU"];
                    row["MSNT"] = rd["MSNT"];
                    dt.Rows.Add(row);
                }

                rd.Close();
                gvGdwu.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                string text = "";
                text += "Giao dịch từ ngày: " + dtNgaybatdau.EditValue + " đến ngày: " + dtNgayketthuc.EditValue;
                gridView1.ViewCaption = text;
            }
            catch (Exception ex)
            {
                if (a == true)
                {

                }
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                cn.closeconnection();
                cn.conn.Dispose();
            }
        }
        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            searchkey();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == XtraMessageBox.Show("Bạn có muốn xóa? ", "Thông báo", MessageBoxButtons.YesNo))
                {
                    cn.openconnection();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.Text;
                    string sql = "update giaodich set IsXoa = 0 where khachhang_KHID=@khid and idGiaoDich = @idgiaodich";

                    cmd.CommandText = sql;
                    cmd.Connection = cn.conn;
                    cmd.Parameters.Add("@khid", MySqlDbType.Int32).Value = khid;
                    cmd.Parameters.Add("@idgiaodich", MySqlDbType.Int32).Value = idgd;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        XtraMessageBox.Show("Đã xóa");
                        btnSearch_Click(sender, e);
                    }
                    else
                    {
                        XtraMessageBox.Show("Xóa thất bại");
                    }
                }
            }
            catch { }
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                    if (view.FocusedRowHandle == vt)
                    {
                        return;
                    }
                    else
                    {
                        c = true;
                        object idgd1 = view.GetRowCellValue(view.FocusedRowHandle, "idGiaoDich");
                        object khid1 = view.GetRowCellValue(view.FocusedRowHandle, "KHID");
                        object ngaygd1 = view.GetRowCellValue(view.FocusedRowHandle, "NgayGD");
                        object giogd1 = view.GetRowCellValue(view.FocusedRowHandle, "GioGD");
                        //    object slgd = view.GetRowCellValue(view.FocusedRowHandle, "GioiTinh");
                        object tongtien1 = view.GetRowCellValue(view.FocusedRowHandle, "TongTien");
                        object gdwu1 = view.GetRowCellValue(view.FocusedRowHandle, "GiaoDichWU");
                        object msnt1 = view.GetRowCellValue(view.FocusedRowHandle, "MSNT");
                        object quocgia1 = view.GetRowCellValue(view.FocusedRowHandle, "QuocGiaGui");
                        object diemchitra1 = view.GetRowCellValue(view.FocusedRowHandle, "DiemChiTra");
                        object ngaygiaodich1 = view.GetRowCellValue(view.FocusedRowHandle, "NgayGiaoDich");
                        object isxoa1 = view.GetRowCellValue(view.FocusedRowHandle, "IsXoa");

                        idgd = Int32.Parse(idgd1.ToString());
                        khid = Int32.Parse(khid1.ToString());
                        ngaygd = Convert.ToDateTime(ngaygd1);
                        tongtien = tongtien1.ToString();
                        string s = gdwu1.ToString();
                        if (s == "Có")
                        {
                            gdwu = true;
                        }
                        else
                        {
                            gdwu = false;
                        }
                        msnt = msnt1.ToString();
                        quocgia = quocgia1.ToString();
                        diemchitra = diemchitra1.ToString();
                        ngaygiaodich = Convert.ToDateTime(ngaygiaodich1);

                    }

                }
                catch { }
            
        }

       /* private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "gridColumn10")
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
                
            }
        }*/
        bool aaa = false;
        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            GridColumn unboundColumn = new GridColumn();
            if(aaa == false)
            {
                gridColumn10.Visible = false;
                unboundColumn.FieldName = "Stt";
                unboundColumn.MaxWidth = 40;
                unboundColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
                gridView1.Columns.Add(unboundColumn);
                unboundColumn.VisibleIndex = 0;
                gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

                //gridView1.Columns.Remove(unboundColumn);
                //gridView1.CustomUnboundColumnData -= gridView1_CustomUnboundColumnData;
                aaa = true;
            }
            else
            {
                unboundColumn.MaxWidth = 40;
                gridColumn10.Visible = false;
                //gridView1.Columns.Add(unboundColumn);
                unboundColumn.VisibleIndex = 0;
                gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;

                //gridView1.Columns.Remove(unboundColumn);
                //gridView1.CustomUnboundColumnData -= gridView1_CustomUnboundColumnData;
                aaa = true;
            }
            
            /*nb = 1;
            int count = gridView1.DataRowCount;
            for (int i = 1; i <= gridView1.DataRowCount; i++)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("STT", typeof(int));
                dt.Rows.Add(i);
                //gridView1.SetRowCellValue(i, "STT", i);
            }*/
        }

        private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            e.Value = gridView1.GetRowHandle(e.ListSourceRowIndex) + 1;
        }
        
    /*    private void button1_Click(object sender, EventArgs e)
        {
            int groupRow = gridView1.FocusedRowHandle;
            if (gridView1.FocusedRowHandle > 0)
                groupRow = gridView1.GetParentRowHandle(gridView1.FocusedRowHandle);
            decimal val1 = Convert.ToDecimal(gridView1.GetGroupSummaryValue(groupRow, gridView1.GroupSummary[1] as GridGroupSummaryItem));
        }*/
      
    }
}