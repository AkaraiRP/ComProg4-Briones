using System;
using System.IO;

namespace LMS_Assessment_Briones
{
    class Bank
    {
        public class Money
        {
            //Initiate current app session Balance, Credit, and Debit transactions.
            public int Balance;
            public int Credit;
            public int Debit;


            public void readBalance()
            {
                // Read from file "records.txt" and then split from space to get account balance and then set current session Balance as records Balance.
                string[] split = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "records.txt").Split(" ");
                Balance = int.Parse(split[1]);
            }

            void saveToFile()
            {
                // Calculate current session's total before appending to records file.
                int total = Balance + Credit - Debit;
                string ToFile = "Balance: " + total;

                // Write to records file.
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "records.txt", ToFile);
            }

            public void addBalance(int amount)
            {
                // Append Timestamp and amount added to creditRecords file.
                var time = DateTime.Now.ToString("yyyy-MM-dd|HH:mm:ss");
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "creditRecords.txt", $"[{time}] Credited {amount} to account." + Environment.NewLine);

                // Add Credit to current session Credits then save to records file.
                Credit += amount;
                saveToFile();
            }

            public void removeBalance(int amount)
            {
                // Append Timestamp and amount removed to debitRecords file.
                var time = DateTime.Now.ToString("yyyy-MM-dd|HH:mm:ss");
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "debitRecords.txt", $"[{time}] Debited {amount} from account." + Environment.NewLine);

                // Add Debit amount to current session Debits then save to records file.
                Debit += amount;
                saveToFile();

            }

            public void readCredits()
            {
                string line;

                // Read the file and display it line by line.  
                StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "creditRecords.txt");
                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }

                file.Close();
            }

            public void readDebits()
            {
                string line;

                // Read the file and display it line by line.  
                StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "debitRecords.txt");
                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }

                file.Close();
            }


        }

        static void checkFile()
        {
            // Set records, creditRecords, and debitRecords file.
            string records = AppDomain.CurrentDomain.BaseDirectory + "records.txt";
            string creditRecords = AppDomain.CurrentDomain.BaseDirectory + "creditRecords.txt";
            string debitRecords = AppDomain.CurrentDomain.BaseDirectory + "debitRecords.txt";

            // Checks if the files exists and if not, create the files.
            if (!File.Exists(records))
            {
                using (StreamWriter writer = File.CreateText(records))
                {
                    writer.WriteLine("Balance: 0");
                }
            }
            else if (!File.Exists(creditRecords))
            {
                using (StreamWriter writer = File.CreateText(creditRecords))
                {
                    var time = DateTime.Now.ToString("yyyy-MM-dd|HH:mm:ss");
                    writer.WriteLine($"[{time}] Account Credit Records Started.");
                }
            }

            else if (!File.Exists(debitRecords))
            {
                using (StreamWriter writer = File.CreateText(debitRecords))
                {
                    var time = DateTime.Now.ToString("yyyy-MM-dd|HH:mm:ss");
                    writer.WriteLine($"[{time}] Account Debit Records Started.");
                }
            }

        }

        static void readFile()
        {
            // Used for debugging, read current records file.
            string records = AppDomain.CurrentDomain.BaseDirectory + "records.txt";

            string text = File.ReadAllText(records);
            Console.WriteLine(text);

        }

        static void Main(string[] args)
        {
            // Start Current session by setting banking to true, checking if the files are valid, and setting current session balance to records balance.
            Boolean banking = true;
            checkFile();
            Money bal = new Money();
            bal.readBalance();

            do
            {
                // Initiate Interface.
                Console.WriteLine("Welcome to STI Banking.");
                Console.WriteLine("1. Account Balance \n2. Add Balance to account \n3. Remove Balance from account \n4. Check Credit records \n5. Check Debit records \n6. Exit session");
                
                string input = Console.ReadLine();

                // Check if user input is a valid integer from selection.
                try
                {
                    // Switch case for different operations.
                    switch (int.Parse(input))
                    {
                        // Check Account Balance.
                        case 1:
                            Console.Clear();
                            AccountInformation.Balance();
                            Console.WriteLine("\nPress Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        // Add balance to account. (No limits)
                        case 2:
                            Console.Clear();
                            try
                            {
                                Console.WriteLine("Please input an amount to deposit.");
                                int amount = int.Parse(Console.ReadLine());
                                AccountCreditInformation.Credit(amount);
                            }
                            catch (Exception)
                            {
                                Console.Clear();
                                Console.WriteLine("Error: Please input a valid amount integer to deposit.\nPress enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            
                            Console.WriteLine("\nPress Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        // Remove balance from account. (No Limits, cannot remove amount greater than account balance)
                        case 3:
                            Console.Clear();
                            try
                            {
                                Console.WriteLine("Please input an amount to withdraw.");
                                int amount = int.Parse(Console.ReadLine());
                                DebitInformation.Debit(amount);
                            }
                            catch (Exception)
                            {
                                Console.Clear();
                                Console.WriteLine("Error: Please input a valid amount integer to deposit.\nPress enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            }

                            Console.WriteLine("\nPress Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        // Check creditRecords file and read line-by-line to interface.
                        case 4:
                            Console.Clear();
                            AccountCreditInformation.CreditRecords();
                            Console.WriteLine("\nPress Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        // Check debitRecords file and read line-by-line to interface.
                        case 5:
                            Console.Clear();
                            DebitInformation.DebitRecords();
                            Console.WriteLine("\nPress Enter to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        // Set banking to false to stop current session.
                        case 6:
                            banking = false;
                            break;

                        // Default in case input is not a valid case.
                        default:
                            Console.Clear();
                            Console.WriteLine("Error: Please choose a valid banking operation.\nPress enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }

                // Default Error message if user input is not a valid operation from the list.
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Error: Please choose a valid banking operation.\nPress enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
                
            }

            // do-while loop for current session.
            while (banking);
        }
    }
    class AccountInformation
    {
        public static void Balance()
        {
            // Initiate Balance and refresh session balance.
            Bank.Money bal = new Bank.Money();
            bal.readBalance();

            // Show user's current balance.
            int currentBalance = bal.Balance;
            Console.WriteLine("Current Account Balance:\n" + currentBalance);
        }
    }
    class AccountCreditInformation
    {
        public static void Credit(int amount) 
        {
            // Initiate Balance and refresh session balance.
            Bank.Money bal = new Bank.Money();
            bal.readBalance();

            // Add balance to account and prompt user that the balance is creddited + show the amount added.
            bal.addBalance(amount);
            Console.WriteLine("Balance is Credited");
            Console.WriteLine("Added " + amount + " to account.");
        }

        public static void CreditRecords()
        {
            // Initiate reading creditRecords.
            Bank.Money bal = new Bank.Money();
            bal.readCredits();
        }
    }

    class DebitInformation : AccountCreditInformation 
    {
        public static void Debit(int amount)
        {
            // Initiate Balance and refresh session balance.
            Bank.Money bal = new Bank.Money();
            bal.readBalance();

            // Check if the amount to be debited is not greater than current balance.
            if (amount > bal.Balance)
            {
                Console.WriteLine("Error: Cannot withdraw amount greater than your current balance.");
            }

            else
            {
                // Remove balance from account and prompt user that the balance is debited + show the amount removed.
                bal.removeBalance(amount);
                Console.WriteLine("Balance is Debited");
                Console.WriteLine("Debited " + amount + " from account.");
            }
        }

        public static void DebitRecords()
        {
            // Initiate reading debitRecords.
            Bank.Money bal = new Bank.Money();
            bal.readDebits();
        }
    }
}
