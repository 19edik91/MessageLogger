using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MessageLoggerForm.Data
{
    class Class_Data
    {
        public enum teDataTable
        {
            Index = 0,
            TimeStamp = 1,
            ByteStream = 2,
            Destination = 3,
            Source = 4,
            Kind = 5,
            Payload = 6,
            Object = 7,
            Command = 8,
            Interpretation = 9
        }

        /// <summary>
        /// Enum-class which added handling for teDataTable enums
        /// </summary>
        public class cteDataTable : Class_Helper.EnumBase<teDataTable>
        {
            public cteDataTable() { }
        }


        /// <summary>
        /// 
        /// </summary>
        public class cData
        {
            public DataTable dt { get; set; }
            private cteDataTable teTable = new cteDataTable();

            public void Clear()
            {
                dt.Clear();
            }

            /// <summary>
            /// Constructor for this class. Initializes the data table
            /// </summary>
            public cData()
            {
                dt.Columns.Add(new DataColumn { DataType = typeof(int), ColumnName=teTable.GetEnumAsString(teDataTable.Index), Caption=teTable.GetEnumAsString(teDataTable.Index) });
                dt.Columns.Add(new DataColumn { DataType = typeof(DateTime), ColumnName = teTable.GetEnumAsString(teDataTable.TimeStamp), Caption = teTable.GetEnumAsString(teDataTable.TimeStamp) });
                dt.Columns.Add(new DataColumn { DataType = typeof(byte[]), ColumnName = teTable.GetEnumAsString(teDataTable.ByteStream), Caption = teTable.GetEnumAsString(teDataTable.ByteStream) });
                dt.Columns.Add(new DataColumn { DataType = typeof(int), ColumnName = teTable.GetEnumAsString(teDataTable.Destination), Caption = teTable.GetEnumAsString(teDataTable.Destination) });
                dt.Columns.Add(new DataColumn { DataType = typeof(int), ColumnName = teTable.GetEnumAsString(teDataTable.Source), Caption = teTable.GetEnumAsString(teDataTable.Source) });
                dt.Columns.Add(new DataColumn { DataType = typeof(int), ColumnName = teTable.GetEnumAsString(teDataTable.Kind), Caption = teTable.GetEnumAsString(teDataTable.Kind) });
                dt.Columns.Add(new DataColumn { DataType = typeof(int), ColumnName = teTable.GetEnumAsString(teDataTable.Payload), Caption = teTable.GetEnumAsString(teDataTable.Payload) });
                dt.Columns.Add(new DataColumn { DataType = typeof(int), ColumnName = teTable.GetEnumAsString(teDataTable.Object), Caption = teTable.GetEnumAsString(teDataTable.Object) });
                dt.Columns.Add(new DataColumn { DataType = typeof(int), ColumnName = teTable.GetEnumAsString(teDataTable.Command), Caption = teTable.GetEnumAsString(teDataTable.Command) });
                dt.Columns.Add(new DataColumn { DataType = typeof(string), ColumnName = teTable.GetEnumAsString(teDataTable.Interpretation), Caption = teTable.GetEnumAsString(teDataTable.Interpretation) });
            }

            /// <summary>
            /// Method to add rows into this data table
            /// </summary>
            /// <param name="index"></param>
            /// <param name="dateTime"></param>
            /// <param name="byteStream"></param>
            /// <param name="dest"></param>
            /// <param name="source"></param>
            /// <param name="kind"></param>
            /// <param name="payload"></param>
            /// <param name="obj"></param>
            /// <param name="cmd"></param>
            /// <param name="interpretation"></param>
            public void FillRow(int index, DateTime dateTime, byte[] byteStream, int dest, int source, int kind, int payload, int obj, int cmd, string interpretation)
            {
                //Create new row with the data table schema
                DataRow dr = dt.NewRow();

                //Fill the row
                dr[teDataTable.Index.ToString()] = index;
                dr[teDataTable.TimeStamp.ToString()] = dateTime;
                dr[teDataTable.ByteStream.ToString()] = byteStream;
                dr[teDataTable.Destination.ToString()] = dest;
                dr[teDataTable.Source.ToString()] = source;
                dr[teDataTable.Kind.ToString()] = kind;
                dr[teDataTable.Payload.ToString()] = payload;
                dr[teDataTable.Object.ToString()] = obj;
                dr[teDataTable.Command.ToString()] = cmd;
                dr[teDataTable.Interpretation.ToString()] = interpretation;

                //Insert at first position
                dt.Rows.InsertAt(dr, 0);
            }
        }
    }
}
