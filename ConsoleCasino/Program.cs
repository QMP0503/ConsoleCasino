using System;
using System.Security;

namespace ConsoleCasino
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //User Accounts
            List<userAccount> accounts = new List<userAccount>();
            accounts.Add(new userAccount("Quang", 123, 0));
            accounts.Add(new userAccount("Jason", 122, 0));

        

            //game start
            Console.WriteLine("Welcome to Console Casino");
            deposit(accounts[0]);

        }
        public static void printOption()
        {
            Console.WriteLine("Please select the following options");
            Console.WriteLine("1. Add deposit to balance");
            Console.WriteLine("2. start playing DICEGAME");
            Console.WriteLine("3. Exit Casino");
        }
        public static void deposit(userAccount currentUser)
        {
            Console.WriteLine("Enter the amount you would like to deposit: ");
            double deposit;
            while (double.TryParse(Console.ReadLine(), out deposit) != true)
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine("Enter the amount you would like to deposit: ");
            }
            currentUser.setNewBalance(deposit);
            Console.WriteLine("Thank you for your deposit!");
        }
        public static double diceGame(double bet)
        {
            double money;
            Random rnd = new Random();
            int userRoll = rnd.Next(1, 7); //random number between 1 and 6
            int dealerRoll = rnd.Next(1, 7);
            if (userRoll > dealerRoll)
            {
                Console.WriteLine("HURRAY you WON!");
                money = bet * 2;
                Console.WriteLine($"You won {money}!!");
            }
            else if (userRoll < dealerRoll)
            {
                Console.WriteLine("Too bad you LOST!!!!!!");
                money = 0;
                Console.WriteLine("The house will take all your bet :(");
            }
            else
            {
                Console.WriteLine("Its a TIE. You get your money back");
                money = bet;
            }
            return money;
        }
        public static void diceGameMenu()
        {
            Console.WriteLine("Welcome to the DICE GAME!");
            Console.WriteLine("Press the \"k\" key when you are ready to start");
            Console.WriteLine("Press the \"n\" key to exit");
        }
        public static bool withdrawBalance(userAccount currentUser, double withdraw)
        {
            if (currentUser.getBalance() - withdraw >= 0)
            {
                currentUser.setNewBalance(currentUser.getBalance() - withdraw);
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient balance");
                return false;
            }
        }
        public static void balance(userAccount currentUser)
        {
            Console.WriteLine("Current Balance: " + currentUser.getBalance());
        }

    }
}