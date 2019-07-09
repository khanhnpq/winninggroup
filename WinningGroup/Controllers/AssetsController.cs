using Microsoft.AspNetCore.Mvc;
using System;
using WinningGroup.Models;
using WinningGroup.Repository;
using WinningGroup.Utility;

namespace WinningGroup.Controllers
{
    [Route("assets/products")]
    [ApiController]
    public class AssetsController : Controller
    {
        private IProductRepository _repository;

        public AssetsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}.json")]
        public ActionResult<CommonJsonDataModel> GetProduct(int id)
        {
            try
            {
                return CommonJsonDataFactory.Create(JsonResponseType.Ok, _repository.GetProductById(id));
            }
            catch (Exception ex)
            {
                return CommonJsonDataFactory.Create(JsonResponseType.BadRequest
                   , errors: ex.Message);
            }
        }
    }
}