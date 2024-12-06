using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{

    public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSucess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand query, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(query.UserName, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
