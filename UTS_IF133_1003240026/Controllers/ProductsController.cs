using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTS_IF133_1003240026.Data;
using UTS_IF133_1003240026.DTOs;
using UTS_IF133_1003240026.Models;

namespace UTS_IF133_1003240026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _context.Products.AsNoTracking().ToListAsync();
        return Ok(products);
    }

    // GET: api/products/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return NotFound(new { message = $"Produk dengan ID {id} tidak ditemukan." });

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductDto dto)
    {
        var isDuplicate = await _context.Products.AnyAsync(p => p.Code == dto.Code);
        if (isDuplicate)
            return Conflict(new { message = $"Produk dengan Code '{dto.Code}' sudah ada." });

        var product = new Product
        {
            Code = dto.Code,
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            IsActive = dto.IsActive
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    // PUT: api/products/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return NotFound(new { message = $"Produk dengan ID {id} tidak ditemukan." });

        product.Code = dto.Code;
        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return NotFound(new { message = $"Produk dengan ID {id} tidak ditemukan." });

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}