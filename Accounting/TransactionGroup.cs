namespace Accounting
{
    public class TransactionGroup
    {
        public TransactionType Type { get; set; }
        public decimal TotalAmount { get; set; }
        public IReadOnlyList<Transaction>? Transactions { get; set; } = null;
    }
}