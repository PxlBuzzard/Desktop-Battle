#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Clippy is a basic enemy with random movement and low hp.
    /// </summary>
    class Clippy : Sprite 
    {
        #region Class Variables
        private string enemyAssetName = "pictures/Clippy"; //name of the hero texture
        private int enemySpeed = 150; //the base speed of the hero
        private int moveUp = -1; //the speed at which they move up
        private int moveDown = 1; //the speed at which they move down
        private int moveLeft = -1; //the speed at which they move left
        private int moveRight = 1; //the speed at which they move right
        Random rnd = new Random();
        int startPositionX; //spawn position X, assigned at creation
        int startPositionY; //spawn position Y, assigned at creation
        #endregion

        /// <summary>
        /// Runs once to generate a new Clippy.
        /// </summary>
        public override void LoadContent(ContentManager theContentManager, GraphicsDeviceManager graphics) 
        {
            GenerateStartingLocation();
            Position = new Vector2(startPositionX, startPositionY);
            sCurrentState = State.Moving;
            HP = 20;
            spriteAngle = 0.0f;
            newlyCreated = false;

            base.LoadContent(theContentManager, enemyAssetName, graphics);
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
        /// Runs on every frame to handle movement of Clippy. Only a 5% chance
        /// of movement change on each frame draw.
        /// </summary>
        public override void Update(GameTime theGameTime) 
        {
            int moveCheck = rnd.Next(100);
            if (moveCheck > 95) 
            {
                UpdateMovement();
            }
            base.Update(theGameTime, spriteSpeed, spriteDirection);
        }

        /// <summary>
        /// Randomly moves Clippy in 1 of 4 directions.
        /// </summary>
        private void UpdateMovement() 
        {
            if (sCurrentState == State.Moving && isAlive) 
            {
                spriteSpeed = Vector2.Zero;
                spriteDirection = Vector2.Zero;
                int randomMovement = rnd.Next(4);

                if (randomMovement == 0) 
                {
                    spriteSpeed.X = enemySpeed;
                    spriteDirection.X = moveLeft;
                }
                else if (randomMovement == 1) 
                {
                    spriteSpeed.X = enemySpeed;
                    spriteDirection.X = moveRight;
                }
                else if (randomMovement == 2) 
                {
                    spriteSpeed.Y = enemySpeed;
                    spriteDirection.Y = moveUp;
                }
                else if (randomMovement == 3) 
                {
                    spriteSpeed.Y = enemySpeed;
                    spriteDirection.Y = moveDown;
                }
            }
        }
    }
}
