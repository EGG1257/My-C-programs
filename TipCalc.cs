using System.Threading;
using System.IO;

internal class TipCalc
{
    private static void Main(string[] args)
    {
        double bill;
        double tip;
        int people;

        //input bill amount
        Console.Write("Enter bill amount: ");
        string sbill = Console.ReadLine();
        if (!double.TryParse(sbill, out bill) || bill <= 0)
        {
            Console.WriteLine("The entered value is invalid");
            return;
        }

        //input the tip amount
        Console.Write("Enter tip percentage: ");
        string stip = Console.ReadLine();
        if (!double.TryParse(stip, out tip) || tip <= 0)
        {
            Console.WriteLine("The entered value is invalid");
            return;
        }

        //calculate the percetage
        double tipout = (tip / bill) * 100;
        double output = tipout + bill;

        //output the tip amount and total bill
        Console.WriteLine("You should leave a tip of $" + tipout);
        Console.WriteLine("Total amount to pay is $" + output);

        //input the amount of people
        Console.Write("Enter the number of people: ");
        string speople = Console.ReadLine();
        if (!int.TryParse(speople, out people) || people <= 0)
        {
            Console.WriteLine("The entered value if invalid");
            return;
        }

        double split = output / people;

        Console.WriteLine("Each person pays $" + split);

        //File stuff

        for (int i = 0; i < people; i++)
        {
            Console.Write($"Enter the name of person #{(i + 1)}: ");
            string name = Console.ReadLine();
            File.WriteAllText($"{name}.txt", $"{name}\nTotal:${output}\nsplit into {people} persons, share amount:${split}");
        }


    }
}