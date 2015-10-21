using System;
using Sample.Bank.Service;
namespace Sample.Bank.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            //AccountNumber acn = new AccountNumber("123456-789");
            try {
                AccountNumber acn = new AccountNumber("423456-52");
                Console.WriteLine(acn.getLongFormat());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
