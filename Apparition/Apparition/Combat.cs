using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DesktopBattle
{
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
        /// Runs on every frame to handle collision detection.
        /// </summary>
        public void Update() 
        {
            // Check collision with player, if hit then lower HP and move away
            foreach (Sprite Enemy in lEnemies)
            {
                if (IntersectPixels(mHero.Size, mHero.TextureData,
                                    Enemy.Size, Enemy.TextureData))
                {
                    mHero.HP -= 10;
                    mHero.Position.X -= 20;
                }

                // Check bullet collision with enemies, if hit then lower HP
                if (IntersectPixels(mHero.Size, mHero.TextureData,
                                    Enemy.Size, Enemy.TextureData))
                {
                    Enemy.HP -= 10;
                }
            }
        }

        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// between two sprites.
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the first sprite</param>
        /// <param name="dataA">Pixel data of the first sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the second sprite</param>
        /// <param name="dataB">Pixel data of the second sprite</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }
    }
}
