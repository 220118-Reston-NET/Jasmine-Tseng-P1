namespace StoreModel

{
    public class Product
    {
        int _quantity;
        public int ItemID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }

        // public int Quantity {
        //     get { return _quantity; }
        //     set 
        //     {
        //         if (value > 0)
        //         {
        //             _quantity = value; 
        //         }
        //         else
        //         {
        //             throw new Exception("You cannot have a negative quantity!");
        //         }
        //     }
        // }
        //Default constructor to add default values to the properties
        public Product()
        {
            Name = "Unknown";
            Price = 0.00m;
            Color = "Unknown";
            Quantity = 0;

        }

        //ToString() method is the string version of your object
        // public override string ToString()
        // {
        //     return $"Name: {Name}\nPrice: ${Price}\nColor: {Color}\nQuantity: {Quantity}";
        // }
    }
}

