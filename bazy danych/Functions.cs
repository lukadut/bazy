using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy_danych
{
    class Functions
    {
        public static string[] classes = { "1", "2", "3", "4.1", "4.2", "4.3", "5.1", "5.2", "6.1", "6.2", "7", "8", "9" };
        public static int FindDriver(int IdInBase, List<Drivers> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == IdInBase)
                    return i;
            }
            return -1;
        }

        public static int FindCar(int IdInBase, List<Cars> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == IdInBase)
                    return i;
            }
            return -1;
        }

        public static bool AllowedPlate(string newPlate, List<Cars>list)
        {
            //newPlate.Skip(5);
            newPlate = System.Text.RegularExpressions.Regex.Replace(newPlate,@"\s","");
            for (int i = 0; i < list.Count; i++)
            {
                
                //if (list[i].Plate == newPlate)
                if(String.Equals(newPlate, System.Text.RegularExpressions.Regex.Replace(list[i].Plate,@"\s","")))
                    return false;
            }
            return true;
        }

        public static bool[] ExpADR(string adr)
        {

            
            bool[] result = new bool[13];
            string[] explode = adr.Split(',');
            int i = 0;
            foreach (var item in classes)
            {
                if (explode.Contains(item))
                {
                    result[i] = true;
                }
                else
                {
                    result[i] = false;
                }
                i++;
            }
            return result;
        }
    }
}
