using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

using MessageLoggerForm.Data;

namespace MessageLoggerForm.ClassListView
{
    public static class Class_ListView
    {
        public static ListView lv = new ListView();
        private static Class_Data.cteDataTable cteDataTable = new Class_Data.cteDataTable();
        
        /// <summary>
        /// Initializes the Listview module
        /// </summary>
        public static void LV_Initialize()
        {
            lv.Dock = DockStyle.Fill;
            lv.GridLines = true;
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.Items.Clear();
            
            //Get the Data-Table enums which are used for the columns
            List<string> lszEnums = cteDataTable.GetEnumListAsString();

            //Fill each column 
            foreach(string szEnum in lszEnums)
            {
                lv.Columns.Add(new ColumnHeader { Name = szEnum, Text = szEnum, Width = -2 });
            }
        }

        /// <summary>
        /// Method to add the latest row from the data table into the list view.
        /// </summary>
        public static void AddNewestRowToListView(DataRow dr)
        {
            //Get the enum as a list
            var lst = cteDataTable.GetEnumListAsString();

            //Create new list view item object
            //ListViewItem item = new ListViewItem(dt.Rows[0][teDataTable.Index.ToString()].ToString());
            ListViewItem item = new ListViewItem(dr[Class_Data.teDataTable.Index.ToString()].ToString());

            foreach (string szEnum in lst)
            {
                if (szEnum != Class_Data.teDataTable.Index.ToString())
                {
                    //Add subitems
                    item.SubItems.Add(dr[szEnum].ToString());
                }
            }

            lv.Items.Insert(0, item);
        }
    }
}
