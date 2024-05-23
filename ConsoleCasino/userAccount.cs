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
        public string username {  get; set; }
        public double balance {  get; set; }
        public int pin { get; set; }

        public List<History> histories { get; set; }

        public userAccount(string username, int pin, double balance, List<History> histories) //need to add history class to manage amount of money won.
        {
            this.username = username;
            this.pin = pin;
            this.balance = balance;
            this.histories = histories;
        }

        
        
    }
}
