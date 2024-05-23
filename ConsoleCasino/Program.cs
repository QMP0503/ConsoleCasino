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
            accounts.Add(new userAccount("Quang", 123, 0, new List<History>())); //using for demo
            accounts.Add(new userAccount("Jason", 122, 10, new List<History>()));
            accounts.Add(new userAccount("Bob", 111, 100, new List<History>()));
            accounts.Add(new userAccount("Lee", 124, 800, new List<History>()));
            accounts.Add(new userAccount("Hob", 190, 0, new List<History>()));
            accounts.Add(new userAccount("Jan", 666, 100, new List<History>()));
            accounts.Add(new userAccount("Quin", 444, 1, new List<History>()));
            accounts.Add(new userAccount("Jack", 999, 5, new List<History>()));

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
                    if (currentUser.pin == pin)
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
                        if (currentUser.histories.Count() > 0)
                        {
                            ShowRecord(currentUser);
                        }
                        else
                        {
                            Console.WriteLine("You have an empty record. Please select another option.\n");
                        }
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
                        Console.WriteLine("Thank You for visiting Concole Casino!");
                        Console.WriteLine("We hope to see you again soon! :)) \n");
                        return;
                    default: //rendundant as fallback option
                        Console.WriteLine("\nPlease enter a valid option\n");
                        break;
                } 
            }
           
        }
        public static void AddUserRecord(userAccount currentUser, double money, string game, string result)
        {
                currentUser.histories.Add(new History(money, game, result));
        }
        public static void PrintOption()
        {
            Console.WriteLine("\nPlease select the following options");
            Console.WriteLine("1. Add deposit to balance");
            Console.WriteLine("2. View user gambling records");//need second menu for user to select what they want to see
            Console.WriteLine("3. Start playing DICEGAME");
            Console.WriteLine("4. Start playing Roulette Game");
            Console.WriteLine("5. View balance");
            Console.WriteLine("6. Exit Casino\n");
        }
        
        public static void ShowRecord(userAccount currentUser) 
        {
            Console.WriteLine("Welcome to your gambling history!");
            while (true)
            {
                Console.WriteLine("\nPlease select the following option:");
                Console.WriteLine("1. Win record");
                Console.WriteLine("2. Loss record");
                Console.WriteLine("3. All records");
                Console.WriteLine("4. Stat summary");
                Console.WriteLine("5. Exit");

                int input;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out input) == true && input > 0 && input < 6)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option\n");
                    }
                }
                switch (input)
                {
                    case 1:
                        GetWinRecord(currentUser); break;
                    case 2:
                        GetLossRecord(currentUser); break;
                    case 3:
                        GetAllRecord(currentUser); break;
                    case 4:
                        StatSummary(currentUser); break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Please enter a valid option\n");
                        break;
                }
            }
            
        }
        
        public static void GetAllRecord(userAccount currentUser)
        {
            var recordQuery =
                from History in currentUser.histories
                select History;
           
            Console.WriteLine("\nALL TIME RECORD: \n");
            Console.WriteLine("   Game \tAmount \tResult");
            double totalMoney = 0;
            int i = 0;
            foreach (History history in recordQuery)
            {
                i++;
                totalMoney += history.money;
                Console.WriteLine($"{i}. {history.game}\t{history.money}\t{history.result}");
            }
            
            Console.WriteLine("\nTotal Money Won: " + totalMoney);
            Console.WriteLine("Total games played: " + recordQuery.Count());
            
        }
        public static void StatSummary(userAccount currentUser)
        {
            Console.WriteLine($"{currentUser.username}'s All Time Gambling Stats!\n");
            var recordQuery =
                from History in currentUser.histories
                select History;
            int totalGamesPlayed = recordQuery.Count();
            double biggestWin = recordQuery.Max(history => history.money);
            double biggestLoss = recordQuery.Min(history => history.money);
            int DGWins = recordQuery.Count(history => history.game.Equals("Dice Game") && history.result.Equals("Win"));
            var rouletteWins = recordQuery.Count(history => history.game.Equals("Roulette") && history.result.Equals("Win"));

            Console.WriteLine($"You played a total of {totalGamesPlayed} games!");
            Console.WriteLine($"Out of {totalGamesPlayed} you won {DGWins + rouletteWins} games!");
            Console.WriteLine($"In roulette you won {rouletteWins} games.");
            Console.WriteLine($"In Dice Game you won {DGWins} games.");
            Console.WriteLine($"Your biggest win is ${biggestWin} while your biggest loss is ${biggestLoss}.");
        }
        public static void GetWinRecord(userAccount currentUser)
        {
            var winQuery = 
                from History in currentUser.histories
                where History.result == "Win"
                select History;
            int i=0; //counter for number of wins(first log to last)
            double totalMoney = 0;
            Console.WriteLine("Win RECORD: \n");
            Console.WriteLine("   Game \tAmount");
            foreach (History history in winQuery)
            {
                i++;
                totalMoney += history.money;
                Console.WriteLine($"{i}. {history.game}\t{history.money}");
            }
            Console.WriteLine("\nTotal Money Won: " + totalMoney);
        }
        public static void GetLossRecord(userAccount currentUser)
        {
            var lossQuery =
                from History in currentUser.histories
                where History.result == "Loss"
                select History;
            int i = 0; //counter for number of wins(first log to last)
            double totalMoney = 0;
            Console.WriteLine("Loss RECORD: \n");
            Console.WriteLine("   Game \tAmount");
            foreach (History history in lossQuery)
            {
                i++;
                totalMoney += history.money;
                Console.WriteLine($"{i}. {history.game}\t{history.money}");
            }
            
            Console.WriteLine("\nTotal Money Lost: " + totalMoney);
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
            currentUser.balance = deposit;
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
                        currentUser.balance += bet; //returning bet money if they want to exit after placing bet.
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
                    currentUser.balance += (bet*2);
                    AddUserRecord(currentUser, bet * 2, "Dice Game", "Win");
                }
                else if (userRoll < dealerRoll)
                {
                    Console.WriteLine("\nToo bad you LOST!!!!!!");
                    Console.WriteLine("The house will take all your bet :(\n");
                    AddUserRecord(currentUser, -bet, "Dice Game", "Loss");
                }
                else
                {
                    Console.WriteLine("\nON THE MONEY. YOU 5X you MONEY!");
                    Console.WriteLine($"You won {bet * 5}!!\n");
                    //add to record for win
                    currentUser.balance+=(bet*5);
                    AddUserRecord(currentUser, bet * 5, "Dice Game", "Win");
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
                            //add win to record
                            currentUser.balance += (bet * 30);
                            Balance(currentUser);
                            AddUserRecord(currentUser, bet * 30, "Roulette", "Win");

                        }
                        else
                        {
                            Console.WriteLine($"\nSadly you lost {bet} :(\n");
                            //add loss to record
                            Balance(currentUser);
                            AddUserRecord(currentUser, -bet, "Roulette", "Loss");
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
                            currentUser.balance += (bet * 2);
                            AddUserRecord(currentUser, bet * 2, "Roulette", "Win");
                            Balance(currentUser);
                        }
                        else
                        {
                            Console.WriteLine($"\nSadly you lost {bet} :(");
                            AddUserRecord(currentUser, -bet, "Roulette", "Loss");
                            Balance(currentUser);
                        }
                        break;

                    case 3:
                        currentUser.balance += bet; //returning bet money if they want to exit after placing bet.
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
                if (currentUser.balance - withdraw >= 0)
                {
                    currentUser.balance -= withdraw;
                    Console.WriteLine("You are good to go!\n");
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
            Console.WriteLine("Current Balance: " + currentUser.balance +"\n");
        }

    }
}