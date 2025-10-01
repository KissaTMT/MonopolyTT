using Accounting;

public class Program
{
    private static void Main(string[] args)
    {
        var walletController = new WalletController();
        var wallet = new Wallet("Wallet1", "EUR", 1000);
        var wallet1 = new Wallet("Wallet2", "EUR", 3000);
        var wallet2 = new Wallet("Wallet3", "EUR", 0);
        walletController.AddWallet(wallet);
        walletController.AddWallet(wallet1);
        walletController.AddWallet(wallet2);

        Console.WriteLine();

        FillWallet(walletController, wallet);
        FillWallet(walletController, wallet1);
        FillWallet(walletController, wallet2);

        Console.WriteLine(wallet.GetCurrentBalance());
        Console.WriteLine(wallet1.GetCurrentBalance());
        Console.WriteLine(wallet2.GetCurrentBalance());

        Console.WriteLine();
        GetGroupTransactionsByMonth(walletController, wallet);
        Console.WriteLine();
        GetGroupTransactionsByMonth(walletController, wallet1);
        Console.WriteLine();
        GetGroupTransactionsByMonth(walletController, wallet2);
        Console.WriteLine();
        GetBiggestExpensesForMonth(walletController);
    }
    private static void FillWallet(WalletController controller, Wallet wallet)
    {
        for (var i = 0; i < 365; i++)
        {
            controller.AddTransaction(wallet.Name, new Transaction(new DateTime(2025, new Random().Next(1, 13), new Random().Next(1, 28)), new Random().Next(100, 1000)
                ,new Random().NextDouble() > 0.5 ? TransactionType.Income : TransactionType.Expense));
        }
    }
    private static void GetGroupTransactionsByMonth(WalletController controller, Wallet wallet)
    {
        var group = controller.GetGroupTransactionsByMonth(wallet.Name, Month.April);
        Console.WriteLine(wallet);
        for (var i = 0; i < group.Count; i++)
        {
            var transactions = group[i].Transactions;
            for (var j = 0; j < transactions?.Count; j++)
            {
                Console.WriteLine(transactions[j]);
            }
        }
    }
    private static void GetBiggestExpensesForMonth(WalletController controller)
    {
        var total = controller.GetBiggestExpensesForMonth(Month.April);

        foreach (var w in total.Keys)
        {
            var transaction = total[w];
            Console.WriteLine(w);
            for (var i = 0; i < transaction.Count; i++)
            {
                Console.WriteLine(transaction[i]);
            }
            Console.WriteLine();
        }
    }
}