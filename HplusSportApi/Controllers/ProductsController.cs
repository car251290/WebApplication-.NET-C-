using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HplusSportApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HplusSportApi.Controllers
{
    [Route("/products/{id}")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;
        //THIS IS THE CONSTRACTOR of the shop contact 
        public ProductsController(ShopContext context)
        {

            //using this ensurecreated to ensure that you have create an point of the data an can be retributed to the code
            _context = context;
            //this is for see the data
            _context.Database.EnsureCreated();

        }
        [HttpGet("{id}")]

        public IActionResult GetAllProducts(int id)
        {
            //return all  the products and will show on an array
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return OK(product);



        }

        private IActionResult OK(object product)
        {
            throw new NotImplementedException();
        }


    }


}
