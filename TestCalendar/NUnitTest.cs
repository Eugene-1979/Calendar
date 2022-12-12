using Calendar;
using Moq;

namespace TestCalendar
    {
    [TestFixture]
    public class Tests
        {




        DateTime dt;
        DateTime dt1;
        DateTime dt2;
        DateTime dt3;

        Interval i1;
        Interval i2;

        Interval i02;
        Interval i13;
        Mock<Room> mock1 = new Mock<Room>();


        /* начальная инициализация*/

        [SetUp]

        public void Setup()
            {

            dt = DateTime.Now;
            dt1 = DateTime.Now.AddHours(1);
            dt2 = DateTime.Now.AddHours(2);
            dt3 = DateTime.Now.AddHours(3);

            i1 = new Interval(dt, dt1);
            i2 = new Interval(dt2, dt3);

            i02 = new Interval(dt, dt2);
            i13 = new Interval(dt1, dt3);



            }

        [Test]
        public void TestEqualsRoom()
            {
        Room room1 = Mock.Of<Room>(m => m.NameRoom == "room");
        Room room2 = Mock.Of<Room>(m => m.NameRoom == "room");

            Assert.IsTrue(room1.Equals(room2),
                "No Ok");
          
            }

        [Test]
        public void TestEqualsMeeting()
            {
            Interval i1 = XUnitTest.UnitTest1.CreateInterval(1, 5);
            Interval i2 = XUnitTest.UnitTest1.CreateInterval(4, 10);
            Meeting meeting1 = Mock.Of<Meeting>(m => m.MyInterval==i1);
            Meeting meeting2 = Mock.Of<Meeting>(m => m.MyInterval==i2);
       

            Assert.IsTrue(meeting1.Equals(meeting2),
                "No Ok");

            }
        [Test]
        public void TestEqualsInterval()
            {
            Interval i1 = XUnitTest.UnitTest1.CreateInterval(1, 5);
            Interval i2 = XUnitTest.UnitTest1.CreateInterval(4, 10);
     

            Assert.IsTrue(i1.Equals(i2),
                "No Ok");

            }


        [Test]
        public void TestCompareIntaaerval1()
            {
            Assert.IsTrue(i1.CompareTo(i2) < 0,
            "No Ok");
            }

        [Test]
        public void TestCompareIntaaerval2()
            {
            Assert.IsTrue(i2.CompareTo(i1) > 0);
            }

        [Test]
        public void TestCompareIntaaerval3()
            {
            Assert.IsTrue(i02.CompareTo(i13) == 0);
            }


        [Test]
        public void TestEqualsPeople()
            {
            People people1 = Mock.Of<People>(m => m.Name == "Bob");
            People people2 = Mock.Of<People>(m => m.Name == "Bob");

            Assert.IsTrue(people1.Equals(people2));
            }

        [Test]
        public void TestCompareIntaaerval()
            {
            People people1 = Mock.Of<People>(m => m.Age == 50);
            People people2 = Mock.Of<People>(m => m.Age == 60);

            Assert.IsTrue(people1.CompareTo(people2) < 0);
            }


        }
    }