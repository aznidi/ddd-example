using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions
{
    public class InvalidMoneyException : DomainException
    {
        public InvalidMoneyException(
            string message
        ) : base(message) { }

        
    }
}