using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class BankAccount
    {
        public int Balance { get; private set; }
        public int CreditLimit { get; }

        public event EventHandler<ClassArgEvent> Overdraft;
        public event EventHandler<ClassArgEvent> Withdrawn;

        public BankAccount(int balance, int creditLimit)
        {
            Balance = balance;
            CreditLimit = creditLimit;
        }

        public void Withdraw(int amount)
        {
            if (Balance - amount < -CreditLimit)
            {
                OnOverdraft(new ClassArgEvent(amount, $"Overdraft! Balance: {Balance}"));
            }
            else
            {
                Balance -= amount;
                OnWithdrawn(new ClassArgEvent(amount, $"Withdrawn: {amount:C}. Balance: {Balance}"));
            }
        }

        public virtual void OnWithdrawn(ClassArgEvent e) => Withdrawn?.Invoke(this, e);
        public virtual void OnOverdraft(ClassArgEvent e) => Overdraft?.Invoke(this, e);

    }
}
