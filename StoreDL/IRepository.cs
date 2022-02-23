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
        void PlaceOrder(int p_custid, int p_storeid, List<LineItem> _cart, decimal p_totalprice);
        List<LineItem> GetAllLineItems(string p_name); // 3 - 2
        List<Order> GetCustomerOrderByID(int p_custid); //5. View order history
        LineItem ReplenishInventory(LineItem p_lineitem); // 6
    }
}

