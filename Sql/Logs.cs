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
using Sql.Utils;

namespace Sql
{
    public partial class Logs : Form
    {
        public const int aa = 0xA1;
        public const int bb = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr iptr, int msg, int wparam, int iparam);

        [DllImportAttribute("user32.dll")]
        public static extern int ReleaseCapture();

        public Logs()
        {
            InitializeComponent();
            Log.InitializeDB();
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            List<Log> users = Log.GetUsers();

            lvUsers.Items.Clear();

            foreach (Log u in users)
            {
                ListViewItem item = new ListViewItem(new String[] { u.Id.ToString(), u.user, u.action, u.timeOfQuery, u.TableName });
                item.Tag = u;

                lvUsers.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, aa, bb, 0);
        }
    }
}