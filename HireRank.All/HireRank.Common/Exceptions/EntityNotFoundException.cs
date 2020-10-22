using System;

namespace HireRank.Common.Exceptions
{
    public class EntityNotFoundException : HireRankException
    {
        public EntityNotFoundException(Guid entityId, Type entityType) 
            : base($"{ nameof(entityType) } with Id { entityId } was not found in the system.") { }
    }
}
