public class ProductDtoPost
    {
        public string? prname { get; set; }
        public string? prdesc { get; set; }
        public string? prprice { get; set; }
        public string? prdiscount { get; set; }
        public DateTime prupdatedate { get; set; }
        public IFormFile file { get; set; }
    }
