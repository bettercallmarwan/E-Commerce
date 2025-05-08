using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Shared;
namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecification(int? BrandId, int? TypeId, ProductSortingOptions sortingOption)
            : base(P => (!BrandId.HasValue || P.BrandId == BrandId)
            && (!TypeId.HasValue || P.TypeId == TypeId))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);


            switch (sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescebding(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescebding(P => P.Price);
                    break;
                default:
                    break;
            }
        }

        public ProductWithBrandAndTypeSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}