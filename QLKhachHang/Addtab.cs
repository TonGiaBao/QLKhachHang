using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKhachHang
{
    class Addtab
    {
        public void AddtabControl(XtraTabControl xtratabparent, string xtraItemNew, UserControl f)
        {
            XtraTabPage xtratabpage = new XtraTabPage();
            xtratabpage.Name = "Test";
            xtratabpage.Text = xtraItemNew;
            //        xtratabpage.Image = Bitmap.FromFile()
            //      xtratabpage.Dock = DockStyle.Fill;
            f.Dock = DockStyle.Fill;
            xtratabpage.Controls.Add(f);
            xtratabparent.TabPages.Add(xtratabpage);

        }
    }
}
