using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HplusSportApi.Models;
using HplusSport.Api.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HplusSportApi.Controllers
{
    //THIS IS THE CONSTRACTOR of the shop contact 
    //using this ensurecreated to ensure that you have create an point of the data an can be retributed to the code
    //this is for see the data
    //return all  the products and will show on an array


    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/[controller]}")]
    [ApiController]


    public class ProductsV1_0Controller : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsV1_0Controller(ShopContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<product> products = (IQueryable<product>)_context.Products;


            if (queryParameters.MinPrice != null &&
                queryParameters.MaxPrice != null)
            {
                products = products.Where(p => p.Price >= queryParameters.MinPrice.Value &&
                        p.Price <= queryParameters.MaxPrice.Value);
            }
            
            //Impramentating Name on the ITEMS this is for the search tearm
            if (!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(p => p.Sku == queryParameters.Sku);
            }

            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(
                    p => p.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }
            
            // products and search skip
            products = products
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);

            return (IActionResult)Ok(await products.ToArrayAsync());
        }

        [HttpGet("{id}")]
        
        //get that can appear if we dont have the ID
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return (IActionResult)NotFound();
            }
            return Ok(product);
        }
        
        //to post the Products 
        [HttpPost]
        public async Task<ActionResult<product>> PostProduct([FromBody] product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetProduct", new { id = product.Id },
            product
            );
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] product product)
        {
            if (id != product.Id) {
                return (IActionResult)BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Products.Find(id) == null)
                {
                    return (IActionResult)NotFound();
                }
                throw;
            }
            return NotContent();
        }

        private IActionResult NotContent()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<product>> Delete(int id) {
            //this is searching for the product
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;

        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<product>> DeleteMultiple([FromQuery] int[]ids)
        {
            var products = new List<product>();
            foreach (var id in ids)
            {
                //this is searching for the product
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                product.Add(product);

            }

            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();

            return Ok(products);

        }

    }

}
