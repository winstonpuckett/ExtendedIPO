using System.Collections.Generic;
using System.Linq;

namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    class AddToCartValidator : IValidator<AddToCartQueryModel>
    {
        public List<ValidationErrorModel> Validate(AddToCartQueryModel data)
        {
            var errors = new List<ValidationErrorModel>
            {
                QuantityIsRoundNumber(data.NewItem.Item.Quantity),
                NewItemIsNotInShoppingCart(data.NewItem.Item, data.Cart.Items)
            };

            return errors.Where(x => x != null).ToList();
        }

        private ValidationErrorModel QuantityIsRoundNumber(int quantity)
        {
            if (quantity % 1 == 0) return null;

            return new ValidationErrorModel()
            {
                FieldName = "Quantity",
                Message = "Quantity must be a whole number",
                Severity = ErrorSeverityEnum.Error
            };
        }
        private ValidationErrorModel NewItemIsNotInShoppingCart(CartItem newItem, List<CartItem> cartItems)
        {
            if (!cartItems.Contains(newItem)) return null;

            return new ValidationErrorModel()
            {
                FieldName = "Item",
                Message = "New item cannot already be in user's shopping cart.",
                Severity = ErrorSeverityEnum.Error
            };
        }
    }
}
