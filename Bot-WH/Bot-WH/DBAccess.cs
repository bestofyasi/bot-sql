using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bot_WH
{
    [Serializable]
    public class DBAccess
    {
        SqlConnection conn;
        public DBAccess()
        {
            conn = ConnectionManager.GetConnection();
        }

        public bool addItems(string itemName, int itemQty)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed")
            {
                conn.Open();
            }

            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "insert into dbo.items(name, qty)values ('" + itemName + "', '" + itemQty + "')";
            newCmd.ExecuteNonQuery();

            status = true;

            return status;

        }
    }
}