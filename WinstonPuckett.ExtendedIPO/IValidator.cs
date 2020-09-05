using System.Collections.Generic;
using System.Linq;

namespace WinstonPuckett.ExtendedIPO
{
    /// <summary>
    /// Validates a list or single instance of T and returns a List of type U.
    /// It is recommended that you create a separate interface which takes in T
    /// and returns a model that you'll standardize on throughout your project.
    /// Then you use IValidator<T> which implements IValidator<T, U>
    /// </summary>
    /// <typeparam name="T">The type of model to validate.</typeparam>
    /// <typeparam name="U">The type of model to pass back when an error message is hit.</typeparam>
    public interface IValidator<in T, U>
    {
        List<U> Validate(IEnumerable<T> dataList)
        {
            return dataList.SelectMany(data => Validate(data)).ToList();
        }
        List<U> Validate(T data);
    }
}
