public class Product
    {
        public int prid { get; set; }
        public string? prname { get; set; }
        public string? prdesc { get; set; }
        public string? prprice { get; set; }
        public string? prdiscount { get; set; }
        public DateTime prupdatedate { get; set; }

        public Product()
        {
            prprice = "0000";
            prupdatedate = DateTime.Now;
        }
    }
