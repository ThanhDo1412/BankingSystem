using System;
using System.Collections.Generic;
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
                Console.WriteLine("4. Test conccurrency with 4 requests for each deposit and withdraw");
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
                            TestConcurrency().Wait();
                            break;
                    }
                }
                Console.WriteLine("----------------------------------------------------");
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
                    Console.WriteLine(result);
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
            Console.WriteLine("Please input information as format: \"acount number - amount - currency\"");
            var input = Console.ReadLine();
            while (true)
            {
                var items = input.Split("-").Select(p => p.Trim()).ToArray();
                if (items.Count() == 3 
                    && int.TryParse(items[0], out accountNumber)
                    && decimal.TryParse(items[1], out amount))
                {
                    var result = await client.PostDeposit(new TransactionBaseRequest(accountNumber, amount, items[2]));
                    Console.WriteLine("Result:");
                    Console.WriteLine(JsonConvert.SerializeObject(result));
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
            Console.WriteLine("Please input information as format: \"acount number - amount - currency\"");
            var input = Console.ReadLine();
            while (true)
            {
                var items = input.Split("-").Select(p => p.Trim()).ToArray();
                if (items.Count() == 3
                    && int.TryParse(items[0], out accountNumber)
                    && decimal.TryParse(items[1], out amount))
                {
                    var result = await client.PostWithdraw(new TransactionBaseRequest(accountNumber, amount, items[2]));
                    Console.WriteLine("Result:");
                    Console.WriteLine(JsonConvert.SerializeObject(result));
                    break;

                }
                else
                {
                    Console.WriteLine("Input data is wrong format. Please try again.");
                    input = Console.ReadLine();
                }
            }
        }

        public async static Task TestConcurrency()
        {
            var client = new httpClientSerive();
            var tasks = new List<Task<TransactionBaseResponse>>();

            var beforeBalance = await client.GetBalance(1);
            Console.WriteLine("balance before run:" + beforeBalance);

            for (int i = 1; i < 5; i++)
            {
                tasks.Add(TestDeposit(new TransactionBaseRequest(1, i * 100, "USD")));
                tasks.Add(TestWithdraw(new TransactionBaseRequest(1, i * 50, "USD")));

                tasks.Add(TestDeposit(new TransactionBaseRequest(1, i * 100, "MYR")));
                tasks.Add(TestWithdraw(new TransactionBaseRequest(1, i * 50, "MYR")));
            }

            await Task.WhenAll(tasks);
            var afterBalance = await client.GetBalance(1);
            Console.WriteLine("balance after run:" + afterBalance);
        }

        private async static Task<TransactionBaseResponse> TestWithdraw(TransactionBaseRequest request)
        {
            var client = new httpClientSerive();
            var response = await client.PostWithdraw(request);
            Console.WriteLine("Withdraw result: " + JsonConvert.SerializeObject(response));
            return response;
        }

        private async static Task<TransactionBaseResponse> TestDeposit(TransactionBaseRequest request)
        {
            var client = new httpClientSerive();
            var response = await client.PostDeposit(request);
            Console.WriteLine("Deposit result: " + JsonConvert.SerializeObject(response));
            return response;
        }
    }
}
