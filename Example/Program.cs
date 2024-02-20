using ThreadSafeCollection;

namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var list = new Collection<string, string, RegionDetail>();

            list.Add("01", "Республика Адыгея", new RegionDetail("Майкоп", 497985));
            list.Add("09", "Республика Карачаево-Черкессия", new RegionDetail("Черкесск", 468444));
            list.Add("16", "Республика Татарстан", new RegionDetail("Казань", 4001625));
            list.Add("25", "Приморский край", new RegionDetail("Владивосток", 1820076));
            list.Add("37", "Ивановская область", new RegionDetail("Иваново", 914725));
            list.Add("50", "Московская область", new RegionDetail("Москва", 8591736));
            list.Add("750", "Московская область", new RegionDetail("Москва", 8591736));

            Print();

            list.RemoveById("01");
            list.RemoveByName("Московская область");

            Print();

            void Print()
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"{item}");
                }

                Console.WriteLine();
            }
        }
    }
}