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

        public List<History> histories = new List<History>();

        public userAccount(string username, int pin, double balance) 
        {
            this.username = username;
            this.pin = pin;
            this.balance = balance;
            
        }
        public static void AddUserRecord(userAccount currentUser, double money, string game, string result)
        {
            currentUser.histories.Add(new History(money, game, result));
        }



    }
}
