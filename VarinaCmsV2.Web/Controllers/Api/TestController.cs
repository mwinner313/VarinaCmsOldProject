//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
//using VarinaCmsV2.Common.WebApi;
//using VarinaCmsV2.Core.Web.ActionFilters;
//using VarinaCmsV2.ViewModel.Entities;

//namespace VarinaCmsV2.Web.Controllers.Api
//{
//    [AllowAnonymous]
//    public class TestController:BaseApiController
//    {
//        public class Product
//        {
//            public int Id { get; set; }
//            public string Name { get; set; }
//            public decimal Price { get; set; }
//        }
//        public TestController()
//        {
            
//        }
//        List<Product> products = new List<Product>();

//        [WebApiEnableEntityValidation]
//        public IHttpActionResult Post(EntityViewModel model)
//        {

//            return Ok();
//        }
       

//        public TestController(List<Product> products)
//        {
//            this.products = products;
//        }
       
//        public IHttpActionResult GetAllProducts()
//        {
//          //  return  Request.CreateResponse(HttpStatusCode.OK,products);
//            return Unauthorized("test message");
//        }

     

//        public IHttpActionResult GetProduct(int id)
//        {
//            var product = products.FirstOrDefault((p) => p.Id == id);
//            if (product == null)
//            {
//                return NotFound();
//            }
//            return Ok(product);
//        }

//        public async Task<IHttpActionResult> GetProductAsync(int id)
//        {
//            return await Task.FromResult(GetProduct(id));
//        }
//    }
//}