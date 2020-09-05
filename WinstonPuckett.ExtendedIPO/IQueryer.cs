namespace WinstonPuckett.ExtendedIPO
{
    /// <summary>
    /// A Queryer is a class which takes in information local to the calling function and returns data from the surrounding universe.
    /// Usually that universe is an external database of some sort.
    /// </summary>
    /// <typeparam name="T">The type of model we need to query the universe.</typeparam>
    /// <typeparam name="U">The type of model returned from the query.</typeparam>
    public interface IQueryer<in T, out U>
    {
        /// <summary>
        /// Uses input local to the user flow and gets information outside of the scope of the calling method.
        /// For example, "The user has given me their user id, and I need to know their email address."
        /// </summary>
        /// <param name="input">The information needed to grab extra information from the universe.</param>
        /// <returns>Extra information from the universe.</returns>
        U Query(T input);
    }
}
