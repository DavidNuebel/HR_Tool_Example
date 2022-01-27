namespace DjnIndustries.HR_Tool_Example.Model
{
    /// <summary>
    /// <para>
    ///     This interface is a blueprint for an employee class.
    ///     An employee is also a person and therefore this interface
    ///     inherits the interface IPerson which provides the blueprint
    ///     of a normal person.
    ///     That means, here were only those properties and methods defined that
    ///     will top up a person to an employee.
    /// </para>
    /// </summary>
    public interface IEmployee : IPerson
    {
        /// <summary>
        /// Salary e.g. "1234"
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Date of hire "01.06.2021"
        /// </summary>
        public DateTime DateOfHire { get; set; }
    }
}