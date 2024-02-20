
namespace Example
{
    /// <summary>
    /// Детали региона
    /// </summary>
    public record RegionDetail
    {
        /// <summary>
        /// Региональный центр
        /// </summary>
        public string RegionCenter { get; set; }

        /// <summary>
        /// Численность населения (тыс. чел.)
        /// </summary>
        public int PeopleCount { get; set; }

        public RegionDetail(string name, int count)
        {
            RegionCenter = name;
            PeopleCount = count; 
        }
    }
}
