namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand
    (
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    ) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required")
                                .Length(2, 150).WithMessage("Name Must Between 2 and 150 Characters");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category Is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile Is Required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description Is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must Be Greater Than 0");
        }
    }

    public class CreateProductHandler(IDocumentSession session) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = command.Adapt<Product>();

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
