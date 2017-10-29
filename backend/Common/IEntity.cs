using System;

namespace Common
{
    public interface IEntity<out TId>
    {
        TId Id { get; }
    }
}