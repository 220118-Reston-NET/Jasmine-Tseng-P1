namespace StoreModel
{

    public class Order
    {
        private int _quantity;
        private decimal _totalPrice = 0.00m;
        public int ID { get; set; }
        public string StoreFrontName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public string Product { get; set; }
        public DateTime DateCreated { get; set; }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value > 0)
                {
                    _quantity = value;
                }
                else
                {
                    throw new Exception("You cannot have a negative quantity!");
                }
            }
        }

        public decimal TotalPrice { get; set; }


        //   private List<Product> p_product;
        //   public List<Product> Product
        //   { 
        //       get { return p_product; }
        //       set
        //       {
        //           p_product = value;
        //       }
        //}
        //    public override string ToString()
        //     {
        //         return $"Customer ID: {ID}\nOrder ID: {CustomerID}\nProduct: {Product}\nPrice: ${string.Format("{0:f2}", Price)}";
        //     }  

    }


}