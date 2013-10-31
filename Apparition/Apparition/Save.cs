#region Using Statements
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Handles all the saving and loading for the game.
    /// </summary>
    [Serializable()]
    public class Save
    {
        #region Class Variables
        private SaveData saveGameData;
        public bool GameSaveRequested = false; //allows other classes to ask for a save
        public bool GameLoadRequested = false; //allows other classes to ask for a load
        private string exeDir = System.IO.Path.GetDirectoryName
                    (System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        private string saveFileName = "\\savegame.xml";
        private string fullFilePath
        {
            get //combines file path with file name
            {
                string temp = exeDir + saveFileName;
                return temp.Replace("file:\\", "");
            }
        }
        #endregion

        public Save()
        {
            saveGameData = new SaveData() //prime SaveData for saving game info
            {
                currentRoom = Game1.cArea.currentRoom,
                theHero = Game1.cHero,
            };
        }

        /// <summary>
        /// Saves the game in the same directory as the executable.
        /// </summary>
        private void SaveGame()
        {
            DeleteExisting(); //deletes old file if it exists
            StreamWriter file = new StreamWriter(fullFilePath);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            serializer.Serialize(file, saveGameData);
            file.Close();
        }

        /// <summary>
        /// Loads the game from the save file.
        /// </summary>
        public void LoadGame()
        {
            TextReader XMLReader = new StreamReader(fullFilePath);
            XmlSerializer deserializer = new XmlSerializer(typeof(SaveData));
            saveGameData = (SaveData)deserializer.Deserialize(XMLReader);

            //get the variables reassociated with the runtime data
            Game1.cArea.currentRoom = saveGameData.currentRoom;
            Game1.cHero = saveGameData.theHero;
            Game1.cHero.LoadContent(Game1.cHero.spriteName);
            XMLReader.Close();
        }

        /// <summary>
        /// If a previous save file already exists, delete it
        /// </summary>
        private void DeleteExisting()
        {
            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            }
        }

        /// <summary>
        /// Runs every frame to check for a new save.
        /// </summary>
        public void Update()
        {
            if (GameSaveRequested)
            {
                SaveGame();
                GameSaveRequested = false;
            }
            else if (GameLoadRequested)
            {
                LoadGame();
                GameLoadRequested = false;
            }
        }
    }
}
