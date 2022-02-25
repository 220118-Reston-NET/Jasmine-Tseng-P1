namespace StoreModel
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

        private List<Order> _orders;
        public List<Order> _Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
            }
        }
        public Customer() //default values
        {
            ID = 0;
            Name = "FirstName LastName";
            Address = "1111 Address St";
            Phone = "000-000-0000";
            _orders = new List<Order>()
            {
                new Order()
            };
        }

        //ToString() method is the string version of your object, it is what displays after it gets searched
        public override string ToString()
        {
            return $"ID: {ID}\nName: {Name}\nAddress: {Address}\nPhone: {Phone}";
        }
    }

}