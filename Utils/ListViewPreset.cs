using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utils
{
    public class ListViewPreset
    {
        private Dictionary<string, int> Columns;

        public ListViewPreset(Dictionary<string, int> set)
        {
            Columns = set;
        }

        public void SetListView(ListView lw)
        {
            lw.Columns.Clear();

            foreach(var k in Columns.Keys)
            {
                lw.Columns.Add(k, Columns[k]);
            }
        }
    }
}
