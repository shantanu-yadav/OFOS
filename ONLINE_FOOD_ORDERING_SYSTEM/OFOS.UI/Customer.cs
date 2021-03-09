using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OFOS.DAL;
using OFOS.Model;
using OFOS.ExceptionLogs;
using System.Text.RegularExpressions;
using OFOS.CustomException;

namespace OFOS.UI
{
    public class Customer
    {
        public static void Main(string[] args)
        {
            bool exit = true;
            do
            {

                Console.WriteLine("1.New User(Register)\n2.Login\n3.Exit");
                int choice = int.Parse(Console.ReadLine());
                CustomerDAO customer = new CustomerDAO();
                ExceptionLogging e = new ExceptionLogging();


                switch (choice)
                {

                    case 1:
                        Console.WriteLine("Create Username");
                        string Username = Console.ReadLine();
                        Console.WriteLine("Create Password");
                        string pass = Console.ReadLine();


                        try
                        {

                            if (customer.RegisterCustomer(Username, pass))
                            {
                                Console.WriteLine("Registered Successfully.");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.ToString().Contains("duplicate key"))
                            {
                                Console.WriteLine("User already exists!!");
                            }
                            else
                            {
                                e.LogInFile(ex);
                                Console.WriteLine("Something went wrong please check logs!!");
                            }
                        }


                        break;
                    case 2:
                        Console.WriteLine("Username");
                        string Uname = Console.ReadLine();
                        Console.WriteLine("Password");
                        string Pass = Console.ReadLine();

                        try
                        {

                            if (customer.CustomerLoginAuth(Uname, Pass))
                            {
                                bool exitchoice = true;

                                do
                                {
                                    Console.WriteLine("Menu");
                                    Console.WriteLine("1.Show Menu");
                                    Console.WriteLine("2.Place Order");
                                    Console.WriteLine("3.View Orders");
                                    Console.WriteLine("4.Track Order");
                                    Console.WriteLine("5.Update Order");
                                    Console.WriteLine("6.Logout");
                                    Console.WriteLine("Enter Option");
                                    int ch = int.Parse(Console.ReadLine());
                                    OrderDAO order = new OrderDAO();
                                    MenuDAO food = new MenuDAO();
                                    PaymentDAO payment = new PaymentDAO();

                                    switch (ch)
                                    {
                                        case 1:
                                            {
                                                DataTable df = food.GetMenu();
                                                if (df.Rows.Count > 0)
                                                {
                                                    foreach (DataRow r in df.Rows)
                                                    {
                                                        Console.WriteLine($"FoodId:{r["FoodID"]} FoodName:{r["FoodName"]} FoodCategory:{r["FoodCategory"]} price:{r["price"]} Availability:{r["stock"]}");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Menu Not Available!!");
                                                }

                                            }
                                            break;
                                        case 2:
                                            {
                                                try
                                                {

                                                    Console.WriteLine("Enter Food Id");
                                                    int FoodId = int.Parse(Console.ReadLine());
                                                    Console.WriteLine("Enter Quantity");
                                                    int Quantity = int.Parse(Console.ReadLine());
                                                    DataRow r = food.GetFoodById(FoodId);
                                                    decimal TotalAmount = 0;


                                                    if (r == null)
                                                        throw new InvalidOrderIdException();
                                                    else
                                                    {
                                                        TotalAmount = (decimal)r["price"] * Quantity;
                                                        Console.WriteLine("Enter Shipping Address");
                                                        string address = Console.ReadLine();
                                                        string orderStatus = "Processing";
                                                        DateTime d = DateTime.Now;
                                                        DateTime d2 = d.AddHours(1.0);

                                                        
                                                        Console.WriteLine("Choose Payment Option : \n1.COD \n2.Online Payment");
                                                        int paymentChoice = int.Parse(Console.ReadLine());
                                                        int customerId = customer.GetCustomerId(Uname, Pass);
                                                        switch (paymentChoice)
                                                        {
                                                            case 1:
                                                                if (order.CreateOrder(new Model.OrderDetails() { Food_Id = FoodId, CustomerId = customerId, Order_Status = orderStatus, Shipping_Address = address, Expected_Time_of_Delivery = d2, quantity = Quantity, Total_Amount = TotalAmount }))
                                                                {
                                                                    Console.WriteLine("Order Placed");
                                                                }
                                                                break;
                                                            case 2:
                                                                Console.WriteLine("Enter Cardholder Name");
                                                                string name = Console.ReadLine();
                                                                Console.WriteLine("Enter Card Number");
                                                                string CardNumber = Console.ReadLine();
                                                                Console.WriteLine("Enter Phone Number");
                                                                string phoneNumber = Console.ReadLine();


                                                                if (!Regex.IsMatch(CardNumber, @"^-?\d+$"))
                                                                    throw new InvalidCardNumberExceptions();
                                                                if (!Regex.IsMatch(phoneNumber, @"^-?\d+$"))
                                                                    throw new InavalidPhoneNumberExceptions();


                                                                string transactionStatus = "Successfull";
                                                                if (payment.AddPaymentDetails(new Model.PaymentDetails() { Customer_Name = name, Customer_Card_Number = CardNumber, Total_Amount = TotalAmount, Customer_Phone_Number = phoneNumber, Transaction_Status = transactionStatus }))
                                                                {
                                                                    if (order.CreateOrder(new Model.OrderDetails() { Food_Id = FoodId, CustomerId = customerId, Order_Status = orderStatus, Shipping_Address = address, Expected_Time_of_Delivery = d2, quantity = Quantity, Total_Amount = TotalAmount }))
                                                                    {
                                                                        Console.WriteLine("Order Placed");
                                                                    }
                                                                }


                                                                break;
                                                        }
                                                    }
                                                }
                                                catch (InvalidOrderIdException ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                                catch (InavalidPhoneNumberExceptions ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                                catch (InvalidCardNumberExceptions ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                                catch (Exception ex)
                                                {

                                                    e.LogInFile(ex);
                                                    Console.WriteLine("Something went wrong please check logs!!");


                                                }
                                            }
                                            break;
                                        case 3:
                                            {
                                                try
                                                {
                                                    int customerId = customer.GetCustomerId(Uname, Pass);


                                                    if (customerId != 0)
                                                    {
                                                        DataTable dt = order.GetOrderByCustomerId(customerId);


                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            foreach (DataRow or in dt.Rows)
                                                            {
                                                                int foodId = (int)or["FoodID"];
                                                                DataRow r = food.GetFoodById(foodId);

                                                                Console.WriteLine($"OrderId : {or["OderID"]} FoodItem : {r["FoodName"]}  Quantity : {or["Quantity"]} TotalAmount : {or["TotalAmount"]} Expected Time of Delivery : {or["ExpectedTimeOfDelivery"]}");

                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("No Orders found!!");
                                                        }
                                                    }


                                                }
                                                catch (Exception ex)
                                                {
                                                    e.LogInFile(ex);
                                                    Console.WriteLine("Something went wrong please check logs!!");
                                                }

                                            }
                                            break;
                                        case 4:
                                            Console.WriteLine("Enter Order Id");
                                            int OrderId = int.Parse(Console.ReadLine());
                                            DataRow dr = order.GetOrderById(OrderId);
                                            if (dr != null)
                                            {
                                                Console.WriteLine($"Order Status : {dr["Orderstatus"]}");

                                            }
                                            else
                                            {
                                                Console.WriteLine("Order not found!");
                                            }
                                            break;
                                        case 5:
                                            {
                                                Console.WriteLine("Enter Order ID:");
                                                int UpdateOrderId = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter New Food Id");
                                                int UpdatedFoodId = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter Quantity");
                                                int Quantity = int.Parse(Console.ReadLine());

                                                DataRow r = food.GetFoodById(UpdatedFoodId);

                                                int customerId = customer.GetCustomerId(Uname, Pass);



                                                decimal TotalAmount = (decimal)r["price"] * Quantity;
                                                Console.WriteLine("Enter Updated Shipping Address");
                                                string address = Console.ReadLine();
                                                string orderStatus = "Processing";
                                                DateTime d = DateTime.Now;
                                                DateTime d2 = d.AddHours(1.0);

                                                try
                                                {
                                                    
                                                    Console.WriteLine("Choose Payment Option : \n1.COD \n2.Online Payment");
                                                    int paymentChoice = int.Parse(Console.ReadLine());

                                                    switch (paymentChoice)
                                                    {
                                                        case 1:
                                                            if (order.UpdateOrder(UpdateOrderId, UpdatedFoodId, orderStatus, address, d2, Quantity, TotalAmount))
                                                            {
                                                                Console.WriteLine("Order Updated Successfully");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Order Update Failed!!");
                                                            }
                                                            break;
                                                        case 2:
                                                            Console.WriteLine("Enter Cardholder Name");
                                                            string name = Console.ReadLine();
                                                            Console.WriteLine("Enter Card Number");
                                                            string CardNumber = Console.ReadLine();
                                                            Console.WriteLine("Enter Phone Number");
                                                            string phoneNumber = Console.ReadLine();
                                                            try
                                                            {
                                                                if (!Regex.IsMatch(CardNumber, @"^-?\d+$"))
                                                                    throw new InvalidCardNumberExceptions();
                                                                if (!Regex.IsMatch(phoneNumber, @"^-?\d+$"))
                                                                    throw new InavalidPhoneNumberExceptions();

                                                                string transactionStatus = "Successfull";
                                                                if (payment.AddPaymentDetails(new Model.PaymentDetails() { Customer_Name = name, Customer_Card_Number = CardNumber, Total_Amount = TotalAmount, Customer_Phone_Number = phoneNumber, Transaction_Status = transactionStatus }))
                                                                {
                                                                    if (order.UpdateOrder(UpdateOrderId, UpdatedFoodId, orderStatus, address, d2, Quantity, TotalAmount))
                                                                    {
                                                                        Console.WriteLine("Order Updated Successfully");
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("Order Update Failed!!");
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Console.WriteLine(ex.Message);
                                                            }

                                                            break;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    e.LogInFile(ex);
                                                    Console.WriteLine("Something went wrong please check logs!!");
                                                }
                                            }
                                            break;
                                        default:
                                            exitchoice = false;
                                            break;
                                    }
                                } while (exitchoice);


                            }
                            else
                            {
                                Console.WriteLine("User does not exists or Invalid Credentials!");
                            }
                        }
                        catch (Exception ex)
                        {

                            e.LogInFile(ex);
                            Console.WriteLine("Something went wrong please check logs!!");

                        }
                        break;
                    case 3:
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("Enter Proper Option");
                        break;

                }
            } while (exit);
        }
    }
}

