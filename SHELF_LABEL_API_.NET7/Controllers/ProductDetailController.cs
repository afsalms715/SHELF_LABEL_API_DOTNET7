using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHELF_LABEL_API_.NET7.Services;
using SHELF_LABEL_API_NET7.Models;

namespace SHELF_LABEL_API_NET7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        ProductDtlService productDtlService;
        public ProductDetailController(ProductDtlService productDtlService)
        {
            this.productDtlService = productDtlService;
        }
        [HttpGet("product")]
        public ActionResult<ProductDtlModel> Get(string barcode, string loc)
        {
            if (barcode == "" || barcode.Length < 4 || loc == "")
            {
                return BadRequest();
            }
            var result = productDtlService.GetProduct(barcode, loc);
            if (result.Su_desc == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
