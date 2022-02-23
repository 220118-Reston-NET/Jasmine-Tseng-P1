namespace StoreModel
{
    public class LineItem
    {
        private int _quantity;
        private decimal TotalPrice;
        public int LineItemID { get; set; }
        // public string Store { get; set; }
        public string Product { get; set; }
        public int ProductID { get; set; }
        public int Quantity {
            get { return _quantity; }
            set 
            {
                if (value >= 0)
                {
                    _quantity = value; 
                }
                else
                {
                    throw new Exception("You cannot have a negative quantity!");
                }
            }
        }
    
        public LineItem()
            {
                LineItemID = 0;
                Product = "Unknown";
                Quantity = 0;
            
            }
     
        public override string ToString()
            {
                return $"{Product}: {Quantity}\n{TotalPrice}";
            }
    }
}