﻿using MySql.Data.MySqlClient;
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

namespace Sql
{
    public partial class Login : Form
    {
        public const int aa = 0xA1;
        public const int bb = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr iptr, int msg, int wparam, int iparam);

        [DllImportAttribute("user32.dll")]
        public static extern int ReleaseCapture();

        public Login()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUid.Text == "k702760")
            {
                string ee = string.Format("server={0};uid={1};pwd={2};database={3};", txtServer.Text, txtUid.Text, txtPassword.Text, txtDatabase.Text);
                LoginCred tt = LoginCred.GetInstance();
                MySqlConnection dbConn;

                dbConn = new MySqlConnection(ee);
                try
                {
                    dbConn.Open();
                    tt.Creds = ee;
                    Hide();
                    Main form1 = new Main();
                    form1.Show();
                }
                catch (MySql.Data.MySqlClient.MySqlException)
                {
                    MessageBox.Show("False credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("This is mapped (works) only for the user k702760", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, aa, bb, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtServer.Text = "dspai.kastoria.teiwm.gr";
            txtUid.Text = "k702760";
            txtPassword.Text = "K@$t0r1@";
            txtDatabase.Text = "world";
            button1.PerformClick();
        }
    }
}