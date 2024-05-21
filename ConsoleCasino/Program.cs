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
            void diceGame()
            {
                Console.WriteLine("Welcome to the DICE GAME!");
                Console.WriteLine("Press the \"k\" key when you are ready to start");
                Console.WriteLine("Press the \"n\" key when you are not ready");
                string input = Console.ReadLine();
                while(true)
                {
                    if (input.ToLower() == "k")
                    {
                        break;
                    }
                    else if (input.ToLower() == "n")
                    {

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