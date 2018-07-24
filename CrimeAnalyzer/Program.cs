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
            if (args == null || args.Length != 2)
            {
                Console.WriteLine("Incorrect arguments. Usage should be \"CrimeAnalyzer.exe <crime_csv_file_path> <report_file_path>\". \n");
                Console.ReadLine();
                Environment.Exit(1);
            }
            string CSVFilename = FileCheck(args[0]);
            string reportFilename = FileCheck(args[1]);

            List<CrimeYear> crimeStatsLIst = ReadCSV(CSVFilename);
            PrintList(crimeStatsLIst);
            Console.ReadLine();

        }
    

        static List<CrimeYear> ReadCSV(string filename)
        {
            {
                
                string line;
                int row = 0;
                List<CrimeYear> crimeStatsList = new List<CrimeYear>();
                
                try
                {
                    StreamReader sr = new StreamReader(filename);

                    string header = sr.ReadLine();                              //reads header
                    int numItemsInRow = header.Split(',').Length;               //number of items in CSV header
                   
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        row++;                                                  //keeps track of which row is being read. First row of numerical data is 1
                        int[] values = StringToInt(line.Split(','),row);
                        LengthCheck(values.Length, numItemsInRow, row);
                            
                        crimeStatsList.Add(new CrimeYear(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10]));
                        line = sr.ReadLine();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);                //display exceptions
                }
                finally
                {
                    
                }
                return null;    //placeholder

            }

        }


        static string FileCheck(string filename)
        {
            if (File.Exists(filename)) return filename;

            Console.WriteLine("The file {0} does not exist. Exiting program.",filename);
            Console.ReadLine();
            Environment.Exit(2);
            return null;
        }


        static int[] StringToInt(string[] strArray, int row)
        {
            int[] intArray = new int[strArray.Length];                             //creates array of integers with same number of elements as array passed to this method
            int i = 0;
            foreach (string element in strArray)
            {
                if (!Int32.TryParse(strArray[i], out intArray[i]))                       //convert strings to integers. If it fails, exit program
                {
                    Console.WriteLine("Error occured while reading row {0}:\n", row);
                    Console.WriteLine("Could not convert data item to integer. Entry must be numerical.");
                    Console.ReadLine();
                    Environment.Exit(3);
                }
            }
            return intArray;
        }


        static void LengthCheck(int itemsInList, int rowLength, int row)
        {
            if(itemsInList != rowLength)
            {
                Console.WriteLine("Error occured while reading row {0}:\n",row);
                Console.WriteLine("Row contains {0} values. It should contain {1}",rowLength,itemsInList);
                Console.ReadLine();
                Environment.Exit(4);
            }
        }

        static void PrintList(List<CrimeYear> crime)
        {
            foreach(var listItem in crime)
            {
                Console.WriteLine("Year: {0}, Rape: {1}\n", listItem.year, listItem.rape);
            }
        }

        
    }
}
