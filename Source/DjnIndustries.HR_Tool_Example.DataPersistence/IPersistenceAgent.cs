namespace DjnIndustries.HR_Tool_Example.DataPersistence
{
    public interface IPersistenceAgent
    {
        /// <summary>
        /// Loads data of a given type from a file
        /// </summary>
        /// <typeparam name="T">Type of data that should be loaded</typeparam>
        /// <param name="path">File path</param>
        /// <returns>Data from the data storage</returns>
        T Load<T>();

        /// <summary>
        /// Updates a file with a given collection of items
        /// </summary>
        /// <typeparam name="T">Type of the item that should be updated</typeparam>
        /// <param name="item">Item that should be updated</param>
        void Update<T>(T item);
    }
}