using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public DateTime EntryDate { get; set; }
        public Member Member { get; set; }

        public Equipment()
        {
            Member = new Member();
        }

        public static List<Equipment> LstEquipment()
        {
            List<Equipment> list = new List<Equipment>();

            string ConnString = COnfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            // Application Name
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Equipment";
            cmd.Parameters.clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    Equipment equipment = new Equipment();
                    equipment.EquipmentId = reader.GetInt32(0);
                    equipment.Name = reader.GetString(1);
                    equipment.Count = reader.GetInt32(2);
                    equipment.EntryDate = reader.GetDateTime(3);
                    list.Add(equipment);
                }
            }

            //for(int i = 1; i <= 30; i++)
            //{
            //    Equipment equipment = new Equipment();
            //    equipment.Name = "Laptop "+i.ToString();
            //    equipment.Count = i * 5;
            //    equipment.EntryDate = DateTime.Now.Date;
            //    list.Add(equipment);
            //}
            return list;
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