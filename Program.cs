namespace InvalidTransactions
{
    internal class Program
    {
    /*
    A transaction is possibly invalid if:

    the amount exceeds $1000, or;
    if it occurs within (and including) 60 minutes of another transaction with the same name in a different city.
    You are given an array of strings transaction where transactions[i] consists of comma-separated values representing the name, time (in minutes), amount, and city of the transaction.

    Return a list of transactions that are possibly invalid. You may return the answer in any order.

 

    Example 1:

    Input: transactions = ["alice,20,800,mtv","alice,50,100,beijing"]
    Output: ["alice,20,800,mtv","alice,50,100,beijing"]
    Explanation: The first transaction is invalid because the second transaction occurs within a difference of 60 minutes, have the same name and is in a different city. Similarly the second one is invalid too.
    Example 2:

    Input: transactions = ["alice,20,800,mtv","alice,50,1200,mtv"]
    Output: ["alice,50,1200,mtv"]
    Example 3:

    Input: transactions = ["alice,20,800,mtv","bob,50,1200,mtv"]
    Output: ["bob,50,1200,mtv"]
 

    Constraints:

    transactions.length <= 1000
    Each transactions[i] takes the form "{name},{time},{amount},{city}"
    Each {name} and {city} consist of lowercase English letters, and have lengths between 1 and 10.
    Each {time} consist of digits, and represent an integer between 0 and 1000.
    Each {amount} consist of digits, and represent an integer between 0 and 2000.

    */
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public IList<string> InvalidTransactions(string[] transactions)
        {
            int n = transactions.Length;
            bool[] invalidFlags = new bool[n];

            // Parse transactions into a structured list
            var parsedTransactions = new List<(string name, int time, int amount, string city)>();
            for (int i = 0; i < n; i++)
            {
                var parts = transactions[i].Split(',');
                var name = parts[0];
                var time = int.Parse(parts[1]);
                var amount = int.Parse(parts[2]);
                var city = parts[3];
                parsedTransactions.Add((name, time, amount, city));
            }

            // Compare each transaction with every other transaction
            for (int i = 0; i < n; i++)
            {
                var (name1, time1, amount1, city1) = parsedTransactions[i];

                // Check if amount exceeds 1000
                if (amount1 > 1000)
                {
                    invalidFlags[i] = true;
                }

                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;

                    var (name2, time2, amount2, city2) = parsedTransactions[j];

                    // Check same name, different city, within 60 minutes
                    if (name1 == name2 && city1 != city2 && Math.Abs(time1 - time2) <= 60)
                    {
                        invalidFlags[i] = true;
                        // No need to mark j here, as it will be handled when j becomes i in the outer loop
                    }
                }
            }

            // Collect all invalid transactions
            var result = new List<string>();
            for (int i = 0; i < n; i++)
            {
                if (invalidFlags[i])
                {
                    result.Add(transactions[i]);
                }
            }

            return result;
        }
    }
}
