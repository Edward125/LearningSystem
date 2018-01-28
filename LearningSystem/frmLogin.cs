using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LearningSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            skinEngine1.SkinFile = p.AppFolder + @"\MacOS.ssk";
        }





        p.user currentUser = new p.user();



        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Text = "Learning System Login(ver:" + Application.ProductVersion + ")";

            string sql = "SELECT " + p.DBKeyValue.usrid + " from " + p.DBTable.d_usrlist.ToString();

            List<string> usrlst = p.queryDB(sql, p.DBKeyValue.usrid);

            if (usrlst.Count > 0)
            {
                foreach (var item in usrlst)
                {
                    comboUsrid.Items.Add(item);
                }
                comboUsrid.SelectedIndex = 0;
                currentUser.usrid = comboUsrid.Text.Trim();
                loadusrinfo(currentUser.usrid, ref currentUser);
                
            }
            else
            {
                MessageBox.Show("There is no user info in database,pls contact administrator to add user", "NO User Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("pls contact administrator to add user", "Sign In", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsrpwd.Text = string.Empty;
            txtUsrpwd.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string sql = "SELECT " + p.DBKeyValue.usrpwd.ToString() + " from " + p.DBTable.d_usrlist.ToString() + " WHERE " + p.DBKeyValue.usrid.ToString() + " = '" + comboUsrid.Text.Trim() + "'";
            //List<string> pwd = p.queryDB(sql, p.DBKeyValue.usrpwd);
            //if (txtUsrpwd.Text.Trim() == pwd[0])
            //{
            //    Form f = new frmMain();
            //    f.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Invalid password,pls retry", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}

            if (txtUsrpwd.Text.Trim() == currentUser.usrpwd)
            {
                Form f = new frmMain();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid password,pls retry", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


                     

        }

        private void comboUsrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboUsrid.SelectedIndex != -1)
            {
                currentUser.usrid = comboUsrid.Text.Trim();
                loadusrinfo(currentUser.usrid, ref currentUser);
            }
        }




        private void loadusrinfo( string usrid,ref p.user usr)
        {

            //public static Object Parse(Type enumType,string value)
            //例如：(Colors)Enum.Parse(typeof(Colors), "Red")

            usr.usrid = usrid;
            string sql = "SELECT * FROM " + p.DBTable.d_usrlist.ToString() + " WHERE " + p.DBKeyValue.usrid.ToString() + " = '" + usrid + "'";
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader re = cmd.ExecuteReader();

            if (re.HasRows)
            {
                while (re.Read())
                {
                  //  files.Add(re[dbkeyvalue.ToString()].ToString());
                    usr.usrpwd = re[p.DBKeyValue.usrpwd.ToString()].ToString().Trim();
                    usr.permission = (p.PermissionKey)Enum.Parse(typeof(p.PermissionKey), re[p.DBKeyValue.permission.ToString()].ToString().Trim());
                }
            }
            conn.Close();
        }
    }
}
