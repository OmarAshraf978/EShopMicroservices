
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand
    (
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    ) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id Is Required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required")
                                .Length(2, 150).WithMessage("Name Must Between 2 and 150 Characters");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category Is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile Is Required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description Is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must Be Greater Than 0");
        }
    }

    public class UpdateProductHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
