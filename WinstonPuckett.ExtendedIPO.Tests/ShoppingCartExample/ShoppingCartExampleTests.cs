using System.Linq;
using Xunit;

namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    public class ShoppingCartExampleTests
    {
        // You can use interfaces here. These are just examples.
        private readonly AddToCartQueryer _queryer;
        private readonly AddToCartValidator _validator;
        private readonly ShoppingCartAdder _shoppingCartAdder;
        private readonly ErrorHandler _errorHandler;

        public ShoppingCartExampleTests()
        {
            // You can of course use DI here. These are just examples.
            _queryer = new AddToCartQueryer();
            _validator = new AddToCartValidator();
            _shoppingCartAdder = new ShoppingCartAdder();
            _errorHandler = new ErrorHandler();
        }

        [Fact]
        public void AddSomethingToShoppingCart()
        {
            // stuff that would be passed to this method
            var inputModel = new AddToCartInputModel()
            {
                Item = new CartItem()
                {
                    ItemName = "Sock",
                    Quantity = 2
                },
                User = UserEnum.Greta
            };

            var model = _queryer.Query(inputModel);

            var errors = _validator.Validate(model);
            if (!errors.Any(error => error.Severity == ErrorSeverityEnum.Error))
                _shoppingCartAdder.Execute(model);
            else
                _errorHandler.Execute(errors);
        }
    }
}
