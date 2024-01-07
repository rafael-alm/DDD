using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.seedWork.entities.interfaces
{
    public class ResultValidateEntity
    {
        public bool IsValid { get; init; }
        public IMessageNotification Message { get; init; }
        private ResultValidateEntity(bool isValid, IMessageNotification message)
        {
            IsValid = isValid;
            Message = message;
        }

        public static ResultValidateEntity New(bool isValid, IMessageNotification message)
            => new ResultValidateEntity(isValid, message);
    }
}
