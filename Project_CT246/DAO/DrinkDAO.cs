using Project_CT246.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CT246.DAO
{
    public class DrinkDAO
    {
        private static DrinkDAO instance;

        public static DrinkDAO Instance {
            get {
                if (instance == null)
                {
                    instance = new DrinkDAO();
                }
                return DrinkDAO.instance;
            }
            private set { DrinkDAO.instance = value; }
        }

        public static int DrinkWidth = 50;
        public static int DrinkHeight = 50;

        private DrinkDAO() { }
        
        public List<Drink> LoadDrinkList()
        {
            List<Drink> drinklist = new List<Drink>();

            DataTable data = DataProvider.Instance.ExcuteQuery("select * from dbo.Drink");

            foreach (DataRow item in data.Rows)
            {
                Drink drink = new Drink(item);
                drinklist.Add(drink);
            }
            //List<string> drinklist = new List<string>();
            //drinklist.Add("Nhan");
            //drinklist.Add("Trung");
            return drinklist;
        } 
    }
}
