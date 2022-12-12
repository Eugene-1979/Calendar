using Bogus;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Serialization;


namespace Calendar
    {
    internal class Program
        {
        static void Main(string[] args)
            {

       

            Calendar calendar = new Calendar("CALENDAR");
            /*---------------------------------------------------------------------------------------------*/
            /*   заполняем календарь комнатами (10 при создании календаря+100)*/

            AddRooms(calendar, 10);
            /*-------------------------------------------------------------------------------------------*/
            /*   создаем встречи длительностью час и добавляем в календарь
              календарь встречи раскидівает по свободнім комнатам в заданое время встречи
                если комната занята в данное время-то ищет далее по списку*/

            AddMeetings(calendar, 200, 10);
            /*----------------------------------------------------------------------------------------------*/
            Console.WriteLine(calendar);
            /*переключаем режим*/

            calendar.Change();
          /*  -------------------------------------------------------------------------------*/
            Console.WriteLine("Хотим добавить встречи но новій режим не даст");
            Console.WriteLine("Press any key");
            Console.ReadLine();

            /*--------------------------------------------------------------------------------------------------*/
            AddMeetings(calendar, 10, 10);
            /*--------------------------------------------------------------------------------------------*/
            Console.WriteLine("Хотим добавить Room но новій режим не даст");
            Console.WriteLine("Press any key");
            Console.ReadLine();
            /*---------------------------------------------------------------------------------------------*/
            AddRooms(calendar, 10);


            /* ----------------------------------------------------------------------
             * JSON
             Записываем в файл игнорирую циклические ссілки друг на друга
              ReferenceHandler = ReferenceHandler.Preserve */
            Console.WriteLine(" Записываем в файл игнорирую циклические ссілки друг на друга");
            Console.WriteLine("Press any key");

           Console.ReadLine();
            string path = ".\\calendar.json";
            WriteJson<Calendar>(calendar, path);


            Console.WriteLine("Считываем из файла");
            Console.WriteLine("Файл Json десериализовали ?");

            Calendar temp = default;
            Console.WriteLine(ReadJson<Calendar>(ref temp,path));
            Console.WriteLine("Десериализованный Календарь");
            Console.WriteLine("Press any key");

            Console.ReadLine();
            Console.WriteLine(temp);
            /*--------------------------------------------------------------------*/
            /*XML*/
           /* Записываем в файл игнорирую циклические ссілки друг на друга*/
           path = ".\\calendar.txt";

           
          Console.WriteLine("XML   Записываем в файл игнорирую циклические ссілки друг на друга");
            Console.Write("XML Сериализовали?");
            Console.WriteLine(WriteXML<Calendar>(calendar, path));

            Console.WriteLine("десеризация XML");
            Console.WriteLine("Press any key");

            Console.ReadLine();
            Calendar temp1 = default;
            Console.Write("XML ДеСериализовали?");
            Console.WriteLine(ReadXML<Calendar>(ref temp1,path));
            Console.WriteLine("Press any key");

            Console.ReadLine();
            Console.WriteLine(temp1);



            /*     ----------------------------------------------------------------------*/
            /*     теже процедурі с бинарнім*/


            string str2 = ".\\calendar.dat";
            WriteSer(calendar, str2);
            Console.Write("Ok? ");
            Calendar temp2 = default;
            Console.WriteLine(ReadSer(ref temp2,str2)  );
            Console.WriteLine("Press any key");
            Console.ReadLine();
            Console.WriteLine(temp2);


            }
        static bool ReadSer<T>(ref T t, string path)
            {
            FileStream fs = null;
            try
                {
                fs = new FileStream(path, FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                t = (T)formatter.Deserialize(fs);
                }
            catch(Exception)
                {

                return false;
                }

            finally { fs?.Close(); }
            return File.Exists(path);
            }
        static bool WriteSer<T>(T t,string path) {
            FileStream fs = null;
            try
                {
                fs = new FileStream(path, FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, t);
                }
            catch(Exception)
                {

                return false;
                }

            finally { fs?.Close(); }
            return File.Exists(path);

            }


        static bool WriteJson<T>(T t, string path) {
            FileStream fs=null;
            
        try
                {
                fs = new FileStream(path, FileMode.OpenOrCreate);
                /*добавил опции в Json serialyzer- сериалтзует поля и 
                когда обьекті ссілаются друг на друга-не будет зацикливания
                ReferenceHandler = ReferenceHandler.Preserve*/
                JsonSerializerOptions options = new()
                    {
                    IncludeFields = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                    };
                JsonSerializer.Serialize(fs, t,options);
                }
            catch(Exception)
                {

                return false;
                }

            finally { fs?.Close(); }
            return File.Exists(path);
        
        } 

        static bool ReadJson<T>(ref T t,string path) {
            FileStream fs = null;
            try
                {
                fs = new FileStream(path, FileMode.OpenOrCreate);
                t=JsonSerializer.Deserialize<T>(fs);
                }
            catch(Exception)
                {
               
                return false;
                }
            finally { fs?.Close(); }
            return true;
            }

        static bool WriteXML<T>(T t, string path) {
            FileStream fs = null;
            try
                {
                fs = new FileStream(path, FileMode.OpenOrCreate);
                XmlSerializer xmlSerializer = new XmlSerializer(t.GetType());
                xmlSerializer.Serialize(fs, t);
                }
            catch(Exception)
                {

                return false;
                }

            finally { fs?.Close(); }
            return File.Exists(path);
            }


        static bool ReadXML<T>(ref T t, string path)
            {
            FileStream fs = null;
            try
                {
                fs = new FileStream(path, FileMode.OpenOrCreate);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                t=(T)xmlSerializer.Deserialize(fs);
                }
            catch(Exception)
                {

                return false;
                }

            finally { fs?.Close(); }
            return File.Exists(path);
            }


        static void AddRooms(Calendar calendar,int count) {
            new Faker<Room>()
                .RuleFor(q => q.NameRoom, w => w.Name.JobArea())
                .RuleFor(e => e.MyCalendar, r => calendar).
                Generate(count).
                ForEach(q => calendar.AddRoom(q));
            }



        static void AddMeetings(Calendar calendar, int countMeet, int countTime, int countPeople = 10)
            {
            for(int i = 0; i < countMeet; i++)
                {
                DateTime dt = DateTime.Now.AddDays(Faker.RandomNumber.Next(countTime));
                DateTime dt2 = dt.AddHours(1);
           

                Meeting mt = new Meeting(
                 Faker.Name.FullName(),
                new Faker<People>().RuleFor(q => q.Name, w => w.Name.FullName()).Generate(countPeople),
                new Interval(dt, dt2)
                 );
                if(!calendar.AddMeeting(mt))
                /* те встречи,когда все комнаті заняті-не добавляет*/
                    { Console.WriteLine($"встречу с {dt} по {dt2} не добавить"); }
                }
            }
        }
    }