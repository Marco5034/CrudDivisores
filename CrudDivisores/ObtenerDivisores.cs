using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDivisores
{
    public class ObtenerDivisores
    {
        public static List<int> GetDivisors(int number)
        {
            return Enumerable.Range(1, number).Where(n => number % n == 0).ToList();
        }
    }
}
