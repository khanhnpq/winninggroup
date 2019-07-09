using Microsoft.AspNetCore.Mvc;
using System;
using WinningGroup.Repository;
using WinningGroup.Utility;

namespace WinningGroup.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost()]
        public ActionResult<CommonJsonDataModel> AddProduct()
        {
            try
            {
                return CommonJsonDataFactory.Create(JsonResponseType.Ok, _repository.AddProduct());
            }
            catch (Exception ex)
            {
                return CommonJsonDataFactory.Create(JsonResponseType.BadRequest
                   , errors: ex.Message);
            }
        }
    }
}