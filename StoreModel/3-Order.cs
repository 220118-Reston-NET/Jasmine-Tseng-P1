namespace StoreModel
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int StoreID { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public List<LineItem> Cart { get; set; }
    }
}

