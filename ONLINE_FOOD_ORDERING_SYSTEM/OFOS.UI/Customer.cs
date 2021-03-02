using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OFOS.DAL;
using OFOS.Model;

namespace OFOS.UI
{
    class Customer
    {
        static void Main(string[] args)
        { bool exit = true;
            do
            {
                Console.WriteLine("1.New User(Register)\n2.Login");
                int choice = int.Parse(Console.ReadLine());
                CustomerDAO customer = new CustomerDAO();


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
                            Console.WriteLine(ex.Message);
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
                                    Console.WriteLine("4.Exit");
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
                                                foreach (DataRow r in df.Rows)
                                                {
                                                    Console.WriteLine($"FoodId:{r["FoodID"]} FoodName:{r["FoodName"]} FoodCategory:{r["FoodCategory"]} price:{r["price"]} stock:{r["stock"]}");
                                                }

                                            }
                                            break;
                                        case 2:
                                            {
                                                Console.WriteLine("Enter Food Id");
                                                int FoodId = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter Quantity");
                                                int Quantity = int.Parse(Console.ReadLine());

                                                DataRow r = food.GetFoodById(FoodId);

                                                int customerId = customer.GetCustomerId(Uname, Pass);



                                                decimal TotalAmount = (decimal)r["price"] * Quantity;
                                                Console.WriteLine("Enter Shipping Address");
                                                string address = Console.ReadLine();
                                                string orderStatus = "Processing";
                                                DateTime d = DateTime.Now;
                                                DateTime d2 = d.AddHours(1.0);

                                                try
                                                {
                                                    Console.WriteLine("Enter Customer Name");
                                                    string name = Console.ReadLine();
                                                    Console.WriteLine("Enter Card Number");
                                                    string CardNumber = Console.ReadLine();
                                                    Console.WriteLine("Enter Phone Number");
                                                    string phoneNumber = Console.ReadLine();
                                                    string transactionStatus = "Successfull";
                                                    if (payment.AddPaymentDetails(new Model.PaymentDetails() { Customer_Name = name, Customer_Card_Number = CardNumber, Total_Amount = TotalAmount, Customer_Phone_Number = phoneNumber, Transaction_Status = transactionStatus }))
                                                    {
                                                        if (order.CreateOrder(new Model.OrderDetails() { Food_Id = FoodId, CustomerId = customerId, Order_Status = orderStatus, Shipping_Address = address, Expected_Time_of_Delivery = d2, quantity = Quantity, Total_Amount = TotalAmount }))
                                                        {
                                                            Console.WriteLine("Order Placed");
                                                        }
                                                    }

                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                            break;
                                        case 3:
                                            {
                                                try
                                                {
                                                    int customerId = customer.GetCustomerId(Uname, Pass);


                                                    if(customerId != 0)
                                                    {
                                                        DataTable dt = order.GetOrderByCustomerId(customerId);

                                                        foreach (DataRow or in dt.Rows)
                                                        {
                                                            int foodId = (int)or["FoodID"];
                                                            DataRow r = food.GetFoodById(foodId);

                                                            Console.WriteLine($"OrderId : {or["OderID"]} FoodItem : {r["FoodName"]}  Quantity : {or["Quantity"]} TotalAmount : {or["TotalAmount"]} Expected Time of Delivery : {or["ExpectedTimeOfDelivery"]}");

                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("No Orders found");
                                                    }
                                                    
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }

                                            }
                                            break;
                                        case 4:
                                            exitchoice = false;
                                            break;
                                    }
                                } while (exitchoice);


                            }
                            else
                            {
                                Console.WriteLine("Invalid Credentials!");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        exit = false;
                        break;

                }
            } while (exit);



            Console.ReadKey();
        }
    }
}
