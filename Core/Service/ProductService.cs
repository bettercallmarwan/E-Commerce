using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObject;
using Shared;
using DomainLayer.Exceptions;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);

            return BrandsDto;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Specifications = new ProductWithBrandAndTypeSpecification(queryParams);
            var Products = await Repo.GetAllAsync(Specifications); var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);

            var ProductCount = Products.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(queryParams.PageIndex, ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);

            return TypesDto;
        }  

        public async Task<ProductDto> GetProductByIdAsync(int Id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecification(Id);

            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specifications);

            if (Product is null)
            {
                throw new ProductNotFoundException(Id);
            }
            return _mapper.Map<Product, ProductDto>(Product);

        }
    }
}
