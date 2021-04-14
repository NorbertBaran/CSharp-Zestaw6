using System;
using System.Reflection;

namespace Cw6
{
    public class Customer 
    {
        private string _name;
        protected int _age;
        public bool isPreffered;

        public Customer(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Customer name!");
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Address { get; set; }
        public int SomeValue { get; set; }

        public int ImportantCalculation()
        {
            return 1000;
        }

        public void ImportantVoid()
        {
        }

        public enum SomeEnumeration
        {
            ValueOne = 1,
            ValueTwo = 2
        }

        public class SomeNestedClass
        {
            private string _someString;
        }
    }

    class Zad2
    {
        public static void Main2(string[] args)
        {
            Console.WriteLine("Fields: ");

            Console.WriteLine("-- Public: ");
            
            var pFields = typeof(Customer).GetFields();
            foreach (var pField in pFields)
            {
                Console.WriteLine("Name: {0}, Field Type: {1}", pField.Name, pField.FieldType);
            }

            Console.WriteLine("-- Non public: ");
            
            var npFields = typeof(Customer).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var npField in npFields)
            {
                Console.WriteLine("Name: {0}, Field Type: {1}", npField.Name, npField.FieldType);
            }

            Console.WriteLine("Methods:");
            
            var methods = typeof(Customer).GetMethods();
            foreach (var method in methods)
            {
                Console.WriteLine("Name: {0}, Returned Type: {1}", method.Name, method.ReturnType);
            }
            
            Console.WriteLine("Nested types: ");
            
            var nTypes = typeof(Customer).GetNestedTypes();
            foreach (var nType in nTypes)
            {
                Console.WriteLine("Name: {0}", nType.Name);
            }
            
            Console.WriteLine("Properties: ");
            
            var properties = typeof(Customer).GetProperties();
            foreach (var property in properties)
            {
                Console.WriteLine("Name: {0}, Property Type: {1}", property.Name, property.PropertyType);
            }
            
            Console.WriteLine("Members: ");
            
            var members = typeof(Customer).GetMembers();
            foreach (var member in members)
            {
                Console.WriteLine("Name: {0}, Member Type: {1}", member.Name, member.MemberType);
            }
            
        }
    }
}