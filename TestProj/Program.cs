using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj
{
    public class Person
    {
        private int age;
        private string name;
        public Person(int age, string name)
        {
            this.age = age;
            this.name = name;
        }

        public Person()
        {

        }

        public override String ToString()
        {
            return "Name : " + this.name + " , Age : " + this.age;
        }
    }

    public class Student : Person
    {
        private string className;

        public Student(int age, string name, string className)
            : base(age , name)
        {
            this.className = className;
        }

        public override string ToString()
        {
            return base.ToString() + " Class Name : " + this.className;
        }
    }

    public class Fruit
    {
        private static Dictionary<string, Fruit> types = new Dictionary<string, Fruit>();
        private string type;

        /// <summary>
        /// using a private constructor to force use of the factory method.
        /// </summary>
        /// <param name="type">Type of fruit</param>
        private Fruit(String type)
        {
            this.type = type;
        }

        /// </summary>
        /// <param name="type">Any string that describes a fruit type, e.g. "apple"</param>
        /// <returns>The Fruit instance associated with that type.</returns>
        public static Fruit getFruit(string type)
        {
            Fruit f;

            if (!types.TryGetValue(type, out f))
            {
                f = new Fruit(type); // lazy initialization
                types.Add(type, f);
            }

            return f;
        }

        public static void printCurrentTypes()
        {
            if (types.Count > 0)
            {
                Console.WriteLine("Number of instances made = {0}", types.Count);
                foreach (KeyValuePair<string, Fruit> kvp in types)
                {
                    Console.WriteLine(kvp.Key);
                }
                Console.WriteLine();
            }
        }

    }
    class Program
    {
        static void Ref(ref Person pers)
        {
            pers = new Person(33, "Iven");
        }

        static Person NonRef(Person pers)
        {
            pers = new Person(33, "Iven");

            return pers;
        }

        static void Main(string[] args)
        {
            Fruit.getFruit("Banana");
            Fruit.printCurrentTypes();

            Fruit.getFruit("Apple");
            Fruit.printCurrentTypes();

            // returns pre-existing instance from first
            // time Fruit with "Banana" was created
            Fruit.getFruit("Banana");
            Fruit.printCurrentTypes();

            Console.ReadLine();

        }
    }
}
