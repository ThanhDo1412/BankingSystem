using System;
using System.Linq;
using System.Threading.Tasks;
using BankingService.ViewModel;
using Newtonsoft.Json;

namespace BankingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = 0;

            while (true)
            {
                Console.WriteLine("Please select function as below:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Exit program");
                var stringKey = Console.ReadLine();
                if (int.TryParse(stringKey, out key) && key > 0 && key < 5)
                {
                    switch (key)
                    {
                        case 1:
                            CheckBalance().Wait();
                            break;
                        case 2:
                            Deposit().Wait();
                            break;
                        case 3:
                            Withdraw().Wait();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }

        public async static Task CheckBalance()
        {
            var client = new httpClientSerive();
            var accountNumber = 0;
            Console.WriteLine("Please input account number: ");
            var input = Console.ReadLine();
            while (true)
            {
                if (int.TryParse(input, out accountNumber))
                {
                    var result = await client.GetBalance(accountNumber);
                    Console.WriteLine("Resutl:");
                    Console.WriteLine(JsonConvert.SerializeObject(result));
                    break;
                }
                else
                {
                    Console.WriteLine("Account number is an interger. Please input account number again.");
                    input = Console.ReadLine();
                }
            }
        }

        public async static Task Deposit()
        {
            var client = new httpClientSerive();
            var accountNumber = 0;
            decimal amount = 0;
            var currency = string.Empty;
            Console.WriteLine("Please input information as format: \"acount number - amount - currency\"");
            var input = Console.ReadLine();
            while (true)
            {
                var items = input.Split("-").Select(p => p.Trim()).ToArray();
                if (items.Count() == 3 
                    && int.TryParse(items[0], out accountNumber)
                    && decimal.TryParse(items[1], out amount))
                {
                    await client.PostDeposit(new TransactionBaseRequest(accountNumber, amount, currency));
                    break;
                }
                else
                {
                    Console.WriteLine("Input data is wrong format. Please try again.");
                    input = Console.ReadLine();
                }
            }
        }

        public async static Task Withdraw()
        {
            var client = new httpClientSerive();
            var accountNumber = 0;
            decimal amount = 0;
            var currency = string.Empty;
            Console.WriteLine("Please input information as format: \"acount number - amount - currency\"");
            var input = Console.ReadLine();
            while (true)
            {
                var items = input.Split("-").Select(p => p.Trim()).ToArray();
                if (items.Count() == 3
                    && int.TryParse(items[0], out accountNumber)
                    && decimal.TryParse(items[1], out amount))
                {
                    await client.PostWithdraw(new TransactionBaseRequest(accountNumber, amount, currency));
                    break;
                }
                else
                {
                    Console.WriteLine("Input data is wrong format. Please try again.");
                    input = Console.ReadLine();
                }
            }
        }
    }
}
