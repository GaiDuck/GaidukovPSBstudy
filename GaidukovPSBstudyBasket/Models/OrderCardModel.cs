using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket.Models
{
    internal class OrderCardModel
    {
        public string Article { get; set; }
        public double TotalCost { get; set; }
        public double AverageScore { get; set; }
        public double TotalWeight { get; set; }
        public int DeliveryDays { get; set; }
    }
}