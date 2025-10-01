namespace Accounting
{
    public class WalletController
    {
        public IReadOnlyDictionary<string, Wallet> Wallets => _wallets;
        private readonly Dictionary<string, Wallet> _wallets;

        public WalletController()
        {
            _wallets = new Dictionary<string, Wallet>();
        }
        public Wallet GetWallet(string name) => _wallets[name];
        public void AddWallet(Wallet wallet) => _wallets.Add(wallet.Name, wallet);
        public bool RemoveWallet(Wallet wallet)
        {
            return RemoveWallet(wallet.Name);
        }
        public bool RemoveWallet(string walletName)
        {
            if (_wallets.ContainsKey(walletName) == false) throw new ArgumentException($"Wallet with name: {walletName} does nit exist ");
            return _wallets.Remove(walletName);
        }
        public bool AddTransaction(string walletName, Transaction transaction)
        {
            if (_wallets.ContainsKey(walletName) == false) throw new ArgumentException($"Wallet with name: {walletName} does nit exist ");

            return _wallets[walletName].AddTransaction(transaction);
        }

        public decimal GetMonthlyIncome(string walletName, Month month)
        {
            return GetMonthlyIncome(walletName,month, DateTime.Today.Year);
        }
        public decimal GetMonthlyIncome(string walletName,Month month, int year)
        {
            var (start, end) = CalculatePeriodForMonth(month, year);
            return GetTotalAmountTransactions(walletName, start, end, TransactionType.Income);
        }
        public decimal GetMonthlyExpense(string walletName, Month month)
        {
            return GetMonthlyExpense(walletName, month, DateTime.Today.Year);
        }
        public decimal GetMonthlyExpense(string walletName, Month month, int year)
        {
            var (start, end) = CalculatePeriodForMonth(month, year);
            return GetTotalAmountTransactions(walletName,start, end, TransactionType.Expense);
        }
        public IReadOnlyList<TransactionGroup> GetGroupTransactionsByMonth(string walletName, Month month)
        {
            return GetGroupTransactionsByMonth(walletName, month, DateTime.Today.Year);
        }
        public IReadOnlyList<TransactionGroup> GetGroupTransactionsByMonth(string walletName, Month month, int year)
        {
            if (_wallets.ContainsKey(walletName) == false) throw new ArgumentException($"Wallet with name: {walletName} does nit exist");
            var wallet = _wallets[walletName];
            return wallet.GetGroupTransactionsByMonth(month, year);
        }
        public IReadOnlyDictionary<Wallet, IReadOnlyList<Transaction>> GetBiggestExpensesForMonth(Month month)
        {
            return GetBiggestExpensesForMonth(month, DateTime.Today.Year);
        }
        public IReadOnlyDictionary<Wallet, IReadOnlyList<Transaction>> GetBiggestExpensesForMonth(Month month, int year)
        {
            return _wallets.Values.ToDictionary(w => w, w => w.GetTop3ExpensesForMonth(month, year));
        }
        private decimal GetTotalAmountTransactions(string walletName, DateTime start, DateTime end, TransactionType type)
        {
            if (_wallets.ContainsKey(walletName) == false) throw new ArgumentException($"Wallet with name: {walletName} does nit exist");
            var wallet = _wallets[walletName];
            return wallet.GetTotalAmountTransactions(start, end, TransactionType.Expense);
        }
        private (DateTime start, DateTime end) CalculatePeriodForMonth(Month month, int year)
        {
            var start = new DateTime(year, (int)month, 1);
            var end = new DateTime(year, (int)month, DateTime.DaysInMonth(year, (int)month))
                .AddDays(1).AddSeconds(-1);
            return (start, end);
        }
    }
}
