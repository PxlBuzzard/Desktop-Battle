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
    /// Handles Hero movement using either a gamepad or keyboard.
    /// </summary>
    sealed partial class Hero : Sprite
    {
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
