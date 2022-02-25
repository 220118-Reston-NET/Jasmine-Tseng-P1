using StoreDL;
using StoreModel;
using System.Linq;

namespace BL
{
    public class StoreBL : IStoreBL
    {

        //Dependency Injection Pattern
        //- This is the main reason why we created interface first before the class
        //- This will save you time on re-writting code that breaks if you updated a completely separate class
        //- Main reason is to prevent us from re-writting code that already existed on (potentailly) 50 files or more without
        //the compiler helping us
        //===========================
        private IRepository _repo;
        public StoreBL(IRepository p_repo)
        {
            _repo = p_repo;
        }
        //============================


        public Customer AddCustomer(Customer p_customer) //1. Add Customer
        {
            Console.WriteLine("Customer Added Successfully.");
            return _repo.AddCustomer(p_customer); // adds the input into the database. It's okay to end the BL here
        }
        //for searching customers function

        public List<Customer> SearchCustomers(string p_name) //2. Search Customer
        {
            List<Customer> listOfCustomers = _repo.GetAllCustomers(); //BL depends on _repo to get the info back from the DB

            // foreach (Customer cust in listOfCustomers) 
            // {
            //     if (cust.Name.Contains(p_name)) //.contains is not literal like "=="
            //     {
            //         //add to a list 
            //     }
            // }

            //-----linq library-----

            return listOfCustomers
                   .Where(cust => cust.Name.ToLower().Contains(p_name))
                   .ToList(); //Converts data back to a list type which the method is calling

            // return listOfCustomers
            //             .Where(cust => cust.Name.ToLower().Contains(p_name))
            //             .ToList(); //Converts data back to a list type which the method is calling
        }

        public List<StoreFront> SearchStoreFronts(string s_name) //3 View Store Front
        {
            List<StoreFront> listOfStores = _repo.GetAllStores();

            return listOfStores
            .Where(store => store.StoreFrontName.ToLower().Contains(s_name))
            .ToList();
        }
        public List<LineItem> ViewStoreInventory(string p_name) //3 - 2
        {
            // List<LineItem> listOfLineItems = _repo.GetAllLineItems();

            // return listOfLineItems
            // .Where(lineitem => lineitem.Store.Contains(p_name))
            // .ToList();

            return _repo.GetAllLineItems(p_name.ToLower()); // This works
        }

        public List<Customer> GetAllCustomers() // 5
        {
            return _repo.GetAllCustomers();
        }

        // public List<Customer> SearchCustomerLoginID(int p_LogID) //4-1. Place Order - Login
        // {
        //     List<Customer> listOfCustomers = _repo.GetAllCustomers();


        // foreach (Customer cust in listOfCustomers) 
        //     {
        //         if (p_LogID == cust.ID) //.contains is not literal like "=="
        //         {
        //             return Name;
        //         }
        //     }
        // }
        public List<StoreFront> GetAllStores() //4 - 2
        {
            return _repo.GetAllStores();
        }


        public List<Order> GetCustomerOrderByID(int p_custid) //5. View Customer Order
        {
            return _repo.GetCustomerOrderByID(p_custid);
        }

        public LineItem ReplenishInventory(LineItem p_lineitem) // 6. Replenish Inventory
        {
            Console.WriteLine("Item Added Successfully.");
            return _repo.ReplenishInventory(p_lineitem);
        }

        public List<Product> GetAllProducts() //4-3
        {
            return _repo.GetAllProducts();
        }

        public List<Inventory> GetAllInventory() //4-3
        {
            return _repo.GetAllInventory();
        }

        public List<Product> GetAllProductsByStoreID(int p_storeid)
        {
            List<Product> ListOfProducts = new List<Product>();
            Product _prod = new Product();
            foreach (var item in GetAllInventoryByStoreID(p_storeid))
            {
                _prod = GetAllProducts().Find(p => p.ItemID == item.ProductID);
                ListOfProducts.Add(_prod);

            }
            return ListOfProducts;
        }

        public List<Inventory> GetAllInventoryByStoreID(int p_storeid) //4
        {
            return GetAllInventory().FindAll(p => p.StoreID == p_storeid);
        }

        public void PlaceOrder(int p_custid, int p_storeid, List<LineItem> _cart, decimal p_totalprice)
        {
            Console.WriteLine("Your Order has been placed successfully!");
            _repo.PlaceOrder(p_custid, p_storeid, _cart, p_totalprice);
        }

        public User RegisterUser(User p_user)
        {
            if (_repo.GetAllUsers().All(p => p.Username != p_user.Username))
            {
                return _repo.RegisterUser(p_user);

            }

            throw new Exception("Username already exists.");

        }

        public bool Login(User p_user)
        {
            if (_repo.GetAllUsers().All(p => p.Username != p_user.Username))
            {
                return false;
            }
            else
            {
                if (_repo.GetAllUsers().Find(p => p.Username == p_user.Username).Password == p_user.Password)
                {

                    return true;
                }
            }
            return false;
        }
    }
}

