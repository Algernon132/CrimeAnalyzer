using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeAnalyzer
{
    class CrimeYear
    {
        int year;
        int population;
        int violent;
        int murder;
        int rape;
        int robbery;
        int aggravated;
        int property;
        int burglary;
        int theft;
        int mvt;    //motor vehicle theft

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
