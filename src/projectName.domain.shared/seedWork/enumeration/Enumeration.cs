using System.Reflection;

namespace projectName.domain.shared.seedWork.enumeration
{
    public class Enumeration : IComparable
    {
        protected Enumeration(int id, string name)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            Id = id;
            Name = name;
        }

        public int Id { get; init; }
        public string Name { get; init; }

        public override string ToString()
        {
            return $"Id:{Id} - Name: {Name}";
        }
        public override bool Equals(object obj)
        {
            ArgumentNullException.ThrowIfNull(obj, nameof(obj));

            var otherEnum = obj as Enumeration;

            var typeMatches = GetType().Equals(obj.GetType());
            var IdMatches = Id.Equals(otherEnum.Id);

            return typeMatches && IdMatches;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static IEnumerable<TEnumeration> GetAll<TEnumeration>() where TEnumeration : Enumeration
        {
            var type = typeof(TEnumeration);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var locatedValue = info.GetValue(null) as TEnumeration;

                if (locatedValue != null)
                    yield return locatedValue;
            }
        }

        public static TEnumeration GetById<TEnumeration>(int id) where TEnumeration : Enumeration
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var matchingItem = parse<TEnumeration, int>(id, "Id", item => item.Id == id);
            return matchingItem;
        }

        public static TEnumeration GetByName<TEnumeration>(string name) where TEnumeration : Enumeration
        {
            var matchingItem = parse<TEnumeration, string>(name, "Name", item => item.Name == name);
            return matchingItem;
        }

        public static TEnumeration Where<TEnumeration>(Func<TEnumeration, bool> predicate) where TEnumeration : Enumeration
        {
            var matchingItem = GetAll<TEnumeration>().FirstOrDefault(predicate);

            return matchingItem;
        }

        private static TEnumeration parse<TEnumeration, TKey>(TKey value, string description, Func<TEnumeration, bool> predicate) where TEnumeration : Enumeration
        {
            var matchingItem = GetAll<TEnumeration>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format($"'{value}' is not a valid {description} in {typeof(TEnumeration)}");
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((Enumeration)other).Id);
        }
    }
}
