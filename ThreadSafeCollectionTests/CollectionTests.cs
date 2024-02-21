
namespace ThreadSafeCollection.Tests
{
    [TestClass()]
    public class CollectionTests
    {
        [TestMethod()]
        public void Add_CollectionItem_ReturnElementAddingSign()
        {
            var list = new Collection<string, string, (string, int)>();
            var isAdded = list.Add("01", "01", ("01", 1));
            Assert.AreEqual(list.Count, 1);
            Assert.IsTrue(isAdded);
        }

        [TestMethod()]
        public void Add_ExistCollectionItem_ReturnElementAddingSign()
        {
            var list = new Collection<string, string, (string, int)>();
            var isAdded = list.Add("01", "01", ("01", 1));
            Assert.AreEqual(list.Count, 1);
            Assert.IsTrue(isAdded);

            isAdded = list.Add("01", "01", ("01", 1));
            Assert.AreEqual(list.Count, 1);
            Assert.IsFalse(isAdded);
        }

        [TestMethod()]
        public void Remove_CollectionItemByKey_ReturnCountDeletedItems()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            var count = list.Remove("01", "01");
            Assert.AreEqual(count, 1);

            list.Add("01", "01", ("01", 1));
            count = list.Remove("00", "00");
            Assert.AreEqual(count, 0);
        }

        [TestMethod()]
        public void Remove_CollectionItemByKeyId_ReturnCountDeletedItems()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("01", "02", ("01", 1));
            var count = list.RemoveById("01");
            Assert.AreEqual(count, 2);

            list.Add("01", "01", ("01", 1));
            list.Add("01", "02", ("01", 1));
            count = list.RemoveById("01");
            Assert.AreEqual(count, 2);

            list.Add("01", "02", ("01", 1));
            count = list.RemoveById("00");
            Assert.AreEqual(count, 0);
        }

        [TestMethod()]
        public void Remove_CollectionItemByKeyName_ReturnCountDeletedItems()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("02", "01", ("01", 1));
            var count = list.RemoveByName("01");
            Assert.AreEqual(count, 2);

            list.Add("01", "01", ("01", 1));
            list.Add("01", "02", ("01", 1));
            count = list.RemoveByName("01");
            Assert.AreEqual(count, 1);

            list.Add("01", "02", ("01", 1));
            count = list.RemoveByName("00");
            Assert.AreEqual(count, 0);
        }

        [TestMethod()]
        public void Get_CollectionItemByKeyId_ReturnCollectionValues()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("01", "02", ("01", 1));
            var values = list.GetByKeyId("01");
            Assert.IsTrue(values.All(x => x.Item1 == "01" && x.Item2 == 1));
        }

        [TestMethod()]
        public void Get_CollectionItemByKeyName_ReturnCollectionValues()
        {
            var list = new Collection<string, string, (string, int)>();
            list.Add("01", "01", ("01", 1));
            list.Add("02", "01", ("01", 1));
            var values = list.GetByKeyName("01");
            Assert.IsTrue(values.All(x => x.Item1 == "01" && x.Item2 == 1));

            values = list.GetByKeyName("02");
            Assert.IsTrue(values.GetEnumerator().Current == default);
        }

    }
}