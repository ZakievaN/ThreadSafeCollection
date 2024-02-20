
namespace Example
{
    public record class RegionCollectionItem
    {
        /// <summary>
        /// Код региона
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Название региона
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Детали региона
        /// </summary>
        public RegionDetail Value { get; set; }

        public RegionCollectionItem(string code, string name, RegionDetail detail)
        {
            Code = code;
            Name = name;
            Value = detail;
        }
    }
}
