namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    class AddToCartQueryer : IQueryer<AddToCartInputModel, AddToCartQueryModel>
    {
        private readonly ShoppingCartDatabase _database = new ShoppingCartDatabase();

        public AddToCartQueryModel Query(AddToCartInputModel input)
        {
            var shoppingCart = _database.GetShoppingCart(input.User);

            return new AddToCartQueryModel()
            {
                Cart = shoppingCart,
                NewItem = input
            };
        }
    }
}
