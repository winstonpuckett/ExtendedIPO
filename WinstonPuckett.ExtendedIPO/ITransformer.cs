namespace WinstonPuckett.ExtendedIPO
{
    /// <summary>
    /// A Transformer is a class which takes in data and transforms it to another type.
    /// This is useful for business logic, which often requires you to say things like, "If 
    /// they are this type of user, we have to set a pending state instead of setting the value
    /// directly."
    /// </summary>
    /// <typeparam name="T">The type of model which will be transformed.</typeparam>
    /// <typeparam name="U">The type which will be returned. It is acceptable for this to be the same as T.</typeparam>
    public interface ITransformer<in T, out U>
    {
        U Transform(T dataToTransform);
    }
}
