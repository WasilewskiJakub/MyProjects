using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZabwazIO
{
    public enum BankName
    {
        PKO_BP, PEKAO_SA, ALIOR_BANK, MBANK, GETIN_BANK, SANTANDER, BOS_BANK, NONDEFINED
    };
    [Serializable]
    public class BankAccount
    {
        public BankName Bank { get; set; }
        public Currency Money { get; set; }
        public string Password { get; set; }
        public BankAccount()
        {
            Bank = BankName.NONDEFINED;
            Money = null;
            Password = "1234";
        }
        public override string ToString()
        {
            return $"Bank Account Details:\n BankName: {Bank}, Currency: {Money.Name}, Balnace: { Money.Amount}\n" +
                $"Password: {this.Password}\n";
        }
        public bool SetNewPasword(string password, string setnew)
        {
            if (password == Password)
            {
                password = setnew;
                return true;
            }
            return false;
        }
        public bool SetBankAccount(BankName name, CurrencyType curr, string password = "0000")
        {
            this.Money = new Currency();
            this.Money.Name = curr;
            this.Password = password;
            this.Bank = name;
            return true;
        }
        public bool Deposit(string password, Currency value)
        {
            if (password != this.Password) return false;
            this.Money.Amount += Currency.Exchange(value.Amount, value.Name, this.Money.Name);
            return true;
        }
        public (double,bool) PayOut(string password, long value, CurrencyType type)
        {
            if (password != this.Password) return (0, false);
            double MoneyToWithdraw = Currency.Exchange(value, type, this.Money.Name);
            if (this.Money.Amount - MoneyToWithdraw < 0) return (0, false);
            this.Money.Amount -= MoneyToWithdraw;
            return (MoneyToWithdraw, true);
        }
    }
}
