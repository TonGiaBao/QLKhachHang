using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Xml;
using System.Windows.Forms;
namespace QLKhachHang
{
    public class Connect
    {
        public bool start = true;
        public string server;
        public string port;
        public string database;
        public string user_id;
        public string password;

        static string conString = ConfigurationManager
                        .ConnectionStrings["MyAppConnection"]
                        .ConnectionString;
      
        //public MySqlConnection conn = new MySqlConnection("Server=localhost;Database=ql;Port=3306;User ID=root;Password=123456");
    //    public static String _connectionString = @"Server=baokhoa.ddns.net;Port=3306;Database=dev_tv_test;User ID=dev;Password=hn4558;CharSet=utf8;";
    //    public static String _connectionString = @"Server=bao-pc;Port=3306;Database=test2;User ID=root;Password=123456;CharSet=utf8;";
        public static String _connection;
     //   public static MySqlConnection conn1;
   //     public static String _connectionString = @"Server=bao-pc;Port=3306;Database=test2;User ID=user;Password=123456;CharSet=utf8;";
        public MySqlConnection conn = new MySqlConnection(conString);
        //116.108.71.230
       // public static String _connectionString = @"Server=localhost;Database=ql;Port=3306;User ID=root;Password=123456";

        public void connn()
        {
          /* _connection = @"Server=" + server + ";Port=" + port + ";Database=" + database + ";User ID=" + user_id + ";Password=" + password + ";CharSet=utf8;";
           conn = new MySqlConnection(_connection);
           try
           {
               conn.Open();
               XtraMessageBox.Show("Kết nối thành công");
           }
           catch(Exception ex)
           {
               XtraMessageBox.Show(ex.Message);
           }*/
           try
           {
               StringBuilder msb = new StringBuilder("Server=");
               msb.Append(server);
               msb.Append(";Port=");
               msb.Append(port);
               msb.Append(";Database=");
               msb.Append(database);
               msb.Append(";User ID=");
               msb.Append(user_id);
               msb.Append(";Password=");
               msb.Append(password);
               msb.Append(";CharSet=utf8");
               conString = msb.ToString();
               conn = new MySqlConnection(conString);
               
               Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
               config.ConnectionStrings.ConnectionStrings["MyAppConnection"].ConnectionString = conString;
               //config.ConnectionStrings.ConnectionStrings["MyAppConnection"].ProviderName = "System.Data.SqlClient";
               config.Save(ConfigurationSaveMode.Modified);
               ConfigurationManager.RefreshSection("connectionStrings");
               //Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
               //config.AppSettings.Settings.Add("MyAppConnection", conString);
               //config.Save(ConfigurationSaveMode.Minimal);
               //conString = ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("MyAppConnection", msb.ConnectionString,""));
               /*XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

               foreach (XmlElement xElement in xmlDoc.DocumentElement)
               {
                   if (xElement.Name == "connectionStrings")
                   {
                       xElement.FirstChild.Attributes[2].Value = conString;

                   }
               }
               xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);*/
               conn.Open();
               XtraMessageBox.Show("Kết nối thành công");
           }
           catch(Exception ex)
           {
               XtraMessageBox.Show(ex.Message);
           }
        }
       
        public void openconnection()
        {
            try
            {
                if (conn == null)
                    conn = new MySqlConnection(conString);
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        public void closeconnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();

        }
    }
}
