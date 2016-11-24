namespace VaraniumSharp.Monolith.Enumerations
{
    public enum HangfireStorageEngine
    {
        /// <summary>
        /// Store Hangfire configuration in a memory store
        /// <see>
        ///     <cref>https://www.nuget.org/packages/Hangfire.MemoryStorage/</cref>
        /// </see>
        /// </summary>
        MemoryStorage,

        /// <summary>
        /// Store Hangfire configuration in SQL Server
        /// <see>
        ///     <cref>https://www.nuget.org/packages/HangFire.SqlServer/</cref>
        /// </see>
        /// </summary>
        SqlServer
    }
}