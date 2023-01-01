namespace BHS.Domain.SeedWork;

public interface IEntity
{
    /*object[] GetKeys();*/
}

public interface IEntity<TKey> : IEntity
{
}