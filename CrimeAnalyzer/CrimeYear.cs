using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeAnalyzer
{
    public class CrimeYear
    {
        public int year;
        public int population;
        public int violent;
        public int murder;
        public int rape;
        public int robbery;
        public int aggravated;
        public int property;
        public int burglary;
        public int theft;
        public int mvt;    //motor vehicle theft

        public CrimeYear(int Year, int Population, int Violent, int Murder, int Rape, int Robbery, int Aggravated,
                         int Property, int Burglary, int Theft, int Mvt)
        {
            this.year = Year;
            this.population = Population;
            this.violent = Violent;
            this.murder = Murder;
            this.rape = Rape;
            this.robbery = Robbery;
            this.aggravated = Aggravated;
            this.property = Property;
            this.burglary = Burglary;
            this.theft = Theft;
            this.mvt = Mvt;
        }   //constructor
    }
}
