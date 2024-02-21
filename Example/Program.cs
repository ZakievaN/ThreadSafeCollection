using ThreadSafeCollection;
using System.Diagnostics;
namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var list = new Collection<string, string, RegionDetail>();

            var startTime = Stopwatch.StartNew();
            ParallelLoadData();
            startTime.Stop();
            Console.WriteLine($"Скорость загрузки {startTime.Elapsed}");

            startTime = Stopwatch.StartNew();
            var values = list.GetByKeyName("Московская область");
            startTime.Stop();
            Console.WriteLine($"Скорость получения списка по имени = {startTime.Elapsed}, количество = {values.Count()}");

            startTime = Stopwatch.StartNew();
            var countRemoved = list.RemoveById("01");
            startTime.Stop();
            Console.WriteLine($"Скорость удаления из списка по id = {startTime.Elapsed}, количество = {countRemoved}");

            startTime = Stopwatch.StartNew();
            countRemoved = list.RemoveByName("Московская область");
            startTime.Stop();
            Console.WriteLine($"Скорость удаления из списка по имени = {startTime.Elapsed}, количество = {countRemoved}");

            startTime = Stopwatch.StartNew();
            list.Remove("06", "Московская область");
            startTime.Stop();
            Console.WriteLine($"Скорость удаления из списка по составному ключу = {startTime.Elapsed}");

            void Print()
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"{item}");
                }

                Console.WriteLine();
            }

            void ParallelLoadData()
            {
                var tasks = new Task[100];
                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = Task.Run(() =>
                    {
                        LoadData(i);
                    });
                }
                Task.WaitAll(tasks);
            }

            void LoadData(int i)
            {
                for (int j = 0; j < 10000; j++)
                {
                    list.Add($"0{i*j}", "Республика Адыгея", new RegionDetail("Майкоп", 497985));
                    list.Add($"0{i*j}", "Республика Карачаево-Черкессия", new RegionDetail("Черкесск", 468444));
                    list.Add($"0{i*j}", "Республика Татарстан", new RegionDetail("Казань", 4001625));
                    list.Add($"0{i*j}", "Приморский край", new RegionDetail("Владивосток", 1820076));
                    list.Add($"0{i*j}", "Ивановская область", new RegionDetail("Иваново", 914725));
                    list.Add($"0{i*j}", "Московская область", new RegionDetail("Москва", 8591736));
                }
            }
        }
    }
}