using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1404Helper.BuildingTypes
{
    class WorkInProgressBuilding : Building
    {
        public override string Calculate(decimal[] inhabitants, List<Building> workInProgressBuildings, decimal divisor, decimal[] satisfies)
        {
            for (int i = 0; i < Satisfies.Length; i++)
            {
                Satisfies[i] = satisfies[i] / divisor;
            }

            Amount = Calculator.calculate(Satisfies, inhabitants);
            decimal efficiency = Amount * 100;
            Amount = Math.Ceiling(Amount);

            if (Amount != 0)
                efficiency = Math.Round(efficiency / Amount);

            string labelText = "";

            if (Dependencies != null)
            {
                for (int i = 0; i < Dependencies.Length; i++)
                {
                    Building temp = workInProgressBuildings.Find(b => b.Name.Equals(Dependencies[i]));
                    labelText += temp.Calculate(inhabitants, workInProgressBuildings, DependencyDivisors[i], Satisfies);
                }
            }
            labelText += Name + ": " + Amount.ToString() + " (" + efficiency + "%)" + Environment.NewLine;
            return labelText;
        }
    }
}
