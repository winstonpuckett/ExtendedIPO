using System.Collections.Generic;

namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    class ShoppingCart
    {
        public UserEnum UserId { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
