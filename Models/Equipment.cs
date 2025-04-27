using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace Inventory.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public DateTime EntryDate { get; set; }
        public Member Member { get; set; }
        public int Stock { get; set; }

        public Equipment()
        {
            Member = new Member();
        }

        public static List<Equipment> LstEquipment()
        {
            List<Equipment> plstData = new List<Equipment>();
            
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            // Application Name
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstEquipment";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            ////cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            ////cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader mrd = cmd.ExecuteReader();
            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    Equipment obj = new Equipment();
                    obj.EquipmentId = Convert.ToInt32(mrd["EquipmentId"].ToString());
                    obj.Name = mrd["EquipmentName"].ToString();
                    obj.Count = Convert.ToInt16(mrd["Quantity"].ToString());
                    obj.Stock = Convert.ToInt16(mrd["Stock"].ToString());
                    obj.EntryDate = Convert.ToDateTime(mrd["EntryDate"].ToString());

                    plstData.Add(obj);
                }
            }


            cmd.Dispose();
            connection.Close();

            return plstData;
        }

        public static DataTable dtEquipment()
        {
            //List<Equipment> list = new List<Equipment>();
            DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            // Application Name
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstEquipment";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            ////cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            ////cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);


            cmd.Dispose();
            connection.Close();

            return dataTable;
        }

        public static List<Equipment> LstAssignedEquipment()
        {
            List<Equipment> list = new List<Equipment>();

            for (int i = 1; i <= 30; i++)
            {
                Equipment equipment = new Equipment();
                equipment.Name = "Laptop " + i.ToString();
                equipment.Count = i * 5;
                equipment.EntryDate = DateTime.Now.Date;

                equipment.Member.MemberName = "Member " + i.ToString();
                equipment.Member.MemberDesignation = "SWE";
                equipment.Member.MobileNumber = "123456";
                list.Add(equipment);
            }
            return list;
        }
    }
}