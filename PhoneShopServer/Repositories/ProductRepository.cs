using Microsoft.EntityFrameworkCore;
using PhoneShopServer.Data;
using PhoneShopSharedLib.Models;
using PhoneShopSharedLib.Responses;
using PhoneShopSharedLib.Contracts;
using System.Transactions;

namespace PhoneShopServer.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly AppDbContext appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<ServiceResponse> AddProduct(Product model)
        {
            if (model == null)
            {
                return new ServiceResponse(false, "Model is null");
            }
            var (flag, message) = await CheckName(model.Name!);
            if(flag)
            {
                appDbContext.Products.Add(model);
                await Commit();
                return new ServiceResponse(true, "Saved");
            }
            return new ServiceResponse(flag, message);
        }

        private async Task<ServiceResponse> CheckName(string name)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
            return product is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Product already exist");
        }

        private async Task Commit()=> await appDbContext.SaveChangesAsync();
    }

}
