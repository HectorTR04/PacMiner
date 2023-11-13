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
using static System.Net.Mime.MediaTypeNames;
using PacMiner.GameObjects;
using PacMiner.Game_Objects;

namespace PacMiner.BaseClasses
{
    internal class GameObject
    {
        //Texture
        protected Texture2D tex;

        //Movement
        protected Vector2 pos;
        protected Rectangle rect;
        protected Vector2 direction;
        protected Vector2 newDirection;
        protected Vector2 destination;
        protected float speed;
        protected List<Vector2> vectorList;
        protected Random random = new Random();      

        //Animation
        protected int currentFrame = 0;
        protected int numberOfFrames = 2;
        protected double timeSinceLastFrame = 0;
        protected double timeBetweenFrames = 0.2;

        //ControllerVibration
        protected float vibrationTimer;

        protected enum Movement
        {
            Stationary,
            ChangingDirection,
            Moving,
        }

        protected Movement movementState;

        public GameObject(Vector2 pos, Texture2D tex, float speed)
        {
            this.pos = pos;
            this.tex = tex;
            this.speed = 100;
            vectorList = new List<Vector2>() { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
            direction = vectorList[random.Next(vectorList.Count - 1)];
            movementState = Movement.Stationary;
            destination = Vector2.Zero;
        }

        protected Rectangle Rect
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

        public virtual void Update(GameTime gameTime)
        {          
            
            if (movementState == Movement.Stationary)
            {
                newDirection = vectorList[random.Next(vectorList.Count - 1)];
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
            vibrationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vibration();
            CheckExits();
        }

        protected bool IsTileWalkable()
        {
            Vector2 newDestination = pos + (newDirection * 32);
            return !Level.GetTileAtPosition(newDestination);
        }
        protected void Animation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastFrame >= timeBetweenFrames)
            {
                timeSinceLastFrame -= timeBetweenFrames;
                currentFrame++;
                if (currentFrame >= numberOfFrames)
                {
                    currentFrame = 0;
                }
            }
        }

        protected virtual void CheckExits()
        {
            if (pos == new Vector2(448, 736) || pos == new Vector2(896, 512))
            {
                pos = new Vector2(96, 608);
                movementState = Movement.Stationary;
            }
        }

        public virtual void CollisionPacMan(PacMan pacMan)
        {
            if (pacMan.PlayerRect.Intersects(Rect))
            {
                pacMan.health--;
                pacMan.ResetPosition();               
                vibrationTimer = 1;
            }
        }

        protected void Vibration()
        {
            if (vibrationTimer > 0)
            {
                GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
            }
            else
            {
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            if (direction.X > 0)
            {

                flip = SpriteEffects.FlipHorizontally;
            }
            Rectangle ghostRect = new Rectangle(currentFrame * tex.Width / numberOfFrames, 0,
            tex.Width / numberOfFrames, tex.Height);
            spriteBatch.Draw(tex, pos, ghostRect, Color.White, 0f, Vector2.Zero, 1f, flip, 0);
        }

    }
}
