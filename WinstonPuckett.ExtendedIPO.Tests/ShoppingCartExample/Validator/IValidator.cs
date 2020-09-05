namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    public interface IValidator<in T> : IValidator<T, ValidationErrorModel>
    { }
}
