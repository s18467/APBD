namespace LinqTutorials.Tests
{
    [TestClass()]
    public class LinqTasksTests
    {
        [TestMethod()]
        public void Task1Test()
        {
            var list = LinqTasks.Task1();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count());
        }

        [TestMethod()]
        public void Task2Test()
        {
            var list = LinqTasks.Task2();
            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count());
            Assert.AreEqual("Paweł Latowski", list.Select(emp => emp.Ename).First());
        }

        [TestMethod()]
        public void Task3Test()
        {
            var r = LinqTasks.Task3();
            Assert.IsNotNull(r);
            Assert.AreEqual(12000, r);
        }

        [TestMethod()]
        public void Task4Test()
        {
            var list = LinqTasks.Task4();
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count());
            Assert.AreEqual(8, list.First().Empno);
        }

        [TestMethod()]
        public void Task5Test()
        {
            var list = LinqTasks.Task5();
            Assert.IsNotNull(list);
            Assert.AreEqual(10, list.Count());
            Assert.IsInstanceOfType(list.First(), typeof(string));
        }

        [TestMethod()]
        public void Task6Test()
        {
            var list = LinqTasks.Task6();
            Assert.IsNotNull(list);
            Assert.AreEqual(9, list.Count());
            dynamic first = list.ToList().First();
            Assert.IsNotNull(first);
        }

        [TestMethod()]
        public void Task7Test()
        {
            var list = LinqTasks.Task7();
            Assert.IsNotNull(list);
            Assert.AreEqual(7, list.Count());
        }

        [TestMethod()]
        public void Task8Test()
        {
            var r = LinqTasks.Task8();
            Assert.IsNotNull(r);
            Assert.AreEqual(true, r);
        }

        [TestMethod()]
        public void Task9Test()
        {
            var emp = LinqTasks.Task9();
            Assert.IsNotNull(emp);
            Assert.AreEqual("Paweł Latowski", emp.Ename);
        }

        [TestMethod()]
        public void Task10Test()
        {
            var list = LinqTasks.Task10();
            Assert.IsNotNull(list);
            Assert.AreEqual(11, list.Count());
        }

        [TestMethod()]
        public void Task11Test()
        {
            var list = LinqTasks.Task11();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count());
        }

        [TestMethod()]
        public void Task12Test()
        {
            var list = LinqTasks.Task12();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count());
        }

        [TestMethod()]
        public void Task13Test()
        {
            var r = LinqTasks.Task13(new[] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 2, 7, 7, 7, 7, 7, 5, 5 });
            Assert.IsNotNull(r);
            Assert.AreEqual(7, r);
        }

        [TestMethod()]
        public void Task14Test()
        {
            var list = LinqTasks.Task14();
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count());
        }
    }
}