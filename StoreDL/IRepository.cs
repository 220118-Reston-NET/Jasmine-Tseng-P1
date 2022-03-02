using StoreModel;

namespace StoreDL
{
    public interface IRepository
    {

        // Product AddItem(Product pp_item);

        // List<Product> GetAllProduct();

        Customer AddCustomer(Customer p_customer); // 1. Add a Customer
        List<Customer> GetAllCustomers(); // 2. Search a Customer
        List<StoreFront> GetAllStores(); // 3 - 1
        List<Product> GetAllProducts(); //3-2
        List<Inventory> GetAllInventory(); //4
        void PlaceOrder(Order p_order);
        List<LineItem> GetAllLineItemsByOrderID(int p_orderID); // 3 - 2
        // List<Order> GetCustomerOrderByID(int p_custid); //5. View Customer order history
        // List<Order> GetStoreOrderHistoryByID(int p_storeid); // View Store Order History
        List<Order> GetAllOrders();
        Inventory ReplenishInventory(Inventory p_inventory); // 6
        User RegisterUser(User p_user);
        List<User> GetAllUsers();

        void AssignMangerRoleToUser(string p_username);
        List<CartDetails> GetAllCartByOrderID(int p_orderID);
    }
}

