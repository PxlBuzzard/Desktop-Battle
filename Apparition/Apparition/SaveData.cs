#region Using Statements
using System;
using System.IO;
using System.Xml.Serialization;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Acts as the bridge between the saved XML data and the 
    /// game variables that are going to be saved.
    /// </summary>
    [Serializable()]
    public struct SaveData
    {
        public int currentRoom;
        public Hero theHero;
    }
}
