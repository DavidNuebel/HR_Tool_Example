namespace DjnIndustries.HR_Tools_Example.IO
{
    public interface IDataHandler
    {
        /// <summary>
        /// This method makes sure that the file is created
        /// </summary>
        /// <returns><c>true</c> if the file exists, <c>false if not</c></returns>
        void Append(string content);

        /// <summary>
        /// This method will check the file and path and then override a file with content.
        /// </summary>
        /// <param name="content">Content that should be written</param>
        string Read();

        /// <summary>
        /// This method will check the file and path and then append the content to a file.
        /// </summary>
        /// <param name="content">Content that should be written</param>
        bool Validate();

        /// <summary>
        /// Reads out a file and returns the lines
        /// </summary>
        /// <returns>Lines of data</returns>
        void Write(string content);
    }
}