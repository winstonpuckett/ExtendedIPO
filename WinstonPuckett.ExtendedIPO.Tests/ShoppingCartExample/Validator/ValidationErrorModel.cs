namespace WinstonPuckett.ExtendedIPO.Tests.ShoppingCartExample
{
    public class ValidationErrorModel
    {
        /// <summary>
        /// The name of the field which has an error.
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// The messages associated with the error
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The severity of the error.
        /// </summary>
        public ErrorSeverityEnum Severity { get; set; }
        /// <summary>
        /// The row number associated with the message.
        /// Null if not applicable.
        /// </summary>
        public int? RowNumber { get; set; }
    }
}
