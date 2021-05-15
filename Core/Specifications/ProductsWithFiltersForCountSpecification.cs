using Core.Entities;

namespace Core.Specifications
{
  public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
  {
    public ProductsWithFiltersForCountSpecification(ProductsSpecParams productParams)
    : base(x =>
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    )
    {

    }
  }
}
