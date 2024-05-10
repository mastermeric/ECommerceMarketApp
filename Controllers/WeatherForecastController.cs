using ECommerceMarketApp.repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceMarketApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ECommerceController : ControllerBase
{    

    private readonly ILogger<ECommerceController> _logger;
    private readonly IProductRepository _productRepository;

    public ECommerceController(ILogger<ECommerceController> logger ,IProductRepository productRepository )
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    // [HttpGet(Name = "GetECommerce")]
    // public String Get(String prm)
    // {
    //     return "GET API.. : " + prm;
    // }

    // [HttpPost(Name = "PostECommerce")]
    // public String Post(String productName, int productPrice , int discount)
    // {
    //     // Business Logic ------------------------        
    //     discount = discount + 10;
    //     productPrice = productPrice - discount;
    //     //----------------------------------------        
    //     return "POST API..  ÜRÜN: " + productName + " Fiyat : " + productPrice.ToString() + " İndirim : " +discount  ;
    // }

    // [HttpDelete(Name = "DeleteECommerce")]
    // public String Delete(String prm)
    // {
    //     return "DELETE API.. " + prm;
    // }

    [EnableRateLimiting("User")]
    [HttpGet("GetByProductId/{productId}")]
    public async Task<IActionResult> GetByProductId(string productId)
    {
        try
        {
            //IEnumerable<Arac> arac = await _aracRepo.GetAracByUserKod(userkodu);
            // if (arac == null || arac.Count() <= 0)
            //     return NotFound();
            // return Ok(arac);
            return Ok("Tamamdır..." + productId + " geldi..");
        }
        catch (Exception ex)
        {
            //log error
            //_multiThreadFileWriter.WriteLine("EXCEPTION at GetAracByUserKod -> " + userkodu + " -> " + ex.Message + " -> " + ex.InnerException);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            //IEnumerable<Arac> arac = await _aracRepo.GetAracByUserKod(userkodu);
            // if (arac == null || arac.Count() <= 0)
            //     return NotFound();
            // return Ok(arac);
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }
        catch (Exception ex)
        {
            //log error
            //_multiThreadFileWriter.WriteLine("EXCEPTION at GetAracByUserKod -> " + userkodu + " -> " + ex.Message + " -> " + ex.InnerException);            
            return StatusCode(500, " OLMADI ! " + ex.Message + ex.InnerException);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> CreateArac([FromBody] Product product)
    {
        try
        {
            var createdProduct = await  _productRepository.CreateProduct(product);
            // Retrieve amacli tekrar GET calistir !
            return CreatedAtRoute("CreateArac", new { aracId = createdProduct.prid}, createdProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut()]
    //public async Task<IActionResult> UpdateArac(int aracId, [FromBody] string PlakaNo, [FromBody] string HizLimit)
    public async Task<IActionResult> UpdateArac(Product product)
    {
        try
        {
            var item = await _productRepository.GetProductById(product.prid);
            if (item == null)
                return NotFound();
            await _productRepository.UpdateProduct(item.prid, item);
            return Ok("UPDATE Başarılı !");
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
}
