using System;
using System.Collections.Generic;
using System.Text;

namespace OFOS.Model
{
    public class OrderDetails
    {
        public int Order_Id { get; set; }
        public int Food_Id { get; set; }

        public int CustomerId { get; set; }
        public string Order_Status { get; set; }
        public string Shipping_Address { get; set; }

        public int quantity { get; set; }

        public decimal Total_Amount { get; set; }
        public DateTime Expected_Time_of_Delivery { get; set; }
    }
}
