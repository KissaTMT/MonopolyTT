namespace Accounting
{
    public enum TransactionType
    {
        Income,
        Expense
    }
    public class Transaction
    {
        public int ID { get; private set; }
        public DateTime DateTime { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public string Description {  get; private set; } = string.Empty;

        public Transaction(decimal amount, TransactionType type):
            this(Guid.NewGuid().GetHashCode(), DateTime.Now, amount, type)
        { }
        public Transaction(DateTime dateTime, decimal amount, TransactionType type) :
            this(Guid.NewGuid().GetHashCode(), dateTime, amount, type)
        { }
        public Transaction(int id, DateTime dateTime, decimal amount, TransactionType type) :
            this(id, dateTime,amount,type,string.Empty)
        { }
        public Transaction(int id, DateTime dateTime, decimal amount, TransactionType type, string description)
        {
            ID = id;
            DateTime = dateTime;
            Amount = amount;
            Type = type;
            Description = description;
        }
        public override string ToString() => $"{ID} {DateTime} {Amount} {Type}";
    }
}
