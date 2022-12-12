using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calendar
    {
    [Serializable]
    public class Meeting:IComparable<Meeting>, IEquatable<Meeting?>
        {
        public Meeting()
            {
            }

        public Meeting(string myName, List<People> myPeoples, Interval interval)
            {
            MyName = myName;
            MyPeoples = myPeoples;
            MyInterval = interval;
            }
     
       
        [XmlIgnore]
        public Calendar MyCalendar { get; set; }
        [XmlIgnore]
        public Room MyRoom { get; set; }


     
        public string MyName { get; set; }
        
        public List<People> MyPeoples { get; set; }



       
        public Interval MyInterval { get; set; }

        public int CompareTo(Meeting? other)
            {
            return MyInterval.CompareTo(other.MyInterval);
            }

        public override bool Equals(object? obj)
            {
            return Equals(obj as Meeting);
            }

        public bool Equals(Meeting? other)
            {
            return other is not null &&
                   EqualityComparer<Interval>.Default.Equals(MyInterval, other.MyInterval);
            }

        /*  public override bool Equals(object? obj)
              {

              return obj is Meeting meeting &&
                     MyInterval.Equals(meeting.MyInterval);
              }*/

        public override int GetHashCode()
            {
            return 1;
            }

        public override string ToString()
            {
            return $" время {MyInterval}";
            }
        }
    }
