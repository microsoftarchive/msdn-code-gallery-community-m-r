/*
 * James
 * 5-4-15
 * 
 * This program generates 100 rolls of 2 dice
 * and displays any doubles that are rolled,
 * along with the roll number (e.g. "On the
 * 10th roll, you rolled doubles: 5, 5")
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeEyes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare random number varable
            Random randNum = new Random();
            int rollNum = 1;
            int die1;
            int die2;
            
            /*
             * The random number sequence starts at 1
             * and while it's less than 101 it increments
             * by one more random number.  This will
             * give us a total of 100 random numbers
             * between 1 and 6.
             * 
             */

            for (rollNum = 1; rollNum < 101; rollNum++)
            {
                // Generate random number
                die1 = randNum.Next(1, 7);
                die2 = randNum.Next(1, 7);

                Console.WriteLine("Roll " + rollNum + ": " + die1 + " " + die2);

                /*
                 * If doubles are rolled, display what number roll they were rolled on
                 * as well as the double numbers on the dice
                 * 
                 */

                if (die1 == die2)
                {
                    // Outputs the doubles rolled to the console for each pass of the loop
                    Console.WriteLine("");
                    Console.WriteLine("On roll " + rollNum + " you rolled doubles: " + die1 + " " + die2);
                    Console.WriteLine("");
                }
            }
        }
    }
}