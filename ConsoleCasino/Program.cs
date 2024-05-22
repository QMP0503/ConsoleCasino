using System;
using System.Net.NetworkInformation;
using System.Security;
using System.Security.Cryptography;
using System.Linq;
using System.Data;

namespace ConsoleCasino
{
    public class Program
    {
        public static Random rnd = new Random();

        public static List<userAccount> accounts = new List<userAccount>();
        public static void Main(string[] args)
        {
            //User Accounts
            accounts.Add(new userAccount("Quang", 123, 0,0,0)); //using for demo
            accounts.Add(new userAccount("Jason", 122, 10,2,5));
            accounts.Add(new userAccount("Bob", 111, 100, 6, 5));
            accounts.Add(new userAccount("Lee", 124, 800, 10, 80));
            accounts.Add(new userAccount("Hob", 190, 0, 10, 88));
            accounts.Add(new userAccount("Jan", 666, 100, 20, 70));
            accounts.Add(new userAccount("Quin", 444, 1, 100, 2));
            accounts.Add(new userAccount("Jack", 999, 5, 80, 7));

            //Application start
            Console.WriteLine("Welcome to Console Casino\n");

            //login
            Console.WriteLine("Please enter your username: ");
            userAccount currentUser;
            string accountUsername;
            while (true)
            {
                try
                {
                    accountUsername = Console.ReadLine();
                    currentUser = accounts.FirstOrDefault(a => a.username == accountUsername);
                    if(currentUser != null)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Username does not exist. Please try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Username does not exist. Please try again.");
                }
            }
            Console.WriteLine("Please enter your pin: ");
            int pin;
            while (true)
            {
                try
                {
                    int.TryParse(Console.ReadLine(), out pin);
                    if (currentUser.GetPin() == pin)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid pin. Please try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid pin. Please try again.");
                }
            }

            //game option begins
            while (true)
            {
                PrintOption();
                int input;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out input) == true && input > 0 && input < 8)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option");
                    }
                }

                switch (input)
                {
                    case 1:
                        Deposit(currentUser);
                        break;
                    case 2:
                        ShowRecord(currentUser);
                        break;
                    case 3:
                        DiceGame(currentUser);
                        break;
                    case 4:
                        RouletteGame(currentUser);
                        break;
                    case 5:
                        Balance(currentUser);
                        break;
                    case 6:
                        Leaderboard();
                        Console.WriteLine();
                        break;
                    case 7:
                        Console.WriteLine("Thank You for visiting Concole Casino!");
                        Console.WriteLine("We hope to see you again soon! :)) \n");
                        return;
                    default: //rendundant but incase as fallback option
                        Console.WriteLine("\nPlease enter a valid option\n");
                        break;
                } 
            }
           
        }
        public static void PrintOption()
        {
            Console.WriteLine("Please select the following options");
            Console.WriteLine("1. Add deposit to balance");
            Console.WriteLine("2. View user gambling records");
            Console.WriteLine("3. Start playing DICEGAME");
            Console.WriteLine("4. Start playing Roulette Game");
            Console.WriteLine("5. View balance.");
            Console.WriteLine("6. View winning leaderboard");
            Console.WriteLine("7. Exit Casino\n");
        }
        public static void Leaderboard()
        {
            var userQuery =
               from account in accounts
               orderby account.GetWins() descending
               select account;

            int i = 0; //have visible number in leaderboard.
            foreach (userAccount account in userQuery)
            {
                i++;
                Console.WriteLine($"{i}.{account.username} with {account.GetWins()} WINS");
            }
        }
        public static void ShowRecord(userAccount currentUser)
        {
            Console.WriteLine("ALL TIME RECORD:");
            Console.WriteLine("Wins: " + currentUser.GetWins());
            Console.WriteLine("Loss: " + currentUser.GetLoss() +"\n");
        }
        public static void Deposit(userAccount currentUser)
        {
            Console.WriteLine("Enter the amount you would like to deposit: ");
            double deposit;
            while (double.TryParse(Console.ReadLine(), out deposit) != true || deposit<0)
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("Enter the amount you would like to deposit: ");
            }
            currentUser.SetBalance(deposit);
            Console.WriteLine("Thank you for your deposit!\n");
        }
        public static void DiceGame(userAccount currentUser)
        {
            Console.WriteLine("Welcome to the DICE GAME!");
            Console.WriteLine("Roll the dice and beat the dealer to win! Jackpot if you match the dealer!!");
            while (true)
            {
                Console.WriteLine("Please Enter Your BET! Enter 0 to exit.");
                double bet = WithdrawBalance(currentUser);
                if (bet == 0)
                {
                    return;
                }
                Console.WriteLine("\nPlease Pick An Option:");
                Console.WriteLine("1. Start");
                Console.WriteLine("2. Exit (Your bet will be refunded)\n");
                int input;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out input) && input == 1)
                    {
                        break;
                    }
                    else if(input == 2)
                    {
                        currentUser.SetBalance(currentUser.GetBalance() + bet); //returning bet money if they want to exit after placing bet.
                        Balance(currentUser);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option!");
                    }
                }
                int userRoll = rnd.Next(1, 7); //random number between 1 and 6
                int dealerRoll = rnd.Next(1, 7);
                Console.WriteLine("Your roll is: " + userRoll);
                Console.WriteLine("Dealer roll is: " + dealerRoll);
                if (userRoll > dealerRoll)
                {
                    Console.WriteLine("\nHURRAY you WON!");
                    Console.WriteLine($"You won {bet*2}!!\n");
                    currentUser.SetBalance(currentUser.GetBalance() + bet*2);
                    currentUser.SetWins(currentUser.GetWins() + 1);
                }
                else if (userRoll < dealerRoll)
                {
                    Console.WriteLine("\nToo bad you LOST!!!!!!");
                    Console.WriteLine("The house will take all your bet :(\n");
                    currentUser.SetLoss(currentUser.GetLoss() + 1);
                }
                else
                {
                    Console.WriteLine("\nON THE MONEY. YOU 5X you MONEY!");
                    Console.WriteLine($"You won {bet * 5}!!\n");
                    currentUser.SetWins(currentUser.GetWins() + 1);
                    currentUser.SetBalance(currentUser.GetBalance() + bet*5);
                }
                Balance(currentUser);
            }
            
        }
        public static void RouletteGame(userAccount currentUser)
         {
            Console.WriteLine("\nWELCOME TO ROULETTE!\n");
             while(true)
             {
                Console.WriteLine("Please Enter Your BET! Enter 0 to exit.");
                double bet = WithdrawBalance(currentUser);
                if(bet == 0) 
                { 
                    return; 
                }
                Console.WriteLine("\nSELECT YOUR BETTING OPTION");
                Console.WriteLine("1. Single Number");
                Console.WriteLine("2. Odd or Even");
                Console.WriteLine("3. Exit (Your bet will be refunded)");
                int rouletteRoll = rnd.Next(0, 37);
                int input;
                while (true)
                {
                    if(int.TryParse(Console.ReadLine(), out input))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option!");
                    }
                }
                switch (input)
                {
                    case 1: //pick number win by 30.
                        Console.WriteLine("\nPlease select an integer from 0 to 36:");
                        int betNum;
                        while (true)
                        {
                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out betNum) == true)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nPlease try again and enter a valid integer from 0 to 36.");
                                }
                            }//checking for valid entry
                            if (betNum >= 0 && betNum <= 36)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nPlease try again and enter a valid integer from 0 to 36.");
                            }
                        }//checking for valid int between 0 - 36
                        Console.WriteLine("\nTHE WINNING NUMBER IS: " + rouletteRoll);
                        if (rouletteRoll == betNum)
                        {
                            Console.WriteLine($"\nCONGRATS YOU WON {bet * 30}!!!\n");
                            currentUser.SetWins(currentUser.GetWins() + 1);
                            currentUser.SetBalance(currentUser.GetBalance() + bet * 30);
                            Balance(currentUser);
                        }
                        else
                        {
                            Console.WriteLine($"\nSadly you lost {bet} :(\n");
                            currentUser.SetLoss(currentUser.GetLoss() + 1);
                            Balance(currentUser);
                        }
                        break;

                    case 2:
                        Console.WriteLine("\nPlease select 1 for odd or 0 for even: ");
                        int betInt;
                        while (true)
                        {
                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out betInt) == true)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nPlease try again and enter 1 for odd or 0 for even:");
                                }
                            }//checking for valid entry
                            if (betInt == 1 || betInt == 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nPlease try again and enter  1 for odd or 0 for even:");
                            }
                        }//checking for valid option

                        Console.WriteLine("\nTHE WINNING NUMBER IS: "+ rouletteRoll);
                        if (rouletteRoll % 2 == betInt)
                        {
                            Console.WriteLine($"\nCONGRATS YOU WON {bet * 2}!!!");
                            currentUser.SetBalance(currentUser.GetBalance() + bet * 2);
                            currentUser.SetWins(currentUser.GetWins() + 1);
                            Balance(currentUser);
                        }
                        else
                        {
                            Console.WriteLine($"\nSadly you lost {bet} :(");
                            currentUser.SetLoss(currentUser.GetLoss() + 1);
                            Balance(currentUser);
                        }
                        break;

                    case 3:
                        currentUser.SetBalance(currentUser.GetBalance()+bet); //returning bet money if they want to exit after placing bet.
                        Balance(currentUser);
                        return;

                    default:
                        Console.WriteLine("Please try again and enter a valid option.");
                        break;
                }   
             }
        }

        public static double WithdrawBalance(userAccount currentUser)
        {
            while (true)
            {
                double withdraw;
                while (true)
                {
                    if (double.TryParse(Console.ReadLine(), out withdraw) == true && withdraw>=0)
                    {
                        break;
                    }
                    Console.WriteLine("Please enter a valid value!");
                }
                if (currentUser.GetBalance() - withdraw >= 0)
                {
                    currentUser.SetBalance(currentUser.GetBalance() - withdraw);
                    Console.WriteLine("You are good to go!");
                    return withdraw;
                }
                else
                {
                    Console.WriteLine("Insufficient balance. Please try again");
                    Console.WriteLine("If you are out of money please enter 0 to exit.");
                }
            }
               
        }
        public static void Balance(userAccount currentUser)
        {
            Console.WriteLine("Current Balance: " + currentUser.GetBalance() +"\n");
        }

    }
}