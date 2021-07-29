namespace GameLibrary.API
{
    /// <summary>
    /// Custom Message Model
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public class APIResponse<T>
    {
        /// <summary>
        /// If the operation was successful
        /// </summary>
        public bool Succeed { get; set; }

        /// <summary>
        /// Message of the operation
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Result of the operation
        /// </summary>
        public T Results { get; set; }
    }
}
