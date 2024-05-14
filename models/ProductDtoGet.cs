public class ProductDtoGet
    {
        public int prid { get; set; }
        public string? prname { get; set; }
        public string? prdesc { get; set; }
        public string? prprice { get; set; }
        public string? prdiscount { get; set; }
        public DateTime prupdatedate { get; set; }
        public string? primage { get; set; }
        public byte[] ImageData { get; set; }
    }
