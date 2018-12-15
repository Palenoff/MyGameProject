using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Example
{
    class Program
    {
        static void Arr_Int(List<int> int_list)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            foreach (int n in int_list)
                result[n] = result.Keys.Contains(n) ? ++result[n] : 1;
            foreach (KeyValuePair<int, int> keyValuePair in result)
                Console.WriteLine(keyValuePair);

        }

        static void Arr_Linq_Query(MyList<MyClass> arr)
        {
            var query =
                            from nums in arr
                            group nums by nums into Group
                            select new
                            {
                                n = Group.Key,
                                Count = Group.Count(),
                            };
            foreach (var element in query)
                Console.Write("Количество элемента {" + element.n + "} с полем Data = \"" + element.n.Data + "\" равняется " + element.Count + "\n");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Arr_Int(new List<int> { 1, 2, 3, 4, 11, 11, 7, 8, 2, 5, 6, 7, 1, 3, 4, 8, 9 });
            MyClass obj1 = new MyClass("1");
            MyClass obj2 = new MyClass("2");
            MyClass obj3 = new MyClass("3");
            MyList<MyClass> myClasses = new MyList<MyClass>(8) { obj1, obj2, obj1, obj3, obj2, obj3, obj3, obj3 };
            Arr_Linq_Query(myClasses);
        }
    }
}
