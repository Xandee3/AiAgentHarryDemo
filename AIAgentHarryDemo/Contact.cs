namespace AIAgentHarryDemo
{
    /// <summary>
    /// Represents a contact entered by the user.
    /// </summary>
    public sealed class Contact
    {
        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        public string Salutation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
    }
}
