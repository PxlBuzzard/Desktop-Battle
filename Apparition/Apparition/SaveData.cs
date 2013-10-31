#region Using Statements
using System;
using System.IO;
using System.Xml.Serialization;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Acts as the bridge between the saved XML data and the 
    /// game variables that are going to be saved. This struct
    /// saves the Hero.
    /// </summary>
    [Serializable()]
    public struct SaveHero
    {
        public Hero theHero;
    }

    /// <summary>
    /// This struct saves the rooms.
    /// </summary>
    [Serializable()]
    public struct SaveRoom
    {
        public int currentRoomNumber;
        public List<string> roomTextureNames;
        public List<Room> theRooms;
    }
}
