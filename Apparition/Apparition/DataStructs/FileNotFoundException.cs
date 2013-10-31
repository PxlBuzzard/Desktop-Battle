using System;

namespace DesktopBattle
{
    /// <summary>
    /// An exception that is generated when a file is not found.
    /// </summary>
    public class FileNotFoundException : ApplicationException
    {
        public FileNotFoundException(string msg) : base(msg) 
        {
        }
    }
}