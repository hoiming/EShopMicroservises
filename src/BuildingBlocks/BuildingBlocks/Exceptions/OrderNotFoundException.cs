



namespace BuildingBlocks.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid id) : base("Order", id)
        {
            
        }


    }
}
