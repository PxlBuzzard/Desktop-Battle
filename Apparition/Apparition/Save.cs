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
        private SaveHero saveHeroData;
        private SaveRoom saveRoomData;
        public bool GameSaveRequested = false; //allows other classes to ask for a save
        public bool GameLoadRequested = true; //allows other classes to ask for a load
        private string saveFileHero = "hero.xml"; //the hero file name
        private string saveFileRooms = "rooms.xml"; //the rooms file name
        #endregion

        /// <summary>
        /// Updates the SaveData with the latest information
        /// </summary>
        private void GenerateSaveData()
        {
            saveHeroData = new SaveHero() //prime SaveHero for saving the Hero
            {
                theHero = Game1.cHero,
            };

            saveRoomData = new SaveRoom() //prime SaveRoom
            {
                currentRoomNumber = Game1.cArea.currentRoom,
                roomTextureNames = Game1.cArea.roomTex,
                theRooms = Game1.cArea.Rooms,
            };
        }

        /// <summary>
        /// Saves the game in the same directory as the executable.
        /// </summary>
        private void SaveGame()
        {
            //save hero data
            DeleteExisting(saveFileHero); //deletes old file if it exists
            GenerateSaveData();
            StreamWriter file = new StreamWriter(saveFileHero);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveHero));
            serializer.Serialize(file, saveHeroData);
            file.Close();

            //save area data
            DeleteExisting(saveFileRooms); //deletes old file if it exists
            GenerateSaveData();
            file = new StreamWriter(saveFileRooms);
            serializer = new XmlSerializer(typeof(SaveRoom));
            serializer.Serialize(file, saveRoomData);
            file.Close();
        }

        /// <summary>
        /// Loads the game from the save file.
        /// </summary>
        public void LoadGame()
        {
            try
            {
                TextReader XMLReader = new StreamReader(saveFileHero);
                try
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(SaveHero));
                    saveHeroData = (SaveHero)deserializer.Deserialize(XMLReader);

                    //get the variables reassociated with the runtime data
                    Game1.cHero = saveHeroData.theHero;
                    Game1.cHero.LoadContent(Game1.cHero.heroAssetName);
                }
                catch
                {
                    Game1.cGameUI.Draw("Could not load your hero file, using default values.");
                }
                XMLReader.Close();
            }
            catch
            {
                Game1.cGameUI.Draw("Could not find your hero save file.");
            }

            try
            {
                TextReader XMLReader = new StreamReader(saveFileRooms);
                try
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(SaveRoom));
                    saveRoomData = (SaveRoom)deserializer.Deserialize(XMLReader);

                    //get the variables reassociated with the runtime data
                    Game1.cArea.currentRoom =
                        (int)MathHelper.Clamp(saveRoomData.currentRoomNumber, 0, Game1.cArea.Rooms.Count - 1);

                    try { Game1.cArea.roomTex = saveRoomData.roomTextureNames; }
                    catch { Game1.cGameUI.Draw("You have an invalid string in loaded room texture names."); }

                    try { Game1.cArea.Rooms = saveRoomData.theRooms; }
                    catch { Game1.cGameUI.Draw("You have an invalid room in your loaded room list."); }
                    Game1.cArea.LoadContent();
                }
                catch
                {
                    Game1.cGameUI.Draw("Could not load your saved rooms, using default rooms.");
                }
                XMLReader.Close();
            }
            catch
            {
                Game1.cGameUI.Draw("Could not find your rooms save file.");
            }
        }

        /// <summary>
        /// If a previous save file already exists, delete it
        /// </summary>
        private void DeleteExisting(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        /// <summary>
        /// Runs every frame to check for a new save.
        /// </summary>
        public void Update()
        {
            if (GameSaveRequested)
            {
                Game1.cGameUI.Draw("Saving...");
                SaveGame();
                GameSaveRequested = false;
            }
            else if (GameLoadRequested)
            {
                Game1.cGameUI.Draw("Loading...");
                LoadGame();
                GameLoadRequested = false;
            }
        }
    }
}
