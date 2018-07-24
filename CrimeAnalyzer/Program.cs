using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            string CSVFilename = args[0];
            string ReportFilename = args[1];


        }


        static void OpenCSB(string filename)
        {

        }

        static void ReadCSV(string filename)
        {
            {
                string content = "";
                string line;


                try
                {
                    StreamReader sr = new StreamReader(Filename);

                    line = sr.ReadLine();
                    while (line != null)
                    {
                        content += line;
                        line = sr.ReadLine();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);   //display exceptions
                }
                finally
                {

                }
                

            }

        }

        static void fileCheck(string filename)
        {
            if (File.Exists(filename)) return;

            Console.WriteLine("The file {0} does not exist. Exiting program.",filename);
            Console.ReadLine();
            Environment.Exit(2);
        }
    }
}
