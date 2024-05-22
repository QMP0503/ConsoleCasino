using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCasino
{
    public class userAccount
    {
        public string username;
        public int pin;
        double balance;
        int wins;
        int loss;
        //List<string> record = new List<string>;

        public userAccount(string username, int pin, double balance, int wins, int loss)
        {
            this.username = username;
            this.pin = pin;
            this.balance = balance;
            this.wins = wins;
            this.loss = loss;
        }
        public int GetPin() { return pin; }
        public double GetBalance() { return balance; }
        public string GetUsername() { return username; }
        public void SetUsername(string username) { this.username = username; }
        public void SetPin(int pin) {  this.pin = pin; }
        public void SetBalance(double balance) {  this.balance = balance; }
        public int GetWins() { return wins;}
        public int GetLoss() { return loss;}
        public void SetLoss(int loss)
        {
            this.loss = loss;   
        }
        public void SetWins(int wins)
        {
            this.wins = wins;
        }
    }
}
