using System.Data.SqlClient;
using StoreModel;


namespace StoreDL
{
    public class SQLRepository : IRepository
    {
        private readonly string _connectionStrings;
        public SQLRepository(string p_connectionStrings)
        {
            _connectionStrings = p_connectionStrings;
        }

        //===================================================================================================================================== Add a Customer

        public Customer AddCustomer(Customer p_customer)
        {
            string sqlQuery = @"insert into Customer
                                            values (@customer_name, @customer_address, @customer_phone, @username)";


            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open();

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon);
                command.Parameters.AddWithValue("@customer_name", p_customer.Name);
                command.Parameters.AddWithValue("@customer_address", p_customer.Address);
                command.Parameters.AddWithValue("@customer_phone", p_customer.Phone);
                command.Parameters.AddWithValue("@username", p_customer.Username);

                command.ExecuteNonQuery();
            }
            return p_customer;
        }

        ///===================================================================================================================================== Search a Customer

        public List<Customer> GetAllCustomers() //tested
        {
            List<Customer> listOfCustomers = new List<Customer>();

            string sqlQuery = @"select * from Customer";

            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens connection to database

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon); //create command object that has our sqlquery and con object

                SqlDataReader reader = command.ExecuteReader(); // sqlDataReader is a class that helps C# understand tables

                while (reader.Read()) //runs all the rows until there is no mre data from the sql table. If there is another row = true, if not = false
                {
                    listOfCustomers.Add(new Customer()
                    {
                        ID = reader.GetInt32(0), //in sql the starting point is also 0
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Phone = reader.GetString(3),
                        Username = reader.GetString(4)
                    });
                }
            }
            return listOfCustomers;
        }


        //===================================================================================================================================== View Store Inventory

        public List<StoreFront> GetAllStores() //tested
        {

            List<StoreFront> listOfStores = new List<StoreFront>();

            string sqlQuery = @"select * from storefront";

            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open();

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfStores.Add(new StoreFront()
                    {
                        StoreID = reader.GetInt32(0),
                        StoreFrontName = reader.GetString(1),
                        StoreFrontAddress = reader.GetString(2),
                        Username = reader.GetString(3)
                        // Inventory = listname.Add(reader.GetString(0));-----------------------
                    });
                }
            }
            return listOfStores;
        }

        //============================================================================================================== Get All LineItems By Order ID 

        public List<LineItem> GetAllLineItemsByOrderID(int p_orderID) //tested
        {
            List<LineItem> listOfLineItems = new List<LineItem>();

            string sqlQuery = @"SELECT *  FROM lineitem Where orderid = @orderID";


            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);

                command.Parameters.AddWithValue("@orderID", p_orderID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfLineItems.Add(new LineItem()
                    {
                        OrderID = reader.GetInt32(0),
                        ProductID = reader.GetInt32(1),
                        Quantity = reader.GetInt32(2)
                    });
                }
            }
            return listOfLineItems;
        }

        //=====================================================================================================================================

        // public void PlaceOrder(int p_storeFrontID, int p_custID, int p_total, List<LineItem> p_lineItem)
        // {
        //     string sqlQueryOrder = @"insert into Orders
        //                             values(@customerid, @storefrontid, @itemid);
        //                             select scope_identity();";
        //     string sqlQueryLineItem = @"insert into LineItem
        //                                 values(@productid, @quantity)";

        //     string sqlQueryUpdateQuantity = @"update LineItem
        //                                     set "

        //     using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
        //     {
        //         sqlcon.Open();

        //         SqlCommand command = new SqlCommand()

        //     }
        // }
        public List<Product> GetAllProducts() //4-3 tested
        {
            string sqlQuery = @"select * from product;";

            List<Product> listOfProducts = new List<Product>();
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfProducts.Add(new Product()
                    {
                        ItemID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2)

                    });
                }


            }
            return listOfProducts;


        }


        //=====================================================================================================================================

        // public List<Order> GetCustomerOrderByID(int p_custid)
        // {
        //     List<Order> listOfOrders = new List<Order>();

        //     string sqlQuery = @"select o.orders_customerid , o.order_id, s.storefront_name, p.product_name, p.product_price, l.quantity, o.order_totalprice, o.DateCreated, c.customer_name  
        //                                         from orders o 
        //                                         inner join lineitem l 
        //                                         on o.order_id = l.orderid 
        //                                         INNER join product p 
        //                                         on l.product_id = p.product_id
        //                                         inner join storefront s
        //                                         on o.orders_storefrontid = s.storefront_id
        //                                         inner join Customer c
        //                                         on o.orders_customerid = c.customer_id
        //                                         where o.orders_customerid = @custidinput;";

        //     using (SqlConnection con = new SqlConnection(_connectionStrings))
        //     {
        //         con.Open();
        //         SqlCommand command = new SqlCommand(sqlQuery, con);
        //         command.Parameters.AddWithValue("@custidinput", p_custid);

        //         SqlDataReader reader = command.ExecuteReader();

        //         while (reader.Read())
        //         {
        //             listOfOrders.Add(new Order()
        //             {
        //                 CustomerID = reader.GetInt32(0),
        //                 OrderID = reader.GetInt32(1),
        //                 StoreFrontName = reader.GetString(2),
        //                 Product = reader.GetString(3),
        //                 Price = reader.GetDecimal(4),
        //                 Quantity = reader.GetInt32(5),
        //                 TotalPrice = reader.GetDecimal(6),
        //                 DateCreated = reader.GetDateTime(7),
        //                 CustomerName = reader.GetString(8)
        //             });
        //         }
        //     }
        //     return listOfOrders;
        // }


        //=====================================================================================================================================

        // public List<Order> GetStoreOrderHistoryByID(int p_storeid)
        // {
        //     List<Order> listOfOrders = new List<Order>();

        //     string sqlQuery = @"select o.orders_customerid , o.order_id, s.storefront_name, p.product_name, p.product_price, l.quantity, o.order_totalprice, o.DateCreated, c.customer_name  
        //                                                 from orders o 
        //                                                 inner join lineitem l 
        //                                                 on o.order_id = l.orderid 
        //                                                 INNER join product p 
        //                                                 on l.product_id = p.product_id
        //                                                 inner join storefront s
        //                                                 on o.orders_storefrontid = s.storefront_id
        //                                                 inner join Customer c
        //                                                 on o.orders_customerid = c.customer_id
        //                                                 where orders_storefrontid = @storefrontid;";

        //     using (SqlConnection con = new SqlConnection(_connectionStrings))
        //     {
        //         con.Open();
        //         SqlCommand command = new SqlCommand(sqlQuery, con);
        //         command.Parameters.AddWithValue("@storefrontid", p_storeid);

        //         SqlDataReader reader = command.ExecuteReader();

        //         while (reader.Read())
        //         {
        //             listOfOrders.Add(new Order()
        //             {
        //                 CustomerID = reader.GetInt32(0),
        //                 ID = reader.GetInt32(1),
        //                 StoreFrontName = reader.GetString(2),
        //                 Product = reader.GetString(3),
        //                 Price = reader.GetDecimal(4),
        //                 Quantity = reader.GetInt32(5),
        //                 TotalPrice = reader.GetDecimal(6),
        //                 DateCreated = reader.GetDateTime(7),
        //                 CustomerName = reader.GetString(8)
        //             });
        //         }
        //     }
        //     return listOfOrders;
        // }


        //=====================================================================================================================================

        public Inventory ReplenishInventory(Inventory p_inventory)
        {

            string sqlQuery = @"update Inventory 
                                            set quantity = quantity + @_quantity
                                            where inventory_productid  = @_productid;";


            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens the connection

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon); //command will hold sql query that will execute on the current connection we have on the sqlcon object
                                                                       // command.Parameters.AddWithValue("@customer_id", p_customer.ID); //should auto generate
                command.Parameters.AddWithValue("@_productid", p_inventory.ProductID);
                command.Parameters.AddWithValue("@_quantity", p_inventory.Quantity);

                command.ExecuteNonQuery();
            }
            return p_inventory;

        }

        //=====================================================================================================================================

        public List<Inventory> GetAllInventory() //4-3 tested
        {
            string sqlQuery = @"select * from Inventory;";

            List<Inventory> listOfInventory = new List<Inventory>();
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfInventory.Add(new Inventory()
                    {
                        StoreID = reader.GetInt32(0),
                        ProductID = reader.GetInt32(1),
                        Quantity = reader.GetInt32(2)

                    });
                }

            }

            return listOfInventory;

        }

        //=====================================================================================================================================
        protected decimal CalTotalPrice(List<LineItem> p_cart)
        {
            decimal _totalPrice = 0m;
            decimal _productPrice = 0;
            foreach (var item in p_cart)
            {
                _productPrice = GetAllProducts().Find(p => p.ItemID.Equals(item.ProductID)).Price;
                _totalPrice += item.Quantity * _productPrice;
            }
            return _totalPrice;
        }
        public void PlaceOrder(Order p_order) // 4 - 3
        {
            string sqlQuery = @"insert into orders
                                values(@customerid, @storeid, @price, @datecreated);
                                select scope_identity();";

            string sqlQuery2 = @"insert into lineitem 
                                values (@orderid, @productid, @quantity);";

            string sqlQuery3 = @"update Inventory 
                                set quantity = quantity - @quantity
                                where inventory_storeid = @storeid
                                AND inventory_productid = @productid;";

            p_order.DateCreated = DateTime.UtcNow;
            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens the connection

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon);
                command.Parameters.AddWithValue("@customerid", p_order.CustomerID);
                command.Parameters.AddWithValue("@storeid", p_order.StoreID);
                command.Parameters.AddWithValue("@price", CalTotalPrice(p_order.Cart));
                command.Parameters.AddWithValue("@datecreated", p_order.DateCreated);

                int _orderid = Convert.ToInt32(command.ExecuteScalar());


                p_order.OrderID = _orderid;
                p_order.TotalPrice = CalTotalPrice(p_order.Cart);
                foreach (var item in p_order.Cart)
                {
                    item.OrderID = _orderid;
                    SqlCommand command2 = new SqlCommand(sqlQuery2, sqlcon);
                    command2.Parameters.AddWithValue("@orderid", _orderid);
                    command2.Parameters.AddWithValue("@productid", item.ProductID);
                    command2.Parameters.AddWithValue("@quantity", item.Quantity);
                    command2.ExecuteNonQuery();

                    SqlCommand command3 = new SqlCommand(sqlQuery3, sqlcon);
                    command3.Parameters.AddWithValue("@storeid", p_order.StoreID);
                    command3.Parameters.AddWithValue("@productid", item.ProductID);
                    command3.Parameters.AddWithValue("@quantity", item.Quantity);
                    command3.ExecuteNonQuery();
                }
            }
        }


        //=====================================================================================================================================

        public User RegisterUser(User p_user)
        {
            string sqlQuery = @"INSERT INTO Userdata
                                (username, password)
                                VALUES(@Username, @Password);";

            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens the connection

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon);
                command.Parameters.AddWithValue("@Username", p_user.Username);
                command.Parameters.AddWithValue("@Password", p_user.Password);

                command.ExecuteNonQuery();
            }
            return p_user;
        }


        //=====================================================================================================================================

        public List<User> GetAllUsers()
        {
            List<User> listOfUsers = new List<User>();

            string sqlQuery = @"SELECT username, password
                                            FROM Userdata;";

            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open();

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfUsers.Add(new User()
                    {
                        Username = reader.GetString(0),
                        Password = reader.GetString(1)
                    });
                }
            }
            return listOfUsers;
        }

        public void AssignMangerRoleToUser(string p_username)
        {
            string _sqlQuery = @"INSERT INTO storefront
                                VALUES(@name, @address, @username);";

            using (SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(_sqlQuery, conn);
                command.Parameters.AddWithValue("@name", $"StoreName");
                command.Parameters.AddWithValue("@address", "StoreAddress");
                command.Parameters.AddWithValue("@username", p_username);

                command.ExecuteNonQuery();
            }
        }

        public List<Order> GetAllOrders()
        {
            string _sqlQuery = @"SELECT order_id, orders_customerid, orders_storefrontid, order_totalprice, DateCreated
                                FROM orders;";
            List<Order> _listOrder = new List<Order>();

            using (SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(_sqlQuery, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    _listOrder.Add(new Order()
                    {
                        OrderID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        StoreID = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                        DateCreated = reader.GetDateTime(4)
                    });
                }
            }
            return _listOrder;
        }

        public List<CartDetails> GetAllCartByOrderID(int p_orderID)
        {
            string _sqlQuery = @"SELECT l.product_id, p.product_name, l.quantity, p.product_price 
                                FROM lineitem l, product p
                                WHERE l.product_id = p.product_id 
                                AND l.orderid = @orderID";

            List<CartDetails> _listCart = new List<CartDetails>();

            using (SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(_sqlQuery, conn);
                command.Parameters.AddWithValue("@orderID", p_orderID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    _listCart.Add(new CartDetails()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        Price = reader.GetDecimal(3)
                    });
                }
            }

            return _listCart;
        }
    }
}
