using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calendar
    {
    [Serializable]
    public class Room : IEquatable<Room?>
        {
     

        public Room()
            {
            MyMeeting = new HashSet<Meeting>();
            }

        public Room(string nameRoom, Calendar myCalendar)
            {
            NameRoom = nameRoom;
            MyCalendar = myCalendar;
            MyCalendar.MyRooms.Add(this);
            MyMeeting = new HashSet<Meeting>();
            }

        public string NameRoom { get; set; }
        [XmlIgnore]
        public Calendar MyCalendar { get; set; }



        /*[XmlIgnore]*/
        public HashSet<Meeting> MyMeeting { get; set; }

        public override bool Equals(object? obj)
            {
            return Equals(obj as Room);
            }

        public bool Equals(Room? other)
            {
            return other is not null &&
                   NameRoom == other.NameRoom;
            }

        public override int GetHashCode()
            {
            return HashCode.Combine(NameRoom);
            }

        public override string ToString()
            {
            return $"{{{nameof(NameRoom)}={NameRoom} " +
       /*     $"{nameof(MyCalendar)}={MyCalendar}, " +
            $"{nameof(MyMeeting)}={MyMeeting}" +*/
            $"}}";
            }
        }
    }
