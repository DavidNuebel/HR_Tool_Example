namespace DjnIndustries.HR_Tool_Example.Model
{
    /// <summary>
    /// <para>
    ///     This class represents a person, which properties and actions were suited for the context of this applications.
    ///     The blueprint for this class is the interface IPerson, so that other types of living can be created.
    ///
    ///     As this class ist abstract, no instance can be created.
    ///     It can only be inherited by other classes which are not abstract.
    ///     The object Person is still usable in code like an interface.
    /// </para>
    /// <para>
    ///     Hover over a property name and the description will be displayed as a tooltip.
    ///     The descirption is defined in <see cref="IPerson"/>
    /// </para>
    /// </summary>
    public abstract class Person : IPerson
    {
        #region Properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        #endregion

        #region Ctor
        /// <summary>
        /// This constructor will be called from the Employee class constructor
        /// </summary>
        /// <param name="firstname">Firstname of a person</param>
        /// <param name="lastname">Lastname of a person</param>
        /// <param name="dateOfBirth">Date of birth</param>
        protected Person(string firstname, string lastname, DateTime dateOfBirth)
        {
            FirstName = firstname;
            LastName = lastname;
            DateOfBirth = dateOfBirth;
        }
        #endregion
    }
}