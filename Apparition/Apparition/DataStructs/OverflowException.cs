using System;

//Created by Schwartz
namespace DesktopBattle {
    /// <summary>
    /// An exception that is generated when an element is added into
    /// an array based collection that is already full.
    /// </summary>
    public class OverflowException : ApplicationException {
        public OverflowException(string msg) : base(msg) { }
    }
}