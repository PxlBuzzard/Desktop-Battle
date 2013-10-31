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
    /// Creates the main Hero that the player controls.
    /// </summary>
    sealed partial class Hero : Sprite
    {
        #region Class Variables
        private string heroAssetName = "pictures/maincharacter"; //name of the hero texture
        private int startPositionX = 20; //start position X in the first room
        private int startPositionY = 250; //start position Y in the first room
        private int heroSpeed = 160; //the base speed of the hero
        private int moveUp = -3; //the speed at which they move up
        private int moveDown = 3; //the speed at which they move down
        private int moveLeft = -3; //the speed at which they move left
        private int moveRight = 3; //the speed at which they move right

        KeyboardState oldState; //last known keyboard state
        MouseState oldMouse; //last known mouse state
        public Weapon[] weapons = new Weapon[2]; //array of weapons held by the Hero
        public int currentWeapon; //the current weapon the Hero is wielding
        #endregion

        /// <summary>
        /// Runs once to create the main hero.
        /// </summary>
        public override void LoadContent(ContentManager theContentManager, GraphicsDeviceManager graphics) 
        {
            Position = new Vector2(startPositionX, startPositionY);
            spriteAngle = 0.0f;
            HP = 100;
            sCurrentState = State.Moving;
            newlyCreated = false;

            //this part will need to handle file I/O in the future
            weapons[0] = new Pistol(theContentManager, graphics);
            currentWeapon = 0;

            base.LoadContent(theContentManager, heroAssetName, graphics);
        }

        /// <summary>
        /// Called every time a new frame is generated, handles character
        /// movement and mouse clicks for the hero.
        /// </summary>
        public override void Update(GameTime theGameTime) 
        {
            //updates the gun the hero is using
            weapons[currentWeapon].Update(theGameTime, base.Position);

            //handle keyboard input
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            UpdateMovement(aCurrentKeyboardState);

            //this is the hackiest way to handle weapon switching right now, sorry
            Gun gunCheck = weapons[currentWeapon] as Gun;
            if (gunCheck != null)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Q) &&
                    oldState.IsKeyUp(Keys.Q) && currentWeapon == 1)
                {
                    currentWeapon = 0;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Q) &&
                    oldState.IsKeyUp(Keys.Q) && currentWeapon == 0)
                {
                    currentWeapon = 1;
                }
            }

            oldState = aCurrentKeyboardState;

            //handle mouse input
            //REWRITE THIS TO PASS SHOOT ON EVERY UPDATE FOR MILESTONE 3
            MouseState aCurrentMouseState = Mouse.GetState();
            if (aCurrentMouseState.LeftButton == ButtonState.Pressed 
                && oldMouse.LeftButton == ButtonState.Released) 
            {
                weapons[currentWeapon].Shoot();
            }
            oldMouse = aCurrentMouseState;

            base.Update(theGameTime, spriteSpeed, spriteDirection); //must be final line
        }

        /// <summary>
        /// Draw the Hero and his gun onto the screen.
        /// </summary>
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            if (isAlive)
            {
                theSpriteBatch.Draw(base.mSpriteTexture, base.Position, new Rectangle(0, 0, base.mSpriteTexture.Width, base.mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                weapons[currentWeapon].Draw(theSpriteBatch);
            }
        }
    }
}
