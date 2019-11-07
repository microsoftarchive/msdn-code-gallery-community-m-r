using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ101Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqSamples samples = new LinqSamples();

            #region Restriction Operators 1 to 5

            samples.Linq1(); // This sample  uses the where clause  to find all elements  of an array with a value less than 5
            samples.Linq2(); // This sample uses the where clause to find all products that are out of stock
            samples.Linq3(); // This sample uses the where clause to find all products that are in  stock and cost more than 3.00 per unit
            samples.Linq4(); // This sample uses the where  clause to find all customers in Washington and then it uses a foreach loop to iterate over the orders collection that belongs to each customer
            samples.Linq5(); // This sample demonstrates an indexed where clause that returns digits whose name is shorter than their value

            #endregion Restriction Operators 1 to 5

            #region Projection Operators 6 to 19

            samples.DataSetLinq6();     // This sample uses select to produce a sequence of ints one higher than those in an existing array of ints.
            samples.DataSetLinq7();     // This sample uses select to return a sequence of just the names of a list of products.
            samples.DataSetLinq8();     // This sample uses select to produce a sequence of strings representing the text version of a sequence of ints.
            samples.DataSetLinq9();     // This sample uses select to produce a sequence of the uppercase and lowercase versions of each word in the original array.
            samples.DataSetLinq10();    // This sample uses select to produce a sequence containing text representations of digits and whether their length is even or odd.
            samples.DataSetLinq11();    // This sample uses select to produce a sequence containing some properties of Products...
            samples.DataSetLinq12();    // This sample uses an indexed Select clause to determine if the value of ints in an array match their position in the array.
            samples.DataSetLinq13();    // This sample combines select and where to make a simple query that returns the text form of each digit less than 5.
            samples.DataSetLinq14();    // This sample uses a compound from clause to make a query that returns all pairs of numbers...
            samples.DataSetLinq15();    // This sample uses a compound from clause to select all orders where the order total is less than 500.00.
            samples.DataSetLinq16();    // This sample uses a compound from clause to select all orders where the order was made in 1998 or later.
            samples.DataSetLinq17();    // This sample uses a compound from clause to select all orders where order total is greater than 2000.00...
            samples.DataSetLinq18();    // This sample uses multiple from clauses so that filtering on customers can be done before selecting their orders...
            samples.DataSetLinq19();    // This sample uses an indexed SelectMany clause to select all orders...

            #endregion Projection Operators 6 to 19

            #region Partitioning Operators 20 to 27

            samples.Linq20(); // This sample uses Take to get only the first 3 elements of the array
            samples.Linq21(); // This sample uses Take to get the first 3 orders from customers in Washington
            samples.Linq22(); // This sample uses Skip to get all but the first four elements of the array
            samples.Linq23(); // This sample uses Take to get all but the first 2 orders from customers in Washington
            samples.Linq24(); // This sample uses TakeWhile to return elements starting from the beginning of the array until a number is read whose value is not less than 6
            samples.Linq25(); // This sample uses TakeWhile to return elements starting from the beginning of the array until a number is hit that is less than its position in the array
            samples.Linq26(); // This sample  uses SkipWhile to get the  elements of the array  starting from the first element divisible by 3
            samples.Linq27(); // This sample  uses SkipWhile to get the  elements of the array  starting from the first element less than its position

            #endregion Partitioning Operators 20 to 27

            #region Ordering Operators 28 to 39

            samples.Linq28(); // This sample uses orderby to sort a list of words alphabetically
            samples.Linq29(); // This sample uses orderby to sort a list of words by length
            samples.Linq30(); // This sample uses orderby to sort a list of products by name. Use the \"descending\" keyword at the end of the clause to perform a reverse ordering
            samples.Linq31(); // This sample uses an  OrderBy clause with a custom comparer to do a case-insensitive sort of the words in an array
            samples.Linq32(); // This sample uses  orderby and  descending to sort a list of doubles from highest to lowest
            samples.Linq33(); // This sample uses  orderby to sort a list of products by units in stock from highest to lowest
            samples.Linq34(); // This sample uses method syntax to call OrderByDescending  because it enables you to use a custom comparer
            samples.Linq35(); // This sample uses a compound  orderby to  sort a list of digits,  first by length of their name, and then alphabetically by the name itself
            samples.Linq36(); // The first query in this sample uses method syntax to call OrderBy and ThenBy with a 
            // custom comparer to sort first by word length and then by a case-insensitive sort of 
            // the words in an array.  The second two queries show another way to perform the same task
            samples.Linq37(); // This sample uses a compound  orderby to sort a list of products,  first by category, and then by unit price, from highest to lowest
            samples.Linq38(); // This sample uses an OrderBy and a ThenBy clause with a custom comparer to sort first 
            // by word length and  then by a case-insensitive  descending  sort of  the words in an array

            samples.Linq39(); // This sample uses Reverse to  create a list of  all digits in the  array whose second letter is 'i' that is reversed from the order in the original array

            #endregion OrderingOperators

            # region Grouping Operators 40 to 45

            samples.DataSetLinq40();    // This sample uses group by to partition a list of numbers by their remainder when divided by 5.
            samples.DataSetLinq41();    // This sample uses group by to partition a list of words by their first letter.
            samples.DataSetLinq42();    // This sample uses group by to partition a list of products by category.
            samples.DataSetLinq43();    // This sample uses group by to partition a list of each customer's orders, first by year, and then by month.
            samples.DataSetLinq44();    // This sample uses GroupBy to partition trimmed elements of an array using a custom comparer that matches words that are anagrams of each other.
            samples.DataSetLinq45();    // This sample uses GroupBy to partition trimmed elements of an array using a custom comparer that matches words that are anagrams of each other, and then converts the results to uppercase.

            #endregion Grouping Operators

            #region Set Operators 46 to 53

            samples.Linq46(); // This sample uses Distinct to remove  duplicate  elements in a sequence of factors of 300
            samples.Linq47(); // This sample uses Distinct to find the unique Category names
            samples.Linq48(); // This sample uses Union to create  one sequence that contains the unique values from both arrays
            samples.Linq49(); // This sample uses the Union method to create  one sequence that contains the unique first 
            // letter from both product and customer names. Union is only available through method syntax
            samples.Linq50(); // This sample uses Intersect to create one sequence that contains the common values shared by both arrays
            samples.Linq51(); // This sample uses Intersect  to create one sequence that contains the common first letter from both product and customer names
            samples.Linq52(); // This sample uses Except to create a sequence that contains the values from numbersA that are not also in numbersB
            samples.Linq53(); // This sample uses Except to create one  sequence that contains the 1st letters of product names that are not also first letters of customer names

            #endregion Set Operators 46 to 53

            #region Conversion Operators 54 to 57

            samples.Linq54(); // This sample uses ToArray to immediately evaluate a sequence into an array
            samples.Linq55(); // This sample uses ToList to immediately evaluate a sequence into a List<T>
            samples.Linq56(); // This sample uses ToDictionary to immediately  evaluate a  sequence  and a related key expression into a dictionary
            samples.Linq57(); // This sample uses OfType to return only the elements of the array that are of type double

            #endregion ConversionOperators

            #region Element Operators 58 to 64

            samples.Linq58(); // This sample uses First to return the first matching element as a Product, instead of as a sequence containing a Product
            samples.Linq59(); // This sample uses First to find the first element in the array that starts with 'o'
            samples.Linq61(); // This sample uses FirstOrDefault to try to return the first element of the sequence, 
            // unless there are  no elements,  in which case  the default  value for that type is returned.  FirstOrDefault is useful for creating outer joins

            samples.Linq62(); // This sample uses FirstOrDefault to return the first product whose ProductID is 789 as a single Product object, unless there is no match, in which case null is returned
            samples.Linq64(); // This sample uses ElementAt to retrieve the second number greater than 5 from an array

            #endregion Element Operators

            //Generation Operators
            samples.Linq65(); // This sample uses Range to generate a sequence of numbers from 100 to 149 that is used to find which numbers in that range are odd and even
            samples.Linq66(); // This sample uses Repeat to generate a sequence that contains the number 7 ten times

            #region Quantifiers 67 to 72

            samples.Linq67(); // This sample  uses Any to determine if any of the words in the array contain the substring 'ei'            
            samples.Linq69(); // This sample uses Any to return a grouped a list of products only for categories that have at least one product that is out of stock
            samples.Linq70(); // This sample uses All to determine whether an array contains only odd numbers
            samples.Linq72(); // This sample uses All to return a grouped a list of products only for categories that have all of their products in stock

            #endregion Quantifiers 67 to 72

            #region Aggregate Operations 73 to 93

            samples.Linq73(); // This sample uses Count to get the number of unique prime factors of 300
            samples.Linq74(); // This sample uses Count to get the number of odd ints in the array
            samples.Linq76(); // This sample uses Count to return a list of customers and how many orders each has
            samples.Linq77(); // This sample uses Count to return a list of categories and how many products each has
            samples.Linq78(); // This sample uses Sum to add all the numbers in an array
            samples.Linq79(); // This sample uses Sum to get the total number of characters of all words in the array
            samples.Linq80(); // This sample uses Sum to get the total units in stock for each product category
            samples.Linq81(); // This sample uses Min to get the lowest number in an array
            samples.Linq82(); // This sample uses Min to get the length of the shortest word in an array
            samples.Linq83(); // This sample uses Min to get the cheapest price among each category's products
            samples.Linq84(); // This sample uses Min to get the products with the lowest price in each category
            samples.Linq85(); // This sample uses Max to get the highest number in an array. Note that the method returns a single value
            samples.Linq86(); // This sample uses Max to get the length of the longest word in an array
            samples.Linq87(); // This sample uses Max to get the most expensive price among each category's products
            samples.Linq88(); // This sample uses Max to get the products with the most expensive price in each category
            samples.Linq89(); // This sample uses Average to get the average of all numbers in an array
            samples.Linq90(); // This sample uses Average to get the average length of the words in the array
            samples.Linq91(); // This sample uses Average to get the average price of each category's products
            samples.Linq92(); // This sample uses Aggregate to create a running product on the array that calculates the total product of all elements
            samples.Linq93(); // This sample uses Aggregate to create a running account balance that subtracts each 
            // withdrawal from the initial balance of 100, as long as the balance never drops below 0

            #endregion Aggregate Operations

            //Miscellaneous Operators
            samples.Linq94(); // This sample  uses Concat  to create one  sequence that  contains each array's values, one after the other
            samples.Linq95(); // This sample uses Concat to create one sequence that contains the names of all customers and products, including any duplicates
            samples.Linq96(); // This sample uses SequenceEquals to see if two sequences match on all elements in the same order
            samples.Linq97(); // This sample  uses SequenceEqual to see if two sequences match on all elements in the same order

            //Custom Sequence Operators
            samples.DataSetLinq98();    // This sample uses a user-created sequence operator, Combine, to calculate the dot product of two vectors.

            samples.Linq99();  // The following sample shows how query execution is deferred until the query is enumerated at a foreach statement
            samples.Linq100(); // The following sample shows how queries can be executed immediately, and their results stored in memory, with methods such as ToList
            samples.Linq101(); // The following sample shows how, because of deferred execution, queries can be used again after data changes and will then operate on the new data

            #region Join Operators 102 to 108

            samples.Linq102(); // This sample shows how to perform a simple inner equijoin of two sequences to produce 
            // a flat  result set  that consists  of each  element in suppliers that has a matching element in customers
            samples.Linq103(); // A group join produces a hierarchical sequence.  The following query is an inner join 
            // that produces a sequence of objects, each of which has a key and an inner sequence of all matching elements
            samples.Linq104(); // The group join operator is more general than join, as this slightly more verbose version of the cross join sample shows
            samples.Linq105(); // For each customer in the table of customers, this query returns all the suppliers from 
            // that same country,  or else a string  indicating  that no suppliers  from that country were found
            samples.Linq106(); // For each customer in the table of customers, this query returns all the suppliers from 
            // that same country,  or else a string  indicating  that no suppliers  from that country were found
            samples.Linq107(); // For each supplier in the table of suppliers, this query returns all the customers from 
            // the same city and country,  or else a string  indicating  that no customers  from that 
            // city/country were found.  Note the use of anonymous  types to encapsulate the multiple key values
            samples.Linq108();
            #endregion Join Operators 102 to 108
        }
    }
}