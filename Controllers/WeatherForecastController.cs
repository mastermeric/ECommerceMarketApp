using Microsoft.AspNetCore.Mvc;

namespace ECommerceMarketApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ECommerceController : ControllerBase
{    

    private readonly ILogger<ECommerceController> _logger;

    public ECommerceController(ILogger<ECommerceController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetECommerce")]
    public String Get(String prm)
    {
        return "GET API.. : " + prm;
    }


    [HttpPost(Name = "PostECommerce")]
    public String Post(String productName, int productPrice , int discount)
    {
        // Business Logic ------------------------        
        discount = discount + 10;
        productPrice = productPrice - discount;
        //----------------------------------------

        
        return "POST API..  ÜRÜN: " + productName + " Fiyat : " + productPrice.ToString() + " İndirim : " +discount  ;
    }

    [HttpDelete(Name = "DeleteECommerce")]
    public String Delete(String prm)
    {
        return "DELETE API.. " + prm;
    }
}
