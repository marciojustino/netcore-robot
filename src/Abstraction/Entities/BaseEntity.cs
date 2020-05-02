namespace Abstraction.Entities
{
    using System;

    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public BaseEntity()
        {
            Id = new Guid();
        }
    }
}