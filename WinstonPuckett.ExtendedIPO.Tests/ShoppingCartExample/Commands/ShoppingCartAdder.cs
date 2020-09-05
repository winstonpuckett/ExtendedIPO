using WinstonPuckett.ExtendedIPO;
using WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample;
using System;

namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    class ShoppingCartAdder : ICommander<AddToCartQueryModel>
    {
        public void Execute(AddToCartQueryModel data)
        {
            Console.WriteLine($"{data.NewItem.Item.ItemName} added to {data.Cart.UserId}'s Card");
        }
    }
}
