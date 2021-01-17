using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using moose;

namespace Sql
{
    public partial class Main : Form
    {
        public const int aa = 0xA1;
        public const int bb = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr iptr, int msg, int wparam, int iparam);

        [DllImportAttribute("user32.dll")]
        public static extern int ReleaseCapture();

        private User currUser;

        public Main()
        {
            InitializeComponent();
            User.InitializeDB();
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            List<User> users = User.GetUsers();

            lvUsers.Items.Clear();

            foreach (User u in users)
            {
                ListViewItem item = new ListViewItem(new String[] { u.Id.ToString(), u.firstname, u.lastname, u.age.ToString() });
                item.Tag = u;

                lvUsers.Items.Add(item);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            String u = txtUsername.Text;
            String p = txtPassword.Text;
            int a = Convert.ToInt32(txtAge.Text);
            if (String.IsNullOrEmpty(u) || String.IsNullOrEmpty(p))
            {
                MessageBox.Show("It's empty");
                return;
            }

            currUser = User.Insert(u, p, a);
            LoadAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String u = txtUsername.Text;
            String p = txtPassword.Text;
            int age = Convert.ToInt32(txtAge.Text);
            if (String.IsNullOrEmpty(u) || String.IsNullOrEmpty(p))
            {
                MessageBox.Show("It's empty");
                return;
            }

            currUser.Update(u, p, age);

            LoadAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currUser == null)
            {
                MessageBox.Show("No user selected!");
                return;
            }

            currUser.Delete();

            LoadAll();
        }

        private void LoadAll()
        {
            List<User> users = User.GetUsers();

            lvUsers.Items.Clear();

            foreach (User u in users)
            {
                ListViewItem item = new ListViewItem(new String[] { u.Id.ToString(), u.firstname, u.lastname, u.age.ToString() });
                item.Tag = u;

                lvUsers.Items.Add(item);
            }
        }

        private void lvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count > 0)
            {
                ListViewItem item = lvUsers.SelectedItems[0];
                currUser = (User)item.Tag;

                int id = currUser.Id;
                String u = currUser.firstname;
                String p = currUser.lastname;
                int a = currUser.age;
                txtUsername.Text = u;
                txtId.Text = id.ToString();
                txtPassword.Text = p;
                txtAge.Text = a.ToString();
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, aa, bb, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logs ee = new Logs();
            ee.ShowDialog();
        }
    }
}