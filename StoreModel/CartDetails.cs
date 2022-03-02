using StoreModel;

namespace StoreModel
{
    public class CartDetails
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}