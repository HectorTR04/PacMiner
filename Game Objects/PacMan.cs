using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PacMiner.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMiner.Managers;
using PacMiner.Game_Objects;
using static PacMiner.GameStates.InputSelect;
using PacMiner.BaseClasses;
using Microsoft.Xna.Framework.Audio;

namespace PacMiner.GameObjects
{
    internal class PacMan : GameObject
    {
        //Texture
        private Texture2D texX;
        private Texture2D texY;
        private Texture2D startTex;

        //Movement
        private Vector2 startPos = new Vector2(448, 736);
        private Vector2 rightExit = new Vector2(896,512);
        private Vector2 leftExit = new Vector2(0,512);
        private Vector2 facingUp = new Vector2(0, -1);
        private Vector2 facingDown = new Vector2(0, 1);
        private Vector2 facingLeft = new Vector2(-1, 0);
        private Vector2 facingRight = new Vector2(1, 0);
       
        //Animation
        private bool MoveX = false;
        private bool MoveY = false;

        //Misc
        internal int health;
       
        public PacMan(Vector2 pos, Texture2D tex, float speed) : base(pos, tex, speed)
        {
            startTex = TextureManager.startTex;
            texX = TextureManager.pacXTex;
            texY = TextureManager.pacYTex;
            direction = Vector2.Zero;
            movementState = Movement.Stationary;
            destination = Vector2.Zero;
        }

        public Rectangle PlayerRect
        {
            get
            {
                rect.Y = (int)pos.Y;
                rect.X = (int)pos.X;
                rect.Width = (int)tex.Width;
                rect.Height = (int)tex.Height;
                return rect;
            }
        }

        public override void Update(GameTime gameTime)
        {
           
            KeyMouseReader.Update();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (movementState == Movement.Stationary)
            {
                if (selectedInput == Input.Keyboard)
                {
                    if (KeyMouseReader.KeyPressed(Keys.Up))
                    {
                        newDirection = facingUp;
                        MoveY = true;
                        MoveX = false;
                    }
                    else if (KeyMouseReader.KeyPressed(Keys.Left))
                    {
                        newDirection = facingLeft;
                        MoveX = true;
                        MoveY = false;
                    }
                    else if (KeyMouseReader.KeyPressed(Keys.Down))
                    {
                        newDirection = facingDown;
                        MoveY = true;
                        MoveX = false;
                    }
                    else if (KeyMouseReader.KeyPressed(Keys.Right))
                    {
                        newDirection = facingRight;
                        MoveX = true;
                        MoveY = false;
                    }
                } 
                if (selectedInput == Input.Controller)
                {
                    Vector2 gamepadDirection = gamePadState.ThumbSticks.Left;
                    if (gamepadDirection.Y > 0.1)
                    {
                        newDirection = facingUp;
                        MoveY = true;
                        MoveX = false;
                    }
                    else if (gamepadDirection.X < -0.1)
                    {
                        newDirection = facingLeft;
                        MoveX = true;
                        MoveY = false;
                    }
                    else if (gamepadDirection.Y < -0.1)
                    {
                        newDirection = facingDown;
                        MoveY = true;
                        MoveX = false;
                    }
                    else if (gamepadDirection.X > 0.1)
                    {
                        newDirection = facingRight;
                        MoveX = true;
                        MoveY = false;
                    }
                }

                if (newDirection != Vector2.Zero && IsTileWalkable())
                {
                    movementState = Movement.ChangingDirection;
                }
            }
            else if (movementState == Movement.ChangingDirection)
            {
                direction = newDirection;
                Vector2 newDestination = pos + direction * 32;
                if (!Level.GetTileAtPosition(newDestination))
                {
                    destination = newDestination;
                    movementState = Movement.Moving;
                }
            }
            else if (movementState == Movement.Moving)
            {
                pos += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Vector2.Distance(pos, destination) < 2)
                {
                    pos = destination;
                    movementState = Movement.Stationary;
                }
            }
            Animation(gameTime);
            CheckExits();
        }

        protected override void CheckExits()
        {
            if (pos == rightExit && direction == facingRight)
            {
                pos = leftExit;
            }
            if (pos == leftExit && direction == facingLeft)
            {
                pos = rightExit;
            }
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects flipX = SpriteEffects.None;
            if (direction.X > 0)
            {

                flipX = SpriteEffects.FlipHorizontally;
            }
            SpriteEffects flipY = SpriteEffects.None;
            if (direction.Y > 0)
            {

                flipY = SpriteEffects.FlipVertically;
            }
            if (!MoveX && !MoveY)
            {
                spriteBatch.Draw(startTex, pos, null, Color.White, 0f, Vector2.Zero, 1f, flipX, 0);
            }

            if (MoveX)
            {
                Rectangle pacXRect = new Rectangle(currentFrame * texX.Width / numberOfFrames, 0,
                texX.Width / numberOfFrames, texX.Height);
                spriteBatch.Draw(texX, pos, pacXRect, Color.White, 0f, Vector2.Zero, 1f, flipX, 0);
            }
            if (MoveY)
            {
                Rectangle pacYRect = new Rectangle(currentFrame * texY.Width / numberOfFrames, 0,
                texY.Width / numberOfFrames, texY.Height);
                spriteBatch.Draw(texY, pos, pacYRect, Color.White, 0f, Vector2.Zero, 1f, flipY, 0);
            }

        }

        public void CollisionGold(Gold gold)
        {
            if (PlayerRect.Intersects(gold.ItemRect) && !gold.isCollected)
            {
                gold.isCollected = true;
                Level.scoreManager.Score += 1;
                SoundManager.eatSound.Play();
            }
        }

        public void ResetPosition()
        {
            pos = startPos;
            movementState = Movement.Stationary;
        }

        public Vector2 PacmanPos
        {
            get { return pos; }
        }
    }
}
