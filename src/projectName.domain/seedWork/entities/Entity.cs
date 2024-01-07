using projectName.domain.seedWork.entities.interfaces;

namespace projectName.domain.seedWork.entities
{
    public abstract class Entity<TId> : IEntity<TId>
        where TId : struct
    {
        public TId Id { get; init; }
        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();
        public override string ToString() => GetType().Name + $"[Id = {Id}]";

        public override bool Equals(object entity)
            => Id.Equals(((Entity<TId>)entity).Id) && ReferenceEquals(this, entity);
    }
}
