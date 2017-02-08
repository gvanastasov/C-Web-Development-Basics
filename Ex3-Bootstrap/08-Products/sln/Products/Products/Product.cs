using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Products
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public string PaymentDate { get; set; }
        public string DeliveryDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
