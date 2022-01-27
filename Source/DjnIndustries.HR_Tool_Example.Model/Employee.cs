namespace DjnIndustries.HR_Tool_Example.Model
{
    /// <summary>
    /// <para>
    ///     An employee is a person, but with some extra needed information.
    ///     Therefore this class inherits the class Person and
    ///     implements the interface IEmployee.
    /// </para>
    /// </summary>
    public class Employee : Person, IEmployee
    {
        #region Properties
        public DateTime DateOfHire { get; set; }

        public int Salary { get; set; }
        #endregion

        #region Ctor
        /// <summary>
        ///     This constructor is used to create a new object.
        /// </summary>
        /// <param name="firstname">Firstname of a person</param>
        /// <param name="lastname">Lastname of a person</param>
        /// <param name="dateOfHire">Date of hire</param>
        /// <param name="salary">Employees salary</param>
        public Employee(string firstname, string lastname, DateTime dateOfBirth, DateTime dateOfHire, int salary)
            : base(firstname, lastname, dateOfBirth)    // Calling base with parameters executes the
                                                        // constructor of the class Person and passes
                                                        // values into it, so that those values will
                                                        // be loaded into the properties of Person,
                                                        // which can be accessed within this class
        {
            DateOfHire = dateOfHire;
            Salary = salary;
        }
        #endregion
    }
}