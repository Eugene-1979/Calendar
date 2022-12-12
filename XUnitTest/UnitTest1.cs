using Calendar;
using Moq;
using System.Runtime.Intrinsics;

namespace XUnitTest
    {
    public class UnitTest1
        {

      static  DateTime dt = DateTime.Now;
       static DateTime dt1 = DateTime.Now.AddHours(1);
       static DateTime dt2 = DateTime.Now.AddHours(2);
       static DateTime dt3 = DateTime.Now.AddHours(3);

          Interval i1 = new Interval(dt, dt1);
        Interval i2 = new Interval(dt2, dt3);

        Interval i02 = new Interval(dt, dt2);
        Interval i13 = new Interval(dt1, dt3);




        [Fact]
        public void TestCompareIntaaerval1()
            {
            Assert.True(i1.CompareTo(i2) < 0,
            "No Ok");
            }

        [Fact]
        public void TestCompareIntaaerval2()
            {
            Assert.True(i2.CompareTo(i1) > 0);
            }

        [Fact]
        public void TestCompareIntaaerval3()
            {
            Assert.True(i02.CompareTo(i13) == 0);
            }

        [Fact]
        public void TestEqualsPeople()
            {
            People people1 = Mock.Of<People>(m => m.Name == "Bob");
            People people2 = Mock.Of<People>(m => m.Name == "Bob");

            Assert.True(people1.Equals(people2));
            }

        [Fact]
        public void TestComparePeople()
            {
            People people1 = Mock.Of<People>(m => m.Age==50);
            People people2 = Mock.Of<People>(m => m.Age==60);

            Assert.True(people1.CompareTo(people2)<0);
            }


        public static Interval CreateInterval(int x1, int x2) => new Interval(DateTime.Now.AddHours(x1), DateTime.Now.AddHours(x2));
       /* ______________________________________________________________*/
      /* Проверка на несколько значений*/
        [Theory]
        [InlineData(-1,4,6,7)]
        [InlineData(-1,3,8,100)]
        [InlineData(100,101,200,201)]
        public void Compare1 (int x1,int x2,int y1,int y2 ){
            Assert.True(CreateInterval(x1, x2).CompareTo(CreateInterval(y1, y2)) < 0);
        }
        [Theory]
        [InlineData(6, 7,-1, 4 )]
        [InlineData(8, 100,-1, 3 )]
        [InlineData(200, 201,100, 101 )]
        public void Compare2(int x1, int x2, int y1, int y2)
            {
            Assert.True(CreateInterval(x1, x2).CompareTo(CreateInterval(y1, y2)) > 0);
            }


        [Theory]
        [InlineData(-1, 8,6, 17 )]
        [InlineData(-1, 9,8, 100 )]
        [InlineData(150, 151,100, 201 )]
        [InlineData(150, 251,100, 201 )]
        public void Compare3(int x1, int x2, int y1, int y2)
            {
            Assert.True(CreateInterval(x1, x2).CompareTo(CreateInterval(y1, y2)) == 0);
            
            }

        [Theory]
        [InlineData(-1, 8, 6, 17)]
        [InlineData(-1, 9, 8, 100)]
        [InlineData(150, 151, 100, 201)]
        [InlineData(150, 251, 100, 201)]
        public void EqualsMeeting(int x1, int x2, int y1, int y2)
            {
            Interval i1 = XUnitTest.UnitTest1.CreateInterval(x1, x2);
            Interval i2 = XUnitTest.UnitTest1.CreateInterval(y1, y2);
            Meeting meeting1 = Mock.Of<Meeting>(m => m.MyInterval == i1);
            Meeting meeting2 = Mock.Of<Meeting>(m => m.MyInterval == i2);


            Assert.True(meeting1.Equals(meeting2),
                "No Ok");



            Assert.True(CreateInterval(x1, x2).CompareTo(CreateInterval(y1, y2)) == 0);

            }


        /* Ожидание вібрасівания ожибки.Ошибка вібрасівается.Тест срабатівает*/

        [Theory]
        [InlineData(10, 9)]
        [InlineData(5, 2)]
        public void AssertThrow(int x1, int x2) {
            Assert.Throws<ArgumentException>(()=> { CreateInterval(x1, x2); });
        }


        [Theory]
        [InlineData(10)]
        [InlineData(500)]
        public void AssertThrowPeple(int x)
            {
            Assert.Throws<ArgumentException>(() => new People(x,"Bob" ));
            }

        /*-----------------------------------------------------------------*/
        /*Использовал Mock из пакета Moq для */
        /* В классе Meeting -Compare по Interval. Реализуем Meeting при помощи Moq
         только с полем Interval,т.к только это поле необходимо лля теста*/

        /*CompareTo==0 - означает ,что интервалы пересекаются*/

        [Theory]
        [InlineData(-1, 8, 6, 17)]
        [InlineData(-1, 9, 8, 100)]
        [InlineData(150, 151, 100, 201)]
        [InlineData(150, 251, 100, 211)]
        public void TestMeetingMock(int x1, int x2, int y1, int y2)
            {
            /*    Assert.True(true);*/
           
            var mock1 = Mock.Of<Meeting>(m => m.MyInterval == CreateInterval(x1, x2));
            var mock2 = Mock.Of<Meeting>(m => m.MyInterval == CreateInterval(y1, y2));

            Assert.True(mock1.CompareTo(mock2) == 0);

            }



        }
    }