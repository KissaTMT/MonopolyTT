namespace Accounting
{
    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }
    public class Wallet
    {
        public int ID { get; set; }
        public IReadOnlyList<Transaction> Transactions => _transactions;
        public string Name => _name;
        public string Currency => _currency;

        private List<Transaction> _transactions;

        private readonly string _name;
        private readonly string _currency;
        private readonly decimal _initialBalance;
        public Wallet(string name, string currency, decimal initialBalance = 0):
            this(Guid.NewGuid().GetHashCode(), name, currency, initialBalance)
        { }
        public Wallet(int id, string name, string currency, decimal initialBalance = 0)
        {
            ID = id;
            _name = name;
            _currency = currency;
            _initialBalance = initialBalance;

            _transactions = new();
        }
        public decimal GetCurrentBalance()
        {
            return _initialBalance + _transactions.Where(i => i.Type == TransactionType.Income).Sum(i => i.Amount) 
                - _transactions.Where(i => i.Type == TransactionType.Expense).Sum(i => i.Amount);
        }
        public bool AddTransaction(Transaction transaction)
        {
            if(transaction.Type == TransactionType.Expense && transaction.Amount > GetCurrentBalance()) return false;

            _transactions.Add(transaction);
            return true;
        }
        public IReadOnlyList<Transaction> GetTop3ExpensesForMonth(Month month, int year)
        {
            return _transactions
                .Where(i => i.Type == TransactionType.Expense &&
                           i.DateTime.Month == (int)month &&
                           i.DateTime.Year == year)
                .OrderByDescending(t => t.Amount)
                .Take(3)
                .ToList();
        }
        public IReadOnlyList<TransactionGroup> GetGroupTransactionsByMonth(Month month, int year)
        {
            return _transactions
                .Where(i => i.DateTime.Year == year && i.DateTime.Month == (int)month)
                .GroupBy(i => i.Type)
                .Select(i => new TransactionGroup
                {
                    Type = i.Key,
                    TotalAmount = i.Sum(i => i.Amount),
                    Transactions = i.OrderBy(i => i.DateTime).ToList()
                })
                .OrderByDescending(g => g.TotalAmount)
                .ToList();
        }
        public decimal GetTotalAmountTransactions(DateTime start, DateTime end, TransactionType type)
        {
            return _transactions.Where(i => i.Type == type && i.DateTime >= start && i.DateTime <= end).Sum(i => i.Amount);
        }
        public override string ToString() => $"{ID} {_name} {GetCurrentBalance()}";
        
    }
}
