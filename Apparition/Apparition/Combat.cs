#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Handles all combat-related events ingame, such as damage checks.
    /// </summary>
    class Combat 
    {
        #region Class Variables
        Random rnd = new Random();
        Hero mHero;
        List<Sprite> lEnemies;
        #endregion

        /// <summary>
        /// Runs at startup to get information passed into Combat.
        /// </summary>
        /// <param name="theHero">the hero class</param>
        /// <param name="enemies">the list of enemies</param>
        public void LoadContent(Hero theHero, List<Sprite> enemies) 
        {
            mHero = theHero;
            lEnemies = enemies;
        }

        /// <summary>
        /// Fills the list of enemies with new enemies.
        /// </summary>
        /// <param name="numEnemies">number of enemies to add</param>
        public void CreateEnemies(int numEnemies) 
        {
            for (int i = 1; i <= numEnemies; i++) 
            {
                lEnemies.Add(new Clippy());
            }
        }

        /// <summary>
        /// Runs on every frame to handle collision detection.
        /// </summary>
        public void Update(GameTime gameTime) 
        {
            // Check collision with player, if hit then lower HP and move away
            foreach (Sprite Enemy in lEnemies)
            {
                foreach (Bullet Bullet in mHero.weapons[mHero.currentWeapon].lBullets)
                {
                    if ((Bullet.Size).Intersects(Enemy.Size))
                    {
                        Enemy.HP -= 10; //CHANGE THIS AFTER MILESTONE 2!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        Bullet.isAlive = false;
                    }
                }
                if ((mHero.Size).Intersects(Enemy.Size))
                {
                    mHero.HP -= 10;
                    mHero.Position.X -= 50;
                }
            }

            //updates and removes bullets if necessary
            for (int i = lEnemies.Count() - 1; i >= 0; i--)
            {
                lEnemies[i].Update(gameTime);
                if (!lEnemies[i].isAlive)
                {
                    lEnemies.Remove(lEnemies[i]);
                }
            }
        }
    }
}
