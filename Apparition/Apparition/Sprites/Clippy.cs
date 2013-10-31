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
    public class Clippy : Sprite 
    {
        #region Class Variables
        private string enemyAssetName = "pictures/Clippy"; //name of the hero texture
        private int enemySpeed = 150; //the base speed of the hero
        private int moveUp = -1; //the speed at which they move up
        private int moveDown = 1; //the speed at which they move down
        private int moveLeft = -1; //the speed at which they move left
        private int moveRight = 1; //the speed at which they move right
        #endregion

        /// <summary>
        /// Runs once to generate a new Clippy.
        /// </summary>
        public Clippy() 
        {
            sCurrentState = State.Moving;
            spriteAngle = 0.0f;

            base.LoadContent(enemyAssetName);
        }

        /// <summary>
        /// Runs on every frame to handle movement of Clippy. Only a 5% chance
        /// of movement change on each frame draw.
        /// </summary>
        public override void Update(GameTime theGameTime) 
        {
            if (newlyCreated)
            {
                GenerateStartingLocation();
                HP = 20;
                isAlive = true;
                newlyCreated = false;
            }

            //helps keep the Clippy away from the edges of the screen
            if (Position.X <= 25)
            {
                spriteDirection.X = moveRight;
            }
            else if (Position.X >= maxX - 25)
            {
                spriteDirection.X = moveLeft;
            }
            if (Position.Y <= 25)
            {
                spriteDirection.Y = moveDown;
            }
            else if (Position.Y >= maxY - 25)
            {
                spriteDirection.Y = moveUp;
            }

            //will change Clippy's direction 5% of the time
            int moveCheck = Sprite.rnd.Next(100);
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
                int randomMovement = rnd.Next(8);

                if (randomMovement <= 4) 
                {
                    spriteSpeed.X = enemySpeed;
                    spriteDirection.X = moveLeft;
                }
                else if (randomMovement == 5) 
                {
                    spriteSpeed.X = enemySpeed;
                    spriteDirection.X = moveRight;
                }
                else if (randomMovement == 6) 
                {
                    spriteSpeed.Y = enemySpeed;
                    spriteDirection.Y = moveUp;
                }
                else if (randomMovement == 7) 
                {
                    spriteSpeed.Y = enemySpeed;
                    spriteDirection.Y = moveDown;
                }
            }
        }
    }
}
