using System.Collections.Generic;

namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    class ShoppingCartDatabase
    {
        internal ShoppingCart GetShoppingCart(UserEnum userId)
        {
            return userId switch
            {
                UserEnum.Quantrell => new ShoppingCart()
                {
                    UserId = UserEnum.Quantrell,
                    Items = new List<CartItem>()
                            {
                                new CartItem()
                                {
                                    ItemName = "Cherry MX Red Switches",
                                    Quantity = 104
                                }
                            }
                },
                UserEnum.Greta => new ShoppingCart()
                {
                    UserId = UserEnum.Greta,
                    Items = new List<CartItem>()
                            {
                                new CartItem()
                                {
                                    ItemName = "Paintballs",
                                    Quantity = 400
                                }
                            }
                },
                _ => throw new KeyNotFoundException("Could not find user in database"),
            };
        }
    }
}
