
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("friend_signed_B, PublicKey=0024000004800000940000000602000000240000525341310004000001000100254cf0834bc194d4afc36071bc198626456b5db5f4117dea0dc1be02c0ec3c7c530a427299c25a2e01805d84ec996512a62aba3152d9d692a12cec91303719ca3ffb6224ad784498b34da8cf146a281f09f2f8123d71630f34a92d13094641d4f7ff5a449d943d9a56e8e827a02aa07328335d58deabc213d21a949ceb3955aa")]
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
