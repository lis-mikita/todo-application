namespace todo_aspnetmvc_ui.Models
{
    /// <summary>
    /// Represent error model.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets request number.
        /// </summary>
        /// <value>
        /// Request number.
        /// </value>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether show request ID if that isn't null or empty.
        /// </summary>
        /// <value>
        /// True - is not empty or null.
        /// </value>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
