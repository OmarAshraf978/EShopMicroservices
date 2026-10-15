namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    public class GetProductsByCategoryHandle(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>()
                                       .Where(c => c.Category.Contains(request.Category))
                                       .ToListAsync(cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException($"Product With Category : {request.Category} Not Found");
            }

            return new GetProductsByCategoryResult(product);
        }
    }
}
