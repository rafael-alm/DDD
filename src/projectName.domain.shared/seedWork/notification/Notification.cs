using projectName.domain.shared.seedWork.exceptions;

namespace projectName.domain.shared.seedWork.notification
{
    public class Notification : INotification
    {
        private List<IMessageNotification> errors;

        private Notification()
            => errors = new List<IMessageNotification>();

        public void Add(IMessageNotification message)
            => errors.Add(message);

        public void AddIfFalse(bool argument, IMessageNotification message)
        {
            if (!argument) errors.Add(message);
        }

        public void ThrowExceptionIfError()
        {
            if (HasError)
                throw new EntityValidationException("An error occurred when executing the command", errors);
        }

        public IReadOnlyCollection<IMessageNotification> Errors
            => errors.AsReadOnly();

        public bool HasError
            => errors.Any();

        public static INotification New()
            => new Notification();
    }
}