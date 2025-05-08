using Shared.DataTransferObject;
using System;
using Shared;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? BrandId, int? TypeId, ProductSortingOptions sortingOption); 
        Task<ProductDto> GetProductByIdAsync(int Id);
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
