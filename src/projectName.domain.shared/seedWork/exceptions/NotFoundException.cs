namespace projectName.domain.shared.seedWork.exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string? message) : base(message)
        { }

        public static void ThrowIfNull(
            object? @object,
            string exceptionMessage = "Registro não encontrado.")
        {
            if (@object == null)
                throw new NotFoundException(exceptionMessage);
        }
    }
}
