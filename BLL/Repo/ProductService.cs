using BLL.DTO.Product;
using DLL.Data;
using DLL.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repo
{
    public class ProductService : IProduct
    {
        private readonly Context _context;
        public ProductService(Context context)
        {
            _context = context;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return product;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _context.Products
                    .FirstOrDefaultAsync(b => b.Id == id);

            ProductDto productDtos = new ProductDto()
            {
                Name = product.Name,
                Price = product.Price
            };

            if (product == null)
            {
                return productDtos;
            }

            return productDtos;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<ProductDto> productDtos = new List<ProductDto>();
            var products = await _context.Products.ToListAsync();

            foreach (var item in products)
            {
                ProductDto productDto = new ProductDto
                {
                    Name = item.Name,
                    Price = item.Price
                };
                productDtos.Add(productDto);
            }

            return productDtos;
        }

        public async Task<Product> PostProduct(ProductDto productDto)
        {
            Product product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return product;
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return product;
                }
                else
                {
                    throw;
                }
            }

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
