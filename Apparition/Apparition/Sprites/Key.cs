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
    /// Key chases the Hero constantly and is easy to kill.
    /// </summary>
    public class Key : Sprite
    {
        #region Class Variables
        private string enemyAssetName = "pictures/key"; //name of the sprite texture
        private int enemySpeed = 300; //the base speed of the enemy
        private int moveUp = -1; //the speed at which they move up
        private int moveDown = 1; //the speed at which they move down
        private int moveLeft = -1; //the speed at which they move left
        private int moveRight = 1; //the speed at which they move right
        private int timeToMove;
        private int lastMoveTime = 0; //keeps track of when Key should switch directions
        #endregion

        /// <summary>
        /// Runs once to generate a new Key.
        /// </summary>
        public Key()
        {
            sCurrentState = State.Moving;
            spriteAngle = 0.0f;

            base.LoadContent(enemyAssetName);
        }

        /// <summary>
        /// Key constantly chases the Hero, changes his direction
        /// every two seconds.
        /// </summary>
        public override void Update(GameTime theGameTime)
        {
            if (newlyCreated)
            {
                GenerateStartingLocation();
                HP = 10;
                newlyCreated = false;
            }
            if (timeToMove >= lastMoveTime)
            {
                if (sCurrentState == State.Moving && isAlive)
                {
                    spriteSpeed = Vector2.Zero;
                    spriteDirection = Vector2.Zero;
                    if (Game1.cHero.Position.X >= Position.X)
                    {
                        spriteSpeed.X = enemySpeed;
                        spriteDirection.X = moveRight;
                    }
                    else
                    {
                        spriteSpeed.X = enemySpeed;
                        spriteDirection.X = moveLeft;
                    }

                    if (Game1.cHero.Position.Y <= Position.Y)
                    {
                        spriteSpeed.Y = enemySpeed;
                        spriteDirection.Y = moveUp;
                    }
                    else
                    {
                        spriteSpeed.Y = enemySpeed;
                        spriteDirection.Y = moveDown;
                    }
                }
                timeToMove = lastMoveTime;
                lastMoveTime += 750;
            }
            else
            {
                timeToMove += theGameTime.ElapsedGameTime.Milliseconds;
            }
            
            base.Update(theGameTime, spriteSpeed, spriteDirection);
        }
    }
}
