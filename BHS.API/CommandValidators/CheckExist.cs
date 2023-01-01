/*
namespace BHS.API.CommandValidators;

public class CheckExist
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckExist(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public bool Check<T>(int id) where T : class, IAggregateRoot
    {
        return _unitOfWork.Repository<T>().GetAsync($"Id == {id}").Result != null;
    }
    
    public bool Check<T>(string id) where T : class, IAggregateRoot
    {
        return _unitOfWork.Repository<T>().GetAsync($"Id == \"{id}\"").Result != null;
    }
    
    public bool CheckWithExpression<T>(string expression) where T : class, IAggregateRoot
    {
        return _unitOfWork.Repository<T>().GetAsync(expression).Result == null;
    }
    
 
}*/

