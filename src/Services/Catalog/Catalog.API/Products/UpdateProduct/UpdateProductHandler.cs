
namespace Catalog.API.Products.UpdateProduct
{
    
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string ImageFile, decimal Price, string Description)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler is invoked with {@Query}.", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException();
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
