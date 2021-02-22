using System;
using System.IO;

namespace Game
{
    /// <summary>
    /// Class for game Constants 
    /// </summary>
    public static class Constants
    {
        // Database save file name 
        public const string DatabaseFilename = "game.db3";

        // Flags for SQ Lite 
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        /// <summary>
        /// Get the Database path as a string 
        /// </summary>
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}