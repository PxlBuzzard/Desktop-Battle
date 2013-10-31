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
    [Serializable()]
    public class Hero : Sprite
    {
        #region Class Variables
        [System.Xml.Serialization.XmlIgnore]
        public string heroAssetName = "pictures/maincharacter"; //name of the hero texture
        public int startPositionX = 20; //start position X in the first room
        public int startPositionY = 250; //start position Y in the first room
        public int heroSpeed = 160; //the base speed of the hero
        public int moveUp = -3; //the speed at which they move up
        public int moveDown = 3; //the speed at which they move down
        public int moveLeft = -3; //the speed at which they move left
        public int moveRight = 3; //the speed at which they move right
        private KeyboardState oldState; //last known keyboard state
        private MouseState oldMouse; //last known mouse state
        public Weapon[] weapons = new Weapon[2]; //array of weapons held by the Hero
        public int currentWeapon; //the current weapon the Hero is wielding
        public int nextWeapon { get //goes to the next available weapon in the array
        {
            if (currentWeapon + 1 >= weapons.Count() || weapons[currentWeapon+1] == null)
            { currentWeapon = 0; }
            else { currentWeapon++; }
            return currentWeapon; } 
        }
        public bool isFlipped; //flips the Hero sprite
        private SpriteEffects SpriteEffect
        {
            get
            {
                if (isFlipped) return SpriteEffects.FlipHorizontally;
                else return SpriteEffects.None;
            }
        }

        private static List<Bullet> listBullets = new List<Bullet>();
        public static List<Bullet> lBullets //list of all the onscreen bullets
        {
            get { return listBullets; }
            set { listBullets = value; }
        }
        private static Stack<Bullet> stackBullets = new Stack<Bullet>();
        public static Stack<Bullet> sBullets //stack of all the offscreen bullets
        {
            get { return stackBullets; }
            set { stackBullets = value; }
        }
        #endregion

        /// <summary>
        /// Runs once to create the main hero.
        /// </summary>
        public override void LoadContent() 
        {
            if (HP == 0) HP = 100;
            sCurrentState = State.Moving;
            newlyCreated = false;

            //if (weapons.ElementAt(0) is Weapon) 
            weapons[0] = new Pistol();
            if (sBullets.empty()) sBullets = Weapon.GenerateBulletStack(50);

            //sanity checks on all the Hero variables
            moveDown = (int)MathHelper.Clamp(moveDown, 1, 10);
            moveUp = (int)MathHelper.Clamp(moveUp, -10, -1);
            moveLeft = (int)MathHelper.Clamp(moveLeft, -10, -1);
            moveRight = (int)MathHelper.Clamp(moveRight, 1, 10);
            startPositionX = (int)MathHelper.Clamp(startPositionX, 1, maxX);
            startPositionY = (int)MathHelper.Clamp(startPositionY, 1, maxY);
            HP = (int)MathHelper.Clamp(HP, 1, 10000);

            Position = new Vector2(startPositionX, startPositionY);

            base.LoadContent(heroAssetName);
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
            if (aCurrentKeyboardState.IsKeyDown(Keys.Q) &&
                oldState.IsKeyUp(Keys.Q))
            {
                currentWeapon = nextWeapon;
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
            //flips the Hero horizontally depending on the mouse position
            if (Position.X >= aCurrentMouseState.X)
            {
                isFlipped = true;
            }
            else
            {
                isFlipped = false;
            }
            oldMouse = aCurrentMouseState;

            base.Update(theGameTime, spriteSpeed, spriteDirection); //must be final line
        }

        /// <summary>
        /// Draw the Hero and his weapon onto the screen.
        /// </summary>
        public override void Draw()
        {
            if (isAlive)
            {
                Game1.spriteBatch.Draw(base.mSpriteTexture, base.Position, new Rectangle(0, 0, base.mSpriteTexture.Width, base.mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffect, 0);
                weapons[currentWeapon].Draw();
            }
        }

        /// <summary>
        /// Handles all movement for the hero.
        /// </summary>
        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if ((sCurrentState == State.Moving || sCurrentState == State.Idling) && isAlive)
            {
                spriteSpeed = Vector2.Zero;
                spriteDirection = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) || aCurrentKeyboardState.IsKeyDown(Keys.A))
                {
                    spriteSpeed.X = heroSpeed;
                    spriteDirection.X = moveLeft;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) || aCurrentKeyboardState.IsKeyDown(Keys.D))
                {
                    spriteSpeed.X = heroSpeed;
                    spriteDirection.X = moveRight;
                }

                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) || aCurrentKeyboardState.IsKeyDown(Keys.W))
                {
                    spriteSpeed.Y = heroSpeed;
                    spriteDirection.Y = moveUp;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) || aCurrentKeyboardState.IsKeyDown(Keys.S))
                {
                    spriteSpeed.Y = heroSpeed;
                    spriteDirection.Y = moveDown;
                }
            }
        }
    }
}
