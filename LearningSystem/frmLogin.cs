using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            string sql = "SELECT " + p.DBKeyValue.usrpwd.ToString() + " from " + p.DBTable.d_usrlist.ToString() + " WHERE " + p.DBKeyValue.usrid.ToString() + " = '" + comboUsrid.Text.Trim() + "'";
            List<string> pwd = p.queryDB(sql, p.DBKeyValue.usrpwd);
            if (txtUsrpwd.Text.Trim() == pwd[0])
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

    }
}
