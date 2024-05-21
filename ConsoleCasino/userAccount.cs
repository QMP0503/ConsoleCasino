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
        string username;
        int pin;
        double balance;

        public userAccount(string username, int pin, double balance)
        {
            this.username = username;
            this.pin = pin;
            this.balance = balance;
        }
        public int getPin() { return pin; }
        public double getBalance() { return balance; }
        public string getUsername() { return username; }
        public void setUsername(string username) { this.username = username; }
        public void setPin(int pin) {  this.pin = pin; }
        public void setNewBalance(double deposit) {  balance += deposit; }
    }
}
