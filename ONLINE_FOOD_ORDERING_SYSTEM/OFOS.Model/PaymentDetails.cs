using System;
using System.Collections.Generic;
using System.Text;

namespace OFOS.Model
{
    public class PaymentDetails
    {
        public string Customer_Name { get; set; }
        public string Customer_Card_Number { get; set; }
        public string Customer_Phone_Number { get; set; }
        public decimal Total_Amount { get; set; }
        public string Transaction_Status { get; set; }
    }
}
