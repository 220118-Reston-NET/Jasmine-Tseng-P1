using System.Data.SqlClient;
using StoreModel;


namespace StoreDL
{
    public class SQLRepository : IRepository
    {
        private readonly string _connectionStrings; //SQL repository will now require you to provide a connectionstring to n existing database to create an object out of it. It will also allow SQLrepository to dynamically point to diff databases as long as you have the connection string for it
        public SQLRepository(string p_connectionStrings)
        {
            _connectionStrings = p_connectionStrings;
        }
        //1. Add customer---------------------------------------------------------------------------------------------------\\
        public Customer AddCustomer(Customer p_customer)
        {
            string sqlQuery = @"insert into Customer
                                values (@customer_name, @customer_address, @customer_phone, @username)";


            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens the connection

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon); //command will hold sql query that will execute on the current connection we have on the sqlcon object
                // command.Parameters.AddWithValue("@customer_id", p_customer.ID); //should auto generate
                command.Parameters.AddWithValue("@customer_name", p_customer.Name);
                command.Parameters.AddWithValue("@customer_address", p_customer.Address);
                command.Parameters.AddWithValue("@customer_phone", p_customer.Phone);
                command.Parameters.AddWithValue("@username", p_customer.Username);

                command.ExecuteNonQuery();
            }
            return p_customer;
        }
        //2. search customer---------------------------------------------------------------------------------------------------\\
        public List<Customer> GetAllCustomers()
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


        //3. view store front inventory---------------------------------------------------------------------------------------------------\\
        public List<StoreFront> GetAllStores()
        {

            List<StoreFront> listOfStores = new List<StoreFront>();

            string sqlQuery = @"select * from storefront";

            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens connection to database

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon); //create command object that has our sqlquery and con object

                SqlDataReader reader = command.ExecuteReader(); // sqlDataReader is a class that helps C# understand tables

                while (reader.Read()) //runs all the rows until there is no mre data from the sql table. If there is another row = true, if not = false
                {
                    listOfStores.Add(new StoreFront()
                    {
                        StoreID = reader.GetInt32(0),
                        StoreFrontName = reader.GetString(1), //in sql the starting point is also 0
                        StoreFrontAddress = reader.GetString(2)
                        // Inventory = listname.Add(reader.GetString(0));-----------------------
                    });
                }
            }
            return listOfStores;
        }
        // 3-2-------------------------------------------------------------------------------------------------
        public List<LineItem> GetAllLineItems(string p_name)
        {
            List<LineItem> listOfLineItems = new List<LineItem>();
            string sqlQuery = @"SELECT product.product_name, inventory.quantity
                                from product
                                inner join Inventory
                                on product.product_id = inventory.inventory_productid
                                inner join storefront
                                on storefront.storefront_id = inventory.inventory_storeid 
                                WHERE storefront.storefront_name = @p_nameinput;";


            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);

                command.Parameters.AddWithValue("@p_nameinput", p_name);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfLineItems.Add(new LineItem()
                    {
                        Product = reader.GetString(0),
                        Quantity = reader.GetInt32(1)
                    });
                }
            }
            return listOfLineItems;
        }
        //4.Place Order--------------------------------------------------------------------------------------------

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
        public List<Product> GetAllProducts() //4-3
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

        // 5. view order history---------------------------------------------------------------------------------------------------\\
        public List<Order> GetCustomerOrderByID(int p_custid)
        {
            List<Order> listOfOrders = new List<Order>();

            string sqlQuery = @"select o.orders_customerid , o.order_id, p.product_name, p.product_price 
                                from orders o 
                                inner join lineitem l 
                                on o.order_id = l.orderid 
                                INNER join product p 
                                on l.product_id = p.product_id 
                                where o.orders_customerid =  @custidinput";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@custidinput", p_custid);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfOrders.Add(new Order()
                    {
                        //Reader column is NOT based on table structure but based on what your select query statement is displaying
                        ID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        Product = reader.GetString(2),
                        Price = reader.GetDecimal(3)
                    });
                }
            }
            return listOfOrders;
        }

        //6.------------------------------------------------------------------------------------------

        public LineItem ReplenishInventory(LineItem p_lineitem)
        {

            string sqlQuery = @"update Inventory 
                                set quantity = quantity + @_quantity
                                where inventory_productid  = @_productid;";


            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens the connection

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon); //command will hold sql query that will execute on the current connection we have on the sqlcon object
                // command.Parameters.AddWithValue("@customer_id", p_customer.ID); //should auto generate
                command.Parameters.AddWithValue("@_productid", p_lineitem.LineItemID);
                command.Parameters.AddWithValue("@_quantity", p_lineitem.Quantity);

                command.ExecuteNonQuery();
            }
            return p_lineitem;

        }

        public List<Inventory> GetAllInventory() //4-3
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

        public void PlaceOrder(int p_custid, int p_storeid, List<LineItem> _cart, decimal p_totalprice)
        {
            string sqlQuery = @"insert into orders
                                values(@customerid, @storeid, @price);
                                select scope_identity();";

            string sqlQuery2 = @"insert into lineitem 
                                values (@orderid, @productid, @quantity);";

            string sqlQuery3 = @"update Inventory 
                                set quantity = quantity - @quantity
                                where inventory_storeid = @storeid
                                AND inventory_productid = @productid;";


            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens the connection

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon);
                command.Parameters.AddWithValue("@customerid", p_custid);
                command.Parameters.AddWithValue("@storeid", p_storeid);
                command.Parameters.AddWithValue("@price", p_totalprice);

                int _orderid = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item in _cart)
                {
                    SqlCommand command2 = new SqlCommand(sqlQuery2, sqlcon);
                    command2.Parameters.AddWithValue("@orderid", _orderid);
                    command2.Parameters.AddWithValue("@productid", item.ProductID);
                    command2.Parameters.AddWithValue("@quantity", item.Quantity);
                    command2.ExecuteNonQuery();

                    SqlCommand command3 = new SqlCommand(sqlQuery3, sqlcon);
                    command3.Parameters.AddWithValue("@storeid", p_storeid);
                    command3.Parameters.AddWithValue("@productid", item.ProductID);
                    command3.Parameters.AddWithValue("@quantity", item.Quantity);
                    command3.ExecuteNonQuery();
                }
            }


        }

        public User RegisterUser(User p_user)
        {
            string sqlQuery = @"INSERT INTO Userdata
                                            (username, password)
                                            VALUES(@Username, @Password);";

            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens the connection

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon); //command will hold sql query that will execute on the current connection we have on the sqlcon object
                // command.Parameters.AddWithValue("@customer_id", p_customer.ID); //should auto generate
                command.Parameters.AddWithValue("@Username", p_user.Username);
                command.Parameters.AddWithValue("@Password", p_user.Password);

                command.ExecuteNonQuery();
            }
            return p_user;
        }

        public List<User> GetAllUsers()
        {
            List<User> listOfUsers = new List<User>();

            string sqlQuery = @"SELECT username, password
                                FROM Userdata;";

            using (SqlConnection sqlcon = new SqlConnection(_connectionStrings))
            {
                sqlcon.Open(); //opens connection to database

                SqlCommand command = new SqlCommand(sqlQuery, sqlcon); //create command object that has our sqlquery and con object

                SqlDataReader reader = command.ExecuteReader(); // sqlDataReader is a class that helps C# understand tables

                while (reader.Read()) //runs all the rows until there is no mre data from the sql table. If there is another row = true, if not = false
                {
                    listOfUsers.Add(new User()
                    {
                        Username = reader.GetString(0), //in sql the starting point is also 0
                        Password = reader.GetString(1)
                    });
                }
            }
            return listOfUsers;
        }
        //-------------------------------------------------------------------------------------------------------

    }
}
