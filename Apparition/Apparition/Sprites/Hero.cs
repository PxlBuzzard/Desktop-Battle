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
        public int totalEnemiesKilled; //total enemies killed by the player
        public int startPositionX = 20; //start position X in the first room
        public int startPositionY = 250; //start position Y in the first room
        public int heroSpeed = 500; //the base speed of the hero
        public int moveUp = -3; //the speed at which they move up
        public int moveDown = 3; //the speed at which they move down
        public int moveLeft = -3; //the speed at which they move left
        public int moveRight = 3; //the speed at which they move right
        public List<Weapon> weapons = new List<Weapon>(); //array of weapons held by the Hero
        public int currentWeapon; //the current weapon the Hero is wielding
        public int nextWeapon { get //goes to the next available weapon in the array
        {
            if (weapons[currentWeapon + 1] == null)
              currentWeapon = 0;
            else currentWeapon++;
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
            if (HP <= 0) HP = 100;
            sCurrentState = State.Moving;
            newlyCreated = false;

            weapons.Add(new Pistol());
            if (sBullets.empty()) sBullets = Weapon.GenerateBulletStack(75);

            //sanity checks on all the Hero variables
            moveDown = (int)MathHelper.Clamp(moveDown, 1, 10);
            moveUp = (int)MathHelper.Clamp(moveUp, -10, -1);
            moveLeft = (int)MathHelper.Clamp(moveLeft, -10, -1);
            moveRight = (int)MathHelper.Clamp(moveRight, 1, 10);
            startPositionX = (int)MathHelper.Clamp(startPositionX, 1, Game1.graphics.GraphicsDevice.Viewport.Width);
            startPositionY = (int)MathHelper.Clamp(startPositionY, 1, Game1.graphics.GraphicsDevice.Viewport.Height);
            HP = (int)MathHelper.Clamp(HP, 1, 10000);
            totalEnemiesKilled = (int)MathHelper.Clamp(totalEnemiesKilled, 0, 100000);

            Position = new Vector2(startPositionX, startPositionY);

            base.LoadContent(heroAssetName);
        }

        /// <summary>
        /// Called every time a new frame is generated, handles character
        /// movement and mouse clicks for the hero.
        /// </summary>
        public override void Update(GameTime theGameTime) 
        {
            //handle keyboard input
            UpdateMovement();
            base.Update(theGameTime, spriteSpeed, spriteDirection);

            //updates the gun the hero is using
            weapons[currentWeapon].Update(theGameTime, base.Position);

            //deals with anything related to weapons
            WeaponHandling();

            //handle mouse input
            if (Game1.currentMouseState.LeftButton == ButtonState.Pressed 
                && Game1.lastMouseState.LeftButton == ButtonState.Released) 
            {
                weapons[currentWeapon].Shoot();
            }
            //flips the Hero horizontally depending on the mouse position
            if (Position.X >= Game1.currentMouseState.X)
                isFlipped = true;
            else
                isFlipped = false;
        }

        private void WeaponHandling()
        {
            //switch weapons
            if (Game1.currentKeyboardState.IsKeyDown(Keys.Q) &&
                Game1.lastKeyboardState.IsKeyUp(Keys.Q))
            {
                currentWeapon = nextWeapon;
            }

            //gives the Hero an M16 at the end of room 2
            if (Game1.cArea.currentRoom == 1 && Game1.cArea.areaClear && weapons.Count() == 1)
            {
                weapons.Add(new M16());
                Game1.cGameUI.Draw("You unlocked the M16! Press Q to cycle weapons.");
            }

            //gives the Hero a Shotgun after 100 total kills
            if (totalEnemiesKilled >= 100 && weapons.Count() == 2)
            {
                weapons.Add(new Shotgun());
                Game1.cGameUI.Draw("You unlocked the Shotgun! Press Q to cycle weapons.");
            }
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
        private void UpdateMovement()
        {

            if ((sCurrentState == State.Moving || sCurrentState == State.Idling) && isAlive)
            {
                spriteSpeed = Vector2.Zero;
                spriteDirection = Vector2.Zero;

                if (Game1.currentKeyboardState.IsKeyDown(Keys.Left) || Game1.currentKeyboardState.IsKeyDown(Keys.A))
                {
                    spriteSpeed.X = heroSpeed;
                    spriteDirection.X = moveLeft;
                }
                else if (Game1.currentKeyboardState.IsKeyDown(Keys.Right) || Game1.currentKeyboardState.IsKeyDown(Keys.D))
                {
                    spriteSpeed.X = heroSpeed;
                    spriteDirection.X = moveRight;
                }

                if (Game1.currentKeyboardState.IsKeyDown(Keys.Up) || Game1.currentKeyboardState.IsKeyDown(Keys.W))
                {
                    spriteSpeed.Y = heroSpeed;
                    spriteDirection.Y = moveUp;
                }
                else if (Game1.currentKeyboardState.IsKeyDown(Keys.Down) || Game1.currentKeyboardState.IsKeyDown(Keys.S))
                {
                    spriteSpeed.Y = heroSpeed;
                    spriteDirection.Y = moveDown;
                }
            }
        }
    }
}
