using StoreModel;

namespace StoreApi.DataTransferObject
{
    public class OrderDetails
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public CustomerDetail CustomerDetail { get; set; }
        public int StoreID { get; set; }
        public StoreFrontDetails StoreDetail { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public List<CartDetails> Cart { get; set; }
    }
}