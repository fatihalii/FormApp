using StajEntity.DAL.DbOperations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userDb = UserDb.GetInstance();

            var user = userDb.GetUserByUsernameAndPassword(tbUserName.Text, tbPassword.Text);

            if (user != null)
            {

                MessageBox.Show("Login Success");
            }
            else
            {
                MessageBox.Show("Login Failed");
            }

        }
    }
}
