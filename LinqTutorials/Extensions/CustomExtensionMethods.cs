using LinqTutorials.Models;
using System.Collections.Generic;
using System.Linq;

namespace LinqTutorials.Extensions
{
    public static class CustomExtensionMethods
    {
        public static IEnumerable<Emp> GetEmpsWithSubEmps(this IEnumerable<Emp> emps)
        {
            var array = emps as Emp[] ?? emps.ToArray();
            return array.Where(e => array.Any(e2 => e2.Mgr == e))
                .OrderBy(e => e.Ename)
                .ThenByDescending(e => e.Salary);
        }
    }
}
