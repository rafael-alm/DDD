namespace projectName.domain.shared.seedWork.notification
{
    public interface INotification
    {
        public bool HasError { get; }
        public IReadOnlyCollection<IMessageNotification> Errors { get; }
        public void Add(IMessageNotification message);
        public void AddIfFalse(bool argument, IMessageNotification message);
        void ThrowExceptionIfError();
    }
}