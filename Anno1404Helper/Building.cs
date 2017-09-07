using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anno1404Helper.BuildingTypes;

namespace Anno1404Helper
{
    public abstract class Building
    {
        public String Goods { get; set; }
        public String Name { get; set; }
        public decimal[] Satisfies = new decimal[7];
        public abstract String Calculate(decimal[] inhabitants, List<Building> workInProgressBuildings, decimal divisor, decimal[] satisfies);
        public decimal Amount;
        public String[] Dependencies = null;
        public decimal[] DependencyDivisors = null;
        public Boolean isUsed()
        {
            if (Amount == 0)
            {
                return false;
            }
            return true;
        }
    }
}
