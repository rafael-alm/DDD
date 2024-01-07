using projectName.domain.seedWork.entities.interfaces;

namespace projectName.domain.seedWork.entities
{
    public abstract class EntityWithGuid : Entity<Guid>, IEntityWithGuid
    {
        public override bool Equals(object entity)
            => Id == ((EntityWithGuid)entity).Id && ReferenceEquals(this, entity);

        public override int GetHashCode()
            => base.GetHashCode();
        public override string ToString()
            => GetType().Name + $"[Id = {Id.ToString("N")}]";
    }
}