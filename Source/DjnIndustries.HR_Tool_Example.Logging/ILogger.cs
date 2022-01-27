namespace DjnIndustries.HR_Tool_Example.Logging
{
    /// <summary>
    /// This class is a blueprint for the logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message);
    }
}