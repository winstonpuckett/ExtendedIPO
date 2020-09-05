namespace WinstonPuckett.ExtendedIPO
{
    /// <summary>
    /// A commander is a class which executes a unit of work.
    /// The commander receives the information it needs, and performs the work, expecting that the work will succeed.
    /// 
    /// If the work does not succeed, it will need to throw an exception. A developer writing against an ICommander
    /// should tell the user if they expect any exception to be thrown.
    /// </summary>
    /// <typeparam name="T">The model needed to execute the work.</typeparam>
    public interface ICommander<in T>
    {
        /// <summary>
        /// A unit of work that is expected to run successfully.
        /// </summary>
        /// <param name="data">The data needed to execute the work.</param>
        void Execute(T data);
    }
}
