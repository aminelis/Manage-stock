using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using db_Api.Models.Generated.Product;
using db_Api.Models.Generated;
using Microsoft.AspNetCore.Authorization;

namespace VotreNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly Db_PartialContext _context;

        public ProductController(Db_PartialContext context)
        {
            _context = context;
        }

        [HttpPost("AddProduct")]
        [Authorize] 
        public async Task<ActionResult<NumeroPdt>> AddProduct([FromForm] AddPdtModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérifiez si le produit existe déjà dans la base de données
            bool productExists = await IsProductExistsAsync(model.CodePdt);
            if (productExists)
            {
                return Conflict("Le code de produit existe déjà.");
            }

            // Créez un nouvel produit avec les informations fournies
            var newProduit = new ProduitModel
            {
                CodePdt = model.CodePdt,
                Produit = model.Produit,
                Prix = model.Prix
            };

            try
            {
                // Ajoutez le nouveau produit à la base de données
               var items = _context.products.Add(newProduit);
                await _context.SaveChangesAsync();
                NumeroPdt NumeroPdt = new NumeroPdt();
                NumeroPdt.CodePdt = newProduit.CodePdt;
                return NumeroPdt;


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Méthode pour vérifier si le produit existe déjà dans la base de données
        private async Task<bool> IsProductExistsAsync(string CodePdt)
        {
            return await _context.products.AnyAsync(u => u.CodePdt == CodePdt);
        }

        [HttpDelete("DeleteProduct/{Id}")]
public async Task<ActionResult> DeleteProduct(int Id)
{
    var product = await _context.products.FirstOrDefaultAsync(p => p.Id == Id);
    if (product == null)
    {
        return NotFound("Produit non trouvé.");
    }

    _context.products.Remove(product);
    await _context.SaveChangesAsync();

    return Ok("Produit supprimé avec succès.");
}

[HttpPut("UpdateProduct/{Id}")]
public async Task<ActionResult> UpdateProduct(int Id, [FromBody] AddPdtModel updatedProduct)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }


    var product = await _context.products.FirstOrDefaultAsync(p => p.Id == Id);
    if (product == null)
    {
        return NotFound("Produit non trouvé.");
    }

    product.CodePdt = updatedProduct.CodePdt;
    product.Produit = updatedProduct.Produit;
    product.Prix = updatedProduct.Prix;

    try
    {
        _context.products.Update(product);
        await _context.SaveChangesAsync();
        return Ok("Produit mis à jour avec succès.");
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
}
[HttpGet("GetProductsById")]
[Authorize]
public async Task<ActionResult<List<ProduitModel>>> GetProductsById()
{

    var user = HttpContext.User;
    if (!user.Identity.IsAuthenticated)
    {
        return Unauthorized("Token JWT manquant ou invalide.");
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var products = await _context.products.ToListAsync();
    
    if (products == null || products.Count == 0)
    {
        return NotFound("Aucun produit trouvé.");
    }

    return products;
}


    }
}
