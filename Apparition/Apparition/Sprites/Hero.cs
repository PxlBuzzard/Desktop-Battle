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
    class Hero : Sprite 
    {
        #region Class Constants
        const string HERO_ASSETNAME = "pictures/maincharacter";
        const int START_POSITION_X = 20;
        const int START_POSITION_Y = 250;
        const int HERO_SPEED = 160;
        const int MOVE_UP = -3;
        const int MOVE_DOWN = 3;
        const int MOVE_LEFT = -3;
        const int MOVE_RIGHT = 3;
        #endregion

        #region Class Variables
        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        KeyboardState oldState;
        MouseState oldMouse;
        Weapon[] weapons = new Weapon[2];
        public int currentWeapon;
        #endregion

        /// <summary>
        /// Runs once to create the main hero.
        /// </summary>
        public void LoadContent(ContentManager theContentManager) 
        {
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            HP = 100;
            mCurrentState = State.Moving;

            //this part will need to handle file I/O in the future
            weapons[0] = new Pistol();
            currentWeapon = 0;

            base.LoadContent(theContentManager, HERO_ASSETNAME);
        }

        /// <summary>
        /// Called every time a new frame is generated, handles character
        /// movement and mouse clicks for the hero.
        /// </summary>
        public void Update(GameTime theGameTime) 
        {
            //handle keyboard input
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            UpdateMovement(aCurrentKeyboardState);
            oldState = aCurrentKeyboardState;

            //handle mouse input
            MouseState aCurrentMouseState = Mouse.GetState();
            if (aCurrentMouseState.LeftButton == ButtonState.Pressed 
                && oldMouse.LeftButton == ButtonState.Released) 
            {
                //weapons[currentWeapon].Shoot(aCurrentMouseState);
            }
            oldMouse = aCurrentMouseState;

            base.Update(theGameTime, mSpeed, mDirection); //must be final line
        }

        /// <summary>
        /// Handles all movement for the hero.
        /// </summary>
        private void UpdateMovement(KeyboardState aCurrentKeyboardState) 
        {
            if (mCurrentState == State.Moving) 
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) || aCurrentKeyboardState.IsKeyDown(Keys.A)) 
                {
                    mSpeed.X = HERO_SPEED;
                    mDirection.X = MOVE_LEFT;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) || aCurrentKeyboardState.IsKeyDown(Keys.D)) 
                {
                    mSpeed.X = HERO_SPEED;
                    mDirection.X = MOVE_RIGHT;
                }

                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) || aCurrentKeyboardState.IsKeyDown(Keys.W)) 
                {
                    mSpeed.Y = HERO_SPEED;
                    mDirection.Y = MOVE_UP;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) || aCurrentKeyboardState.IsKeyDown(Keys.S)) 
                {
                    mSpeed.Y = HERO_SPEED;
                    mDirection.Y = MOVE_DOWN;
                }
            }
        }
    }
}
