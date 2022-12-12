using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calendar
    {
    [Serializable]
    public class Calendar
        {
        public Calendar() { }
        public Calendar(string myNameCalendar,
        HashSet<Room> myRooms = null,
                bool myModel=true)
            { 
            
         
            MyNameCalendar = myNameCalendar;

       /*     Заполняем 10 комнатами,если не передали в агрументе комнаті*/
            MyRooms = myRooms ?? new Faker<Room>()
            .RuleFor(q => q.NameRoom, w => w.Name.JobArea())
            .RuleFor(e => e.MyCalendar, r => this).
            Generate(10).ToHashSet();
            MyModel = myModel;
            }

        public string MyNameCalendar { get; set; }
       /* [XmlIgnore]*/
        public HashSet <Room> MyRooms { get; set; }
        public bool MyModel { get; set; }
 /*       public List<Meeting> MyMeetings { get; set; }*/



        public bool AddRoom(Room room)
            {
            if (MyModel)
                {
                MyRooms.Add(room);
                Console.WriteLine($"комната {room} добавлена");
                return true;
                }
                else{
                Console.WriteLine($"комнату {room,-30}  Нельзя добавить");
                return false;}
            
            }
        public void Change() {
            MyModel = !MyModel;
        
        }
        public bool AddMeeting(Meeting meeting)
        {
        if(MyModel)
            {
                foreach(var room in MyRooms )
                    {
                    if(room.MyMeeting.Add(meeting)) {
                      /*  Console.WriteLine("ok");*/
                      /*  Console.WriteLine($"meeting add {meeting} in {room}");*/
                       return true;
                    }
                    }

                Console.Write("нет свободн номеров  -");
                return false;
                }
            Console.Write("Режим запрещает  -");
        return false;
        }
        public override string ToString()
            {

            StringBuilder stringBuilder = new StringBuilder(new string('-', 80));
            stringBuilder.Append(Environment.NewLine).Append (MyNameCalendar).
            Append(Environment.NewLine);
            foreach(var room in MyRooms)
                {
                stringBuilder.Append(room).Append(Environment.NewLine);
                foreach(var meet in room.MyMeeting?? Enumerable.Empty<Meeting>())
                    {
                    stringBuilder.Append("\t"+meet).Append(Environment.NewLine);
                    }
                }


            return stringBuilder.ToString();
            }
        }
  

    }
