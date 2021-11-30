using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CT246.DTO
{
    public class Drink
    {
        public Drink(int id, int idCategory, string name, float price)
        {
            this.ID = id;
            this.IdCategory = idCategory;
            this.Name = name;
            this.Price = price;
        }

        public Drink(DataRow row)
        {
            this.ID = (int)row["id"];
            this.IdCategory = (int)row["idCategory"];
            this.Name = row["name"].ToString();
            this.Price = (float)row["price"];
        }
        private int idCategory;

        private float price;

        private string name;

        private int iD;

        public int ID {
            get => iD;
            set => iD = value;
        }
        public string Name { get => name; set => name = value; }
        
        public int IdCategory { get => idCategory; set => idCategory = value; }
        public float Price { get => price; set => price = value; }
    }
}
