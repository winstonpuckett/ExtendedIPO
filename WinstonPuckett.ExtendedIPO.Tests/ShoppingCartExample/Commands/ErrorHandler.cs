using System.Collections.Generic;
using System.Linq;

namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    class ErrorHandler : ICommander<IEnumerable<ValidationErrorModel>>
    {
        public void Execute(IEnumerable<ValidationErrorModel> errors)
        {
            foreach (var error in errors.Where(e => e.Severity == ErrorSeverityEnum.Error))
                System.Console.WriteLine($"I can't do that because this error happened {error.Message}");

            foreach (var error in errors.Where(e => e.Severity == ErrorSeverityEnum.Warning))
                System.Console.WriteLine($"I In addition, you may want to check on this warning message: {error.Message}");
        }
    }
}
