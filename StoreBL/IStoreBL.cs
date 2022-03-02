using StoreModel;

namespace BL
{
    /// <summary>
    /// Business Layer is responsible for further validation or processing of data obtained from either the database or the user
    /// What kind of processing? That all depends on the type of functionality you will be doing
    /// </summary>
    public interface IStoreBL
    {
        /// <summary>
        /// Will add a product data to the database
        /// </summary>
        /// <param name="pp_product"></param>
        /// <returns></returns>
        /// 
        Customer AddCustomer(Customer p_customer); //1.

        /// <summary>
        /// Will give a list of product objects that are related to the searched name
        /// </summary>
        /// <param name="p_name">Name parameter being used to filter our product</param>
        /// <returns>Gives a filtered list of product via the name</returns>

        List<Customer> SearchCustomers(string p_name); //2
        List<StoreFront> SearchStoreFronts(string s_name); //3 - 1
        List<LineItem> GetAllLineItemsByOrderID(int p_orderID); // 3 - 2
        List<StoreFront> GetAllStores();  //4 - 2
        List<Product> GetAllProducts(); //4 - 3
        List<Inventory> GetAllInventory(); // 4- 3
        List<Product> GetAllProductsByStoreID(int p_storeid); //4 - 3 - 4
        List<Inventory> GetAllInventoryByStoreID(int p_storeid); //4 - 3
        List<Customer> GetAllCustomers(); // 5 - 1
        void PlaceOrder(Order p_order);
        // List<Order> GetCustomerOrderByID(int p_custid); // 5 -2
        // List<Order> GetStoreOrderHistoryByID(int p_storeid);
        Inventory ReplenishInventory(Inventory p_inventory); // 6
        User RegisterUser(User p_user);
        bool Login(User p_user);

        void AssignMangerRoleToUser(string p_username);
        List<Order> GetAllOrders();
        List<CartDetails> GetAllCartByOrderID(int p_orderID);
    }
}

