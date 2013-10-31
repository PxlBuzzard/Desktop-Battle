using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DesktopBattle
{
    class Clippy : Sprite 
    {
        #region Class Constants
        const string HERO_ASSETNAME = "pictures/Clippy";
        //the following are movement speed constants
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const int CLIPPY_SPEED = 150;
        #endregion

        #region Class Variables
        Random rnd = new Random();

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        int startPositionX; //spawn position X
        int startPositionY; //spawn position Y
        #endregion

        /// <summary>
        /// Runs once to generate a new Clippy.
        /// </summary>
        public void LoadContent(ContentManager theContentManager) 
        {
            GenerateStartingLocation();
            Position = new Vector2(startPositionX, startPositionY);
            mCurrentState = State.Moving;
            HP = 20;
            base.LoadContent(theContentManager, HERO_ASSETNAME);
        }

        /// <summary>
        /// Makes a "randomized" starting location for Clippy.
        /// </summary>
        private void GenerateStartingLocation() 
        {
            startPositionX = 900 + rnd.Next(maxX);
            startPositionY = 300 + rnd.Next(maxY);
        }

        /// <summary>
        /// Runs on every frame to handle movement of Clippy.
        /// </summary>
        public void Update(GameTime theGameTime) 
        {
            int moveCheck = rnd.Next(100);
            if (moveCheck > 95) {
                UpdateMovement();
            }
            base.Update(theGameTime, mSpeed, mDirection);
        }

        /// <summary>
        /// Randomly moves Clippy around.
        /// </summary>
        private void UpdateMovement() 
        {
            if (mCurrentState == State.Moving && isAlive) 
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;
                int randomMovement = rnd.Next(4);

                if (randomMovement == 0) 
                {
                    mSpeed.X = CLIPPY_SPEED;
                    mDirection.X = MOVE_LEFT;
                }
                else if (randomMovement == 1) 
                {
                    mSpeed.X = CLIPPY_SPEED;
                    mDirection.X = MOVE_RIGHT;
                }
                else if (randomMovement == 2) 
                {
                    mSpeed.Y = CLIPPY_SPEED;
                    mDirection.Y = MOVE_UP;
                }
                else if (randomMovement == 3) 
                {
                    mSpeed.Y = CLIPPY_SPEED;
                    mDirection.Y = MOVE_DOWN;
                }
            }
        }
    }
}
