using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace demka.AppData
{
    class AppConnect
    {
        public static DemkaEntities modeldb;
        public static Users users;
    }
    public partial class Users
    {
        public string FIO
        {
            get { return (surname + " " + name + " " + patronomic).Trim(); }
        }
    }

    public partial class Products
    {
       
        public string ProductName
        {
            get
            {
                if (!string.IsNullOrEmpty(description))
                {
                    int i = description.IndexOf(". Производитель");
                    if (i > 0) return description.Substring(0, i);
                }
                return StockItems != null ? StockItems.nameProduct : "";
            }
        }

      
        public int Quantity
        {
            get { return StockItems != null ? StockItems.quantity : 0; }
        }

      
        public string ImagePath
        {
            get
            {
                return string.IsNullOrEmpty(images)
                    ? "/Images/picture.png"
                    : "/Images/" + images;
            }
        }

       
        public Brush RowColor
        {
            get
            {
                string color = "#FFFFFF";
                if (Quantity == 0) color = "#ADD8E6";
                else if (Quantity <= 5) color = "#FFD6D6";
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            }
        }
    }

    public partial class OrderItems
    {
      
        public decimal Sum
        {
            get { return quantity * price; }
        }
    }

    public partial class Orders
    {
       
        public decimal TotalSum
        {
            get { return OrderItems.Sum(i => i.quantity * i.price); }
        }

      
        public int TotalCount
        {
            get { return OrderItems.Sum(i => i.quantity); }
        }
    }
}

