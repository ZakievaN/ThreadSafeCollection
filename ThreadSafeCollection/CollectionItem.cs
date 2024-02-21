
namespace ThreadSafeCollection
{
    internal class CollectionItem<TId, TName, TValue> : IComparable<CollectionItem<TId, TName, TValue>>
        where TId : notnull, IComparable
        where TName : notnull, IComparable
    {
        public CollectionItem(TId id, TName name, TValue value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public TId Id { get; init; }

        public TName Name { get; init; }

        public TValue Value { get; set; }

        public int CompareTo(CollectionItem<TId, TName, TValue> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            return Id.CompareTo(other.Id) == 0
                ? Name.CompareTo(other.Name)
                : Id.CompareTo(other.Id);
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Value}";
        }
    }
}
