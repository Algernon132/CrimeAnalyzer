using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args==null || args.Length != 2)
            {
                Console.WriteLine("Incorrect arguments. Usage should be \"CrimeAnalyzer.exe <crime_csv_file_path> <report_file_path>\". \n");
                Console.ReadLine();
                Environment.Exit(1);
            }


        }


        static void OpenCSB(string filename)
        {

        }

        static void ReadCSV(string filename)
        {

        }
    }
}
