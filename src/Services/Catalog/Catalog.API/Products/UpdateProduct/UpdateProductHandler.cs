
using BuildingBlocks.Exceptions;

namespace Catalog.API.Products.UpdateProduct
{
    
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string ImageFile, decimal Price, string Description)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
 
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync();

            return new UpdateProductResult(true);

        }
    }
}
