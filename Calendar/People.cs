using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
    {
    [Serializable]
    public class People:IComparable<People>,IComparable, IEquatable<People?>
        {
        private int myAge;

        public int Age
            {
            get { return myAge; }
            set {
            if (value<18 || value>100) throw new ArgumentException("No ok");
            
            myAge = value; }
            }




        public People() { }

        public People(int age, string name)
            {
         
            Age = age;
            Name = name;
            }

        public string Name { get; set; }

        public override string ToString()
            {
            return $"{{{nameof(Name)}={Name}}}";
            }

        public int CompareTo(People? other)
            {
            return Age - other.Age;
            }

        public int CompareTo(object? obj)
            {
            throw new NotImplementedException();
            }

        public override bool Equals(object? obj)
            {
            return Equals(obj as People);
            }

        public bool Equals(People? other)
            {
            return other is not null &&
                   Name == other.Name;
            }
        }

    }
