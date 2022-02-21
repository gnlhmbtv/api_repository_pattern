using BLL.DTO.Product;
using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repo
{
    public interface IProduct
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProduct(int id);
        Task<Product> PutProduct(int id, Product product);
        Task<Product> PostProduct(ProductDto productDto);
        Task<Product> DeleteProduct(int id);
    }
}
