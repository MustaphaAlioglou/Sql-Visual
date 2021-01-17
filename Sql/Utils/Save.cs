using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sql.Utils
{
    internal class Save
    {
        public void SaveCred()
        {
            SaveFileDialog savefile = new SaveFileDialog();

            savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}