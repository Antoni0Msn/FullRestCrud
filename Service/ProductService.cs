using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication3.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication3.Service
{
    public class ProductService
    {
        private DbContextToConnect _db;

        public ProductService(DbContextToConnect Db)
        {
            _db = Db;
        }
        public Exception errorException;

        public async Task<List<ProductClass>> GetAll()
        {
                List<ProductClass> products = await _db.productClasses.ToListAsync();

                if (products.Count > 0)
                {
                    return products;
                }
                return null;            
        }
        public async Task<ProductClass> GetByIdService(int Id)
        {
            var product = await _db.productClasses.FirstOrDefaultAsync(p => p.Id == Id);
            if(product != null)
            {
                return product;
            }
            return null;
        }
        public async Task<List<ProductClass>> CreateProductService(ProductClass Produto)
        {
            try
            {
                _db.productClasses.Add(Produto);
                await _db.SaveChangesAsync();
                return await GetAll();
            }
            catch (Exception erroCreate)
            {
                errorException = erroCreate;
                return null;
            }
        }
        public async Task<ProductClass> UpdateService(int id, ProductClass newProduct)
        {
            var product = await GetByIdService(id);
            if (product != null)
            {
                try
                {
                    product.Name = newProduct.Name;
                    product.Description = newProduct.Description;
                    await _db.SaveChangesAsync();
                    return product;
                }
                catch (Exception ex)
                {
                    errorException = ex;
                    return null;
                }
            }
            return null;
        }
        public async Task<List<ProductClass>> DeleteService(int id)
        {
            var productToDelete = await GetByIdService(id);
            if (productToDelete != null)
            {
                try
                {
                    _db.Remove(productToDelete);
                    await _db.SaveChangesAsync();
                    return await GetAll();
                }
                catch (Exception ex)
                {
                    errorException = ex;
                    return null;
                }
            }
            return null;
        }


    }
}
