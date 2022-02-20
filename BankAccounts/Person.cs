using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZabwazIO
{
    public enum GenderType
    {
        Male = 0,
        Female = 1
    };
    [Serializable]
    public class Person : IComparable
    {
        public string Name { get;  set; }
        public string Surname { get;  set; }
        public int Age { get;  set; }
        public GenderType Gender { get;  set; }
        public DateTime BornDate { get;  set; }
        public BankAccount account { get; set; }
        public void PrepareObj()
        {
            this.Age = this.CalculateAge();
            Random rnd = new Random();
            string[] tab = { "1234", "2252", "1425", "3948", "7283", "0543", "6148", "3248" };
            this.account = new BankAccount();
            this.account.SetBankAccount((BankName)rnd.Next(0, 5),(CurrencyType)rnd.Next(0, 2), tab[rnd.Next(0, tab.Length - 1)]);
            this.account.Deposit(this.account.Password, new Currency
            {
                Amount = rnd.Next(200, 5000), Name = (CurrencyType)rnd.Next(0, 2) 
            });
        }
        private int CalculateAge()
        {
            //Age:
            int YearNow = DateTime.Now.Year;
            int YearBorn = this.BornDate.Year;
            //Month:
            int MonthNow = DateTime.Now.Month;
            int MonthBorn = this.BornDate.Month;

            //Day:
            int DayNow = DateTime.Now.Day;
            int DayBorn = this.BornDate.Day;

            int age = YearNow - YearBorn;
            if(MonthBorn <= MonthNow)
            {
                if (DayBorn <= DayNow) return age;
            }
            return age - 1;
        }
        public override string ToString()
        {
            return $"[{this.Name}, {this.Surname}] Date of birth: {this.BornDate.ToString()} Age: {this.Age}, gender: {this.Gender}.\n" + account.ToString();
        }
        public int CompareTo(object obj)
        {

            if(this < (Person)obj)
            {
                return -1;
            }
            return 1;
        }
        // Operators Used in BST!
        public static bool operator > (Person p1, Person p2)
        {
            return p1.Age > p2.Age;
        }
        public static bool operator < (Person p1, Person p2)
        {
            return p1.Age < p2.Age;
        }
    }
}
