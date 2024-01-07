namespace productManagement.application.output.seedWork
{
    public interface IPaging<TDTO>
    {
        public IEnumerable<TDTO> Records { get; }
        public int Page { get; }
        public int RecordsPerPage { get; }
        public int TotalRecords { get; }
    }
}
