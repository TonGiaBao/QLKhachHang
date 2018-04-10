using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSplashScreen;

namespace QLKhachHang
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SplashScreenManager splash = new SplashScreenManager();
        bool statesplash = false;
        private void StartForm()
        {
            Application.Run(new frmWait());
        }
        public frmMain()
        {
            try
            {
                if (!splash.IsSplashFormVisible)
                {
                    //  splashScreenManager.ShowWaitForm();
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true);
                }
                statesplash = true;
                InitializeComponent();

                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
                ////       t.Abort();
                DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(skinRibbonGalleryBarItem1, true, true);
                skinRibbonGalleryBarItem1.GalleryItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(skinRibbonGalleryBarItem1_GalleryItemClick);
            }
            catch 
            {
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
                //   Control.CheckForIllegalCrossThreadCalls = false;
        }
        
        private void AddTabControl(UserControl usercontrol, string itemTabname)
        {
            bool kq = false;
            foreach (XtraTabPage tabitem in xtraTabControl1.TabPages)
            {
                if (tabitem.Text == itemTabname)
                {
                    kq = true;
                    xtraTabControl1.SelectedTabPage = tabitem;
                }
            }
            if (kq == false)
            {
                Addtab addtab = new Addtab();
                addtab.AddtabControl(xtraTabControl1, itemTabname, usercontrol);
            }
        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //frmKhachHang f = new frmKhachHang();
                //AddTabControl(f, "Khách hàng");
                if (!splash.IsSplashFormVisible)
                {
                    //  splashScreenManager.ShowWaitForm();
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true);
                }
                statesplash = true;
                //     Thread t = new Thread(new ThreadStart(StartForm));
                //     t.Start();
                frmKhachHang f = new frmKhachHang();
                AddTabControl(f, "Khách hàng");
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
            catch
            {
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
         //   t.Abort();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!splash.IsSplashFormVisible)
                {
                    //  splashScreenManager.ShowWaitForm();
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true);
                }
                statesplash = true;
                //      Thread t = new Thread(new ThreadStart(StartForm));
                //      t.Start();
                frmGDWUAll f = new frmGDWUAll();
                AddTabControl(f, "Giao dịch");
                //      t.Abort();
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
            catch
            {
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
        }

        private void xtraTabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 1;
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            xtraTabControl1.TabPages.RemoveAt(xtraTabControl1.SelectedTabPageIndex);
            xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 1;
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!splash.IsSplashFormVisible)
                {
                    //  splashScreenManager.ShowWaitForm();
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true);
                }
                statesplash = true;
                //      Thread t = new Thread(new ThreadStart(StartForm));
                //       t.Start();
                frmTangQua f = new frmTangQua();
                AddTabControl(f, "Tặng quà");
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
            catch
            {
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
        //    t.Abort();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!splash.IsSplashFormVisible)
                {
                    //  splashScreenManager.ShowWaitForm();
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true);
                }
                statesplash = true;
                //     Thread t = new Thread(new ThreadStart(StartForm));
                //     t.Start();
                frmTangQuaKH f = new frmTangQuaKH();
                AddTabControl(f, "Tặng quà khách hàng");
                //     t.Abort();
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
            catch
            {
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!splash.IsSplashFormVisible)
                {
                    //  splashScreenManager.ShowWaitForm();
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true);
                }
                statesplash = true;
                //      Thread t = new Thread(new ThreadStart(StartForm));
                //       t.Start();
                frmDsKhNhanQua f = new frmDsKhNhanQua();
                AddTabControl(f, "Ds khách hàng đã nhận quà");
                //     t.Abort();
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
            catch
            {
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!splash.IsSplashFormVisible)
                {
                    //  splashScreenManager.ShowWaitForm();
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true);
                }
                statesplash = true;
                //     Thread t = new Thread(new ThreadStart(StartForm));
                //    t.Start();
                frmDsKhChuaTang f = new frmDsKhChuaTang();
                AddTabControl(f, "Ds khách hàng chưa nhận quà");
                //     t.Abort();
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
            catch
            {
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }
                
            }
        }

        string FileName = "UserSettings.txt";
        private void SaveSettings(UserSettings us)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, us);
                fStream.Close();
            }
        }

        private void skinRibbonGalleryBarItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserSettings us = new UserSettings();
            us.SkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName;
            SaveSettings(us);
        }
        private void skinRibbonGalleryBarItem1_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
        {
            UserSettings us = new UserSettings();
            us.SkinName = DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName;
            SaveSettings(us);
        }

        [Serializable]
        public class UserSettings
        {
            public string SkinName;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(FileName))
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(LoadSettings(FileName).SkinName);
        }

        private UserSettings LoadSettings(string fileName)
        {
            UserSettings us = null;
            BinaryFormatter binFormat = new BinaryFormatter();
            Stream fStream = new FileStream(fileName, FileMode.Open);
            try { us = binFormat.Deserialize(fStream) as UserSettings; }
            finally { fStream.Close(); }
            return us;
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!splash.IsSplashFormVisible)
                {
                    //  splashScreenManager.ShowWaitForm();
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true);
                }
                statesplash = true;
                //      Thread t = new Thread(new ThreadStart(StartForm));
                //       t.Start();
                frmConnect f = new frmConnect();
                AddTabControl(f, "Kết nối");
                //     t.Abort();
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }

            }
            catch
            {
                if (splash.IsSplashFormVisible || statesplash == true)
                {
                    SplashScreenManager.CloseForm();
                    statesplash = false;
                }

            }
        }
    }
}