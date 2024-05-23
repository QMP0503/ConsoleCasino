using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCasino
{
    public class History
    {
        public double money {  get; set; }//money won/lost
        public string game { get; set; }
        public string result { get; set; }

        public History(double money, string game,string result)
        {
            this.money = money;
            this.game = game; 
            this.result = result;
        }

    }
}
