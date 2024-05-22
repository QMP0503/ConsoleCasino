using System;
using System.Net.NetworkInformation;
using System.Security;
using System.Security.Cryptography;

namespace ConsoleCasino
{
    public class Program
    {
        public static Random rnd = new Random();
        public static void Main(string[] args)
        {
            //User Accounts
            List<userAccount> accounts = new List<userAccount>();
            accounts.Add(new userAccount("Quang", 123, 0,0,0));
            accounts.Add(new userAccount("Jason", 122, 0,0,0));

        

            //game start
            Console.WriteLine("Welcome to Console Casino");
            Deposit(accounts[0]);
            Balance(accounts[0]);
            DiceGame(accounts[0]);


            
            
        }
        public static void PrintOption()
        {
            Console.WriteLine("Please select the following options");
            Console.WriteLine("1. Add deposit to balance");
            Console.WriteLine("2. start playing DICEGAME");
            Console.WriteLine("3. Exit Casino");
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
            Console.WriteLine("Thank you for your deposit!");
        }
        public static void DiceGame(userAccount currentUser)
        {
            Console.WriteLine("Welcome to the DICE GAME!");
            while (true)
            {
                Console.WriteLine("Please Enter Your BET! Enter 0 to exit.");
                double bet = WithdrawBalance(currentUser);
                if (bet == 0)
                {
                    return;
                }
                Console.WriteLine("Please Pick An Option:");
                Console.WriteLine("1. Start");
                Console.WriteLine("2. Exit (Your bet will be refunded)");
                int input;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out input) || input == 1)
                    {
                        break;
                    }
                    else if(input == 2)
                    {
                        currentUser.SetBalance(currentUser.GetBalance() + bet); //returning bet money if they want to exit after placing bet.
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
                    Console.WriteLine("HURRAY you WON!");
                    Console.WriteLine($"You won {bet*2}!!");
                    currentUser.SetBalance(currentUser.GetBalance() + bet*2);
                }
                else if (userRoll < dealerRoll)
                {
                    Console.WriteLine("Too bad you LOST!!!!!!");
                    Console.WriteLine("The house will take all your bet :(");
                }
                else
                {
                    Console.WriteLine("Its a TIE. You get your money back");
                    currentUser.SetBalance(currentUser.GetBalance() + bet);
                }
                Balance(currentUser);
            }
            
        }
        public static void RouletteGame(userAccount currentUser)
         {
            Console.WriteLine("WELCOME TO ROULETTE!");
             while(true)
             {
                Console.WriteLine("Please Enter Your BET! Enter 0 to exit.");
                double bet = WithdrawBalance(currentUser);
                if(bet == 0) 
                { 
                    return; 
                }
                Console.WriteLine("SELECT YOUR BETTING OPTION");
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
                        Console.WriteLine("Please select an integer from 0 to 36:");
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
                                    Console.WriteLine("Please try again and enter a valid integer from 0 to 36.");
                                }
                            }//checking for valid entry
                            if (betNum >= 0 && betNum <= 36)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please try again and enter a valid integer from 0 to 36.");
                            }
                        }//checking for valid int between 0 - 36
                        Console.WriteLine("THE WINNING NUMBER IS: " + rouletteRoll);
                        if (rouletteRoll == betNum)
                        {
                            Console.WriteLine($"CONGRATS YOU WON {bet * 30}!!!");
                            currentUser.SetBalance(currentUser.GetBalance() + bet * 30);
                            Balance(currentUser);
                        }
                        else
                        {
                            Console.WriteLine($"Sadly you lost {bet} :(");
                            Balance(currentUser);
                        }
                        break;

                    case 2:
                        Console.WriteLine("Please select 1 for odd or 0 for even: ");
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
                                    Console.WriteLine("Please try again and enter 1 for odd or 0 for even:");
                                }
                            }//checking for valid entry
                            if (betInt == 1 || betInt == 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please try again and enter  1 for odd or 0 for even:");
                            }
                        }//checking for valid option

                        Console.WriteLine("THE WINNING NUMBER IS: "+ rouletteRoll);
                        if (rouletteRoll % 2 == betInt)
                        {
                            Console.WriteLine($"CONGRATS YOU WON {bet * 2}!!!");
                            currentUser.SetBalance(currentUser.GetBalance() + bet * 2);
                            Balance(currentUser);
                        }
                        else
                        {
                            Console.WriteLine($"Sadly you lost {bet} :(");
                            Balance(currentUser);
                        }
                        break;

                    case 3:
                        currentUser.SetBalance(currentUser.GetBalance()+bet); //returning bet money if they want to exit after placing bet.
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
            Console.WriteLine("Current Balance: " + currentUser.GetBalance());
        }

    }
}