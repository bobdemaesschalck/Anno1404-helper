using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Anno1404Helper.BuildingTypes;

namespace Anno1404Helper.IO
{
    class BuildingReader
    {
        
        StreamReader sr = new StreamReader(@"..\..\buildings.bct");
        List<Building> EndOfLineBuildings = new List<Building>();
        List<Building> WorkInProgressBuildings = new List<Building>();
        List<String> content = new List<string>();

        public BuildingReader()
        {

        }

        public void readBuildings()
        {
            while (!sr.EndOfStream)
            {
                content.Add(sr.ReadLine());
            }
        }

        public List<Building>[] getBuildings()
        {
            readBuildings();
            Building b = null;
            Boolean newBuilding = false;
            try
            {
                foreach (String line in content)
                {
                    if (!newBuilding && !line.Equals(""))
                    {
                        if (line.Equals("(final)"))
                        {
                            b = new EndOfLineBuilding();
                            newBuilding = true;
                        }
                        else if (line.Equals("(nonFinal)"))
                        {
                            b = new WorkInProgressBuilding();
                            newBuilding = true;
                        }
                        else
                        {
                            throw new Exception("Syntax error at line " + (content.IndexOf(line) + 1) + ": " + line + " is not a supported building type");
                        }
                    }
                    else if (newBuilding)
                    {
                        if (line.Equals("(end)"))
                        {
                            if (b is EndOfLineBuilding)
                            {
                                EndOfLineBuildings.Add(b);
                            }
                            else if (b is WorkInProgressBuilding)
                            {
                                WorkInProgressBuildings.Add(b);
                            }
                            newBuilding = false;
                        }
                        else
                        {
                            String[] lineparts = line.Split('=');
                            switch (lineparts[0])
                            {
                                case "goods":
                                    b.Goods = lineparts[1];
                                    break;
                                case "name":
                                    b.Name = lineparts[1];
                                    break;
                                case "satisfies":
                                    b.Satisfies = Array.ConvertAll(lineparts[1].Split(','), Decimal.Parse);
                                    break;
                                case "dependencies":
                                    b.Dependencies = lineparts[1].Split(',');
                                    break;
                                case "dependencyDivisors":
                                    b.DependencyDivisors = Array.ConvertAll(lineparts[1].Split(';'), Decimal.Parse);
                                    break;
                                default:
                                    throw new Exception("variable " + lineparts[0] + " not recognized");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return new List<Building>[] { EndOfLineBuildings, WorkInProgressBuildings };
        }
    }
}
