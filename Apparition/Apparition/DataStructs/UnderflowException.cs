using System;

//Created by Schwartz
namespace DesktopBattle {
    /// <summary>
    /// An exception that is generated when an element is accessed
    /// or removed from an array based collection that is already empty.
    /// </summary>
    public class UnderflowException : ApplicationException {
        public UnderflowException(string msg) : base(msg) { }
    }
}