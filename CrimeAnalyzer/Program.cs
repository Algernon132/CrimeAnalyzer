using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 * Charles Bruscato
 * July 24, 2018
 * Musser IT 2001 
 * Final Project!
 */
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
            string reportFilename = args[1];

            List<CrimeYear> crimeStatsList = ReadCSV(CSVFilename);
            
            string report = MakeReport(crimeStatsList);

            Console.WriteLine(report);
            WriteFile(report, reportFilename);
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
                return crimeStatsList;
            }

        }


        static string FileCheck(string filename)    //check if file exists
        {
            if (File.Exists(filename)) return filename;

            Console.WriteLine("The file {0} does not exist. Exiting program.",filename);
            Console.ReadLine();
            Environment.Exit(2);
            return null;
        }


        static int[] StringToInt(string[] strArray, int row)    //convert array of strings to array of integers
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
                i++;

            }
            return intArray;
        }


        static void LengthCheck(int itemsInList, int rowLength, int row)    //confirms that the number of data entries in a row is equal to the number of headers in the given file
        {
            if(itemsInList != rowLength)
            {
                Console.WriteLine("Error occured while reading row {0}:\n",row);
                Console.WriteLine("Row contains {0} values. It should contain {1}",rowLength,itemsInList);
                Console.ReadLine();
                Environment.Exit(4);
            }
        }

        static string MakeReport(List<CrimeYear> crimeStatsList)    //takes list of crime stats and uses LINQ queries to generate a readable report
        {
            /*
             * Collect data from CSV list using LINQ. Will use this data to create report
             */

            var years = from crimeStats in crimeStatsList select crimeStats.year;

            var murdersUnder15k = from crimeStats in crimeStatsList where crimeStats.murder < 15000 select crimeStats.year;

            var yrsRobberyOver500k = from crimeStats in crimeStatsList where crimeStats.robbery > 500000 select crimeStats.year;
            var numRobberyOver500k = from crimeStats in crimeStatsList where crimeStats.robbery > 500000 select crimeStats.robbery;

            var violentIn2010 = from crimeStats in crimeStatsList where crimeStats.year == 2010 select crimeStats.violent;
            var popIn2010 = from crimeStats in crimeStatsList where crimeStats.year == 2010 select crimeStats.population;

            var murderPerYear = from crimeStats in crimeStatsList select crimeStats.murder; 

            var murder94to97 = from crimeStats in crimeStatsList where crimeStats.year >= 1994 && crimeStats.year <= 1997 select crimeStats.murder;

            var murder10to13 = from crimeStats in crimeStatsList where crimeStats.year >= 2010 && crimeStats.year <= 2013 select crimeStats.murder;

            var theft99to04 = from crimeStats in crimeStatsList where crimeStats.year >= 1999 && crimeStats.year <= 2004 select crimeStats.theft;

            var MVTPerYear = from crimeStats in crimeStatsList select crimeStats.mvt;

            /*
             * Find necessary values needed to create report
             */

            int minYear = years.Min();
            int maxYear = years.Max();
            int numYears = years.Count();
            float violent2010PerCapita = (float)violentIn2010.Sum() / popIn2010.First();
            float avgMurder = murderPerYear.Sum() / numYears;
            float avgMurder94to97 = murder94to97.Sum() / murder94to97.Count();
            float avgMurder10to13 = murder10to13.Sum() / murder10to13.Count();
            int minThefts99to04 = theft99to04.Min();
            int maxThefts99to04 = theft99to04.Max();
            int maxMVT = MVTPerYear.Max();

            int[] yrsOver500kArray = yrsRobberyOver500k.Cast<int>().ToArray();
            int[] numOver500kArray = numRobberyOver500k.Cast<int>().ToArray();


            /*
             * Create report
             */

            string report = "Years included in data: " + minYear + " to " + maxYear + Environment.NewLine; ;
            report += "Number of years in data: " + numYears + Environment.NewLine;
            report += "Years murder is under 15000: ";
            foreach (var item in murdersUnder15k) report += item + " ";
            report += Environment.NewLine + Environment.NewLine;
            report += "  Years where robbery is greater than 500,000: ";
            for (int i = 0; i < yrsOver500kArray.Length; i++) report += Environment.NewLine + "       Year: " + yrsOver500kArray[i] + " --> Robberies: " + numOver500kArray[i];
            report += Environment.NewLine + Environment.NewLine;
            report += "Violent crime per capita in 2010: " + violent2010PerCapita + Environment.NewLine;
            report += "Average murders per year from " + minYear + " to " + maxYear + ": " + avgMurder + Environment.NewLine;
            report += "Average murders per year from 1994 to 1997: " + avgMurder94to97 + Environment.NewLine;
            report += "Average murders per year from 2010 to 2013: " + avgMurder10to13 + Environment.NewLine;
            report += "Minimum thefts per year from 1999 to 2004: " + minThefts99to04 + Environment.NewLine;
            report += "Maximum thefts per year from 1999 to 2004: " + maxThefts99to04 + Environment.NewLine;
            report += "Year with highest number of motor vehicle thefts: " + maxMVT + Environment.NewLine;


            return report;

        }

        static void WriteFile(string fileContent, string filename)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename); //Will save to current directory

                sw.WriteLine(fileContent);

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not write file. Exception: " + e.Message);   //display exceptions
            }
            finally
            {
                Console.WriteLine("Report {0} was successfully written." , filename);
            }
        }

    }
}
