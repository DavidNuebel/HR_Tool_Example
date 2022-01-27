namespace DjnIndustries.HR_Tool_Example.Model
{
    /// <summary>
    /// <para>
    ///     This interface provides the blueprint for a person, which properties and actions were suited for the context of this applications.
    /// </para>
    /// </summary>
    public interface IPerson
    {
        /// <summary>
        /// Firstname of the person e.g. "Max"
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Lastname of the person e.g. "Mustermann"
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth of the person e.g. "01.08.1998"
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}