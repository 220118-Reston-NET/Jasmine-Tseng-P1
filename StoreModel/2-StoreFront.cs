namespace StoreModel
{
    public class StoreFront
    {
        public int StoreID { get; set; }
        public string StoreFrontName { get; set; }
        public string StoreFrontAddress { get; set; }

        private List<Product> p_inventory;
        public List<Product> Inventory
        {
            get { return p_inventory; }
            set
            {
                p_inventory = value;
            }
        }
        public StoreFront() //default values
        {
            StoreFrontName = "Store Name";
            StoreFrontAddress = "1111 Address St, City, State 12345";
            p_inventory = new List<Product>()
            {
                new Product()
            };
        }



        //ToString() method is the string version of your object

    }

}