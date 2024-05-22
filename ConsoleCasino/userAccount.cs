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
        //List<string> record = new List<string>;

        public userAccount(string username, int pin, double balance/*, List<string> record*/)
        {
            this.username = username;
            this.pin = pin;
            this.balance = balance;
            //this.record = record;
        }
        public int getPin() { return pin; }
        public double getBalance() { return balance; }
        public string getUsername() { return username; }
        public void setUsername(string username) { this.username = username; }
        public void setPin(int pin) {  this.pin = pin; }
        public void setNewBalance(double deposit) {  balance += deposit; }
        /*
        public void addRecord(string record)
        {
            this.record.Add(new string(record));
        }
        */
    }
}
