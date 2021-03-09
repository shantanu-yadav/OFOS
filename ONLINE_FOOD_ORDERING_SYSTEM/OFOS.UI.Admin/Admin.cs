using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OFOS.DAL;
using OFOS.ExceptionLogs;

namespace OFOS.UI
{
    public class Admin
    {
        static void Main(string[] args)
        {
            bool logout = true;
            do
            {
                AdminLoginDOA login = new AdminLoginDOA();
                ExceptionLogging e = new ExceptionLogging();

                Console.WriteLine("Enter the User Name");
                string username = Console.ReadLine();
                Console.WriteLine("Enter the Password");
                string password = Console.ReadLine();
                if (login.AdminLoginAuth(username, password))
                {
                    bool exit = true;
                    do
                    {
                        Console.WriteLine("Menu");
                        Console.WriteLine("1.Add Food Items");
                        Console.WriteLine("2.Show Order Details");
                        Console.WriteLine("3.Modify Order Details");
                        Console.WriteLine("4.Modify Food Availability");
                        Console.WriteLine("5.Logout");
                        Console.WriteLine("6.Exit");
                        Console.WriteLine("Enter Option");
                        int ch = int.Parse(Console.ReadLine());
                        MenuDAO food = new MenuDAO();
                        OrderDAO order = new OrderDAO();
                        switch (ch)
                        {
                            case 1:
                                {
                                    String FName, FCate, stock;
                                    int price;
                                    Console.WriteLine("Enter the Food Name");
                                    FName = Console.ReadLine();
                                    Console.WriteLine("Enter the Food Category");
                                    FCate = Console.ReadLine();
                                    Console.WriteLine("Enter the Food Price");
                                    price = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter the Food Availability");
                                    stock = Console.ReadLine();

                                    try
                                    {
                                        if (food.AddFoodItems(new Model.Menu() { Food_Name = FName, Food_Category = FCate, price = price, stock = stock }))
                                        {
                                            Console.WriteLine("Food Items Added");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        e.LogInFile(ex);
                                        Console.WriteLine("Something went wrong please check logs!!");
                                    }
                                }
                                break;
                            case 2:
                                try
                                {
                                    DataTable dt = order.GetOrders();
                                    foreach (DataRow r in dt.Rows)
                                    {
                                        Console.WriteLine($"OrderID:{r["OderID"]} FoodID:{r["FoodID"]} Order Status: {r["Orderstatus"]} Shipping Address: {r["ShippingAddress"]} ETD: {r["ExpectedTimeOfDelivery"]}  ");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    e.LogInFile(ex);
                                    Console.WriteLine("Something went wrong please check logs!!");
                                }

                                break;
                            case 3:

                                Console.WriteLine("Enter Order ID");
                                int OrderId = int.Parse(Console.ReadLine());
                                Console.WriteLine("1. Change Status to Dispatched");
                                Console.WriteLine("2. Change Status to Delivered");
                                int n = int.Parse(Console.ReadLine());
                                String orderstatus = null;
                                switch (n)
                                {
                                    case 1:
                                        orderstatus = "Dispatched";
                                        break;
                                    case 2:
                                        orderstatus = "Delivered";
                                        break;
                                    default:
                                        Console.WriteLine("Enter The correct Choice");
                                        break;
                                }
                                try
                                {
                                    if (order.ModifyOrder(OrderId, orderstatus))
                                    {
                                        Console.WriteLine("Order Updated");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Order Update Unsuccessfull!");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    e.LogInFile(ex);
                                    Console.WriteLine("Something went wrong please check logs!!");
                                }

                                break;
                            case 4:
                                Console.WriteLine("Enter Food ID");
                                int FoodId = int.Parse(Console.ReadLine());
                                Console.WriteLine("1. Change Availabilty to Available");
                                Console.WriteLine("2. Change Availabilty to Not Available");
                                int choice = int.Parse(Console.ReadLine());
                                String Stock = null;
                                switch (choice)
                                {
                                    case 1:
                                        Stock = "Available";
                                        break;
                                    case 2:
                                        Stock = "Not Available";
                                        break;
                                    default:
                                        Console.WriteLine("Enter The correct Choice");
                                        break;
                                }
                                if (food.ModifyFoodStock(FoodId, Stock))
                                {
                                    Console.WriteLine("Stock Updated");
                                }
                                else
                                {
                                    Console.WriteLine("Availabilty Update Unsuccessfull!");
                                }
                                break;
                            case 5:
                                exit = false;
                                break;
                            case 6:
                                exit = false;
                                logout = false;
                                break;
                            default:
                                Console.WriteLine("Enter Proper option");
                                break;
                        }
                    } while (exit);

                }
                else
                    Console.WriteLine("Incorrect UserName or Password!");
            } while (logout);
        }

    }
}
