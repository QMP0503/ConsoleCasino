using System;

namespace ConsoleCasino
{
    public class Program
    {
        public void Main(string[] args)
        {
            userAccount user;
            void printOption()
            {
                Console.WriteLine("Please select the following options");
                Console.WriteLine("1. Add deposit to balance");
                Console.WriteLine("2. start playing DICEGAME");
                Console.WriteLine("3. Exit Casino");
            }
            void addBalance()
            {
                try
                {
                    Console.WriteLine("Enter the amount you would like to deposit: ");
                    double deposit = double.Parse(Console.ReadLine());
                    user.setNewBalance(deposit);
                    Console.WriteLine("Thank you for your deposit!");
                }catch (Exception ex)
                {
                    Console.WriteLine("Please enter a numerical value.");
                }
                
            }
            double diceGame(double bet)
            {
                double money;
                Random rnd = new Random();
                int userRoll = rnd.Next(1,7); //random number between 1 and 6
                int dealerRoll = rnd.Next(1,7);
                if(userRoll > dealerRoll)
                {
                    Console.WriteLine("HURRAY you WON!");
                    money = bet * 2;
                    Console.WriteLine($"You won {money}!!");
                }else if(userRoll < dealerRoll)
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
            void diceGameMenu()
            {
                Console.WriteLine("Welcome to the DICE GAME!");
                Console.WriteLine("Press the \"k\" key when you are ready to start");
                Console.WriteLine("Press the \"n\" key to exit");
                string input = Console.ReadLine();
                while(true)
                {
                    if (input.ToLower() == "k")
                    {
                        Console.WriteLine("Enter the your bet amount: ");
                        double bet = double.Parse(Console.ReadLine());

                        diceGame(bet);
                        break;
                    }
                    else if (input.ToLower() == "n")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("please enter a valid option");
                    }
                }
            }
        }
    }
}