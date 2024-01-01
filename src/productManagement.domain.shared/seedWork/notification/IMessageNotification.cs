namespace productManagement.domain.shared.seedWork.notification
{
    public interface IMessageNotification
    {
        public string Code { get; }
        public string Text { get; }
    }
}