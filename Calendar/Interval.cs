using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
    {
    [Serializable]
    public class Interval:IComparable<Interval>
        {
    
        public Interval(DateTime from, DateTime to)
            {
            this.from = from;
            this.to = to;
            if(from > to) throw new ArgumentException("no good argument");
            }

        public DateTime from { get; set; }
        public DateTime to { get; set; }

        public int CompareTo(Interval obj)
            {

            if(to.CompareTo(obj.from) < 0) return -1;
            if(from.CompareTo(obj.to) > 0) return 1;
            else return 0;
          

            }

        public override bool Equals(object? obj)
            {
            return
            obj is Interval interval && CompareTo(interval) == 0;
            }

        public bool Equals(Interval other)
            {
        

           return other is Interval interval && CompareTo(interval) == 0;


            }

        public override int GetHashCode()
            {
            return HashCode.Combine(from, to);
            }



        public override string ToString()
            {
            return $"{{{nameof(from)}={from.ToString()}, {nameof(to)}={to.ToString()}}}";
            }
        }
    }
