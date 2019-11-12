using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLIDPrinciplesDemo
{
    //DIP Violation
    // Low level Class
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        
        public void AddFunds(decimal value)
        {
            Balance += value;
        }
        public void RemoveFunds(decimal value)
        {
            Balance -= value;
        }
    }
    // High level Class
    public class TransferAmount
    {
        public BankAccount Source { get; set; }
        public BankAccount Destination { get; set; }
        public decimal Value { get; set; }

        public void Transfer()
        {
            Source.RemoveFunds(Value);
            Destination.AddFunds(Value);
        }
    }
 /* 
    Problem with above design:
 
    1. The high level TransferAmount class is directly dependent upon the lower level BankAccount class i.e. Tight coupling.
    2. The Source and Destination properties reference the BankAccount type.So impossible to substitute other account types unless they are subclasses of BankAccount. 
    3. Later we want to add the ability to transfer money from a bank account to pay bills, the BillAccount class would have to inherit from BankAccount. 
       As bills would not support the removal of funds, 
       3.A. This is likely to break the rules of the Liskov Substitution Principle (LSP) or 
       3.B. Require changes to the TransferAmount class that do not comply with the Open/Closed Principle (OCP).
 
    4. Any extension functionality changes be required to low level modules. 
        4.A. Change in the BankAccount class may break the TransferAmount. 
        4.B. In complex scenarios, changes to low level classes can cause problems that cascade upwards through the hierarchy of modules. 
    5. As the software grows, this structural problem can be compounded and the software can become fragile or rigid.
    6. Without the DIP, only the lowest level classes may be easily reusable.
    7. Unit testing should be redone when there is a change in high level or low level classes.
    8. Time taken process to change the existing functionality and extending the functionality
*/ 

    //Applying DIP resolves these problems by removing direct dependencies between classes. 
    public interface ITransferSource
    {
        long AccountNumber { get; set; }
        decimal Balance { get; set; }
        void RemoveFunds(decimal value);
    }
    public interface ITransferDestination
    {
        long AccountNumber { get; set; }
        decimal Balance { get; set; }
        void AddFunds(decimal value);
    }
    public class BOABankAccount : ITransferSource, ITransferDestination
    {
        public long AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public void AddFunds(decimal value)
        {
            Balance += value;
        }
        public void RemoveFunds(decimal value)
        {
            Balance -= value;
        }
    }
    public class TransferAmounts
    {
        public decimal Amount { get; set; }
        public void Transfer(ITransferSource TransferSource, ITransferDestination TransferDestination)
        {            
            TransferSource.RemoveFunds(Amount);
            TransferDestination.AddFunds(Amount);
        }
    }    
/*
    Advantage in above example after applying DIP:

    1. Higher level classes refer to their dependencies using abstractions, such as interfaces or abstract classes i.e. loose coupling. 
    2. Lower level classes implement the interfaces, or inherit from the abstract classes.
    3. This allows new dependencies can be substituted without any impact. 
    4. Lower levels classes will not cascade upwards as long as they do not involve changing the abstraction.
    5. Increases the robustness of the software and improves flexibility. 
    6. Separation of high level classes from their dependencies raises the possibility of reuse of these larger areas of functionality. 
    7. Minimized risk to affect old funtionallity present in Higher level classes.
    8. Testing applies only for  newly added low level classes.
    9. Though using this principle implies an increased effort and a more complex code, but it is more flexible. 
 
    Note:
    In that case the creation of new low level objects inside the high level classes(if necessary) can not be done using the operator new. 
    Instead, some of the Creational design patterns can be used, such as Factory Method, Abstract Factory, Prototype.

    The Template Design Pattern is an example where the DIP principle is applied.   
  
    This principle cannot be applied for every class or every module. 
    If we have a class functionality that is more likely to remain unchanged in the future there is not need to apply this principle.
*/
    class DependencyInversionPrincipleDemo
    {
        public static void DIPDemo()
        {
             Console.WriteLine("\n\nDependency Inversion Principle Demo ");

            //Created abstract class/interfaces objects for low level classes.
            ITransferSource TransferSource = new BOABankAccount();
            TransferSource.AccountNumber = 123456789;
            TransferSource.Balance = 1000;
            Console.WriteLine("Balance in Source Account : " + TransferSource.AccountNumber + " Amount " + TransferSource.Balance); 

            ITransferDestination TransferDestination = new BOABankAccount();
            TransferDestination.AccountNumber = 987654321;
            TransferDestination.Balance = 0;
            Console.WriteLine("Balance in Destination Account : " + TransferDestination.AccountNumber + " Amount " + TransferDestination.Balance);
            
            
            TransferAmounts TransferAmountsObject = new TransferAmounts();
            TransferAmountsObject.Amount = 100;

            // High level class using abstract class/interface objects 
            TransferAmountsObject.Transfer(TransferSource, TransferDestination);
            Console.WriteLine("Transaction successful");

            Console.WriteLine("Balance in Source Account : " + TransferSource.AccountNumber + " Amount " + TransferSource.Balance);
            Console.WriteLine("Balance in Destination Account : " + TransferDestination.AccountNumber + " Amount " + TransferDestination.Balance);
        }
    }
}