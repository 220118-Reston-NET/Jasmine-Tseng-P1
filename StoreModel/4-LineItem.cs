namespace StoreModel
{
    public class LineItem
    {
        private int _quantity;

        public int OrderID { get; set; }
        public int ProductID { get; set; }
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
    }
}