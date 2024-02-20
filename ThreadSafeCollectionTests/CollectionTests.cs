
namespace ThreadSafeCollection.Tests
{
    [TestClass()]
    public class CollectionTests
    {
        [TestMethod()]
        public void Add()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            Assert.AreEqual(list.Count, 1);
        }

        [TestMethod()]
        public void Remove()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            var count = list.Remove("01", "01");
            Assert.AreEqual(count, 1);
        }

        [TestMethod()]
        public void RemoveByName()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("01", "02", ("01", 1));
            var count = list.RemoveById("01");
            Assert.AreEqual(count, 2);
        }

        [TestMethod()]
        public void RemoveById()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("02", "01", ("01", 1));
            var count = list.RemoveByName("01");
            Assert.AreEqual(count, 2);
        }

        [TestMethod()]
        public void GetByKeyId()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("01", "02", ("01", 1));
            var values = list.GetByKeyId("01");
            Assert.IsTrue(values.All(x => x.Item1 == "01" && x.Item2 == 1));
        }

        [TestMethod()]
        public void GetByKeyNameIfExist()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("02", "01", ("01", 1));
            var values = list.GetByKeyName("01");
            Assert.IsTrue(values.All(x => x.Item1 == "01" && x.Item2 == 1));
        }

        [TestMethod()]
        public void GetByKeyNameIfNotExist()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("02", "01", ("01", 1));
            var values = list.GetByKeyName("02");
            Assert.IsTrue(values.GetEnumerator().Current == default);
        }

    }
}