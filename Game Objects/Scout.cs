using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PacMiner.BaseClasses;
using PacMiner.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMiner.GameStates;
using PacMiner.GameObjects;

namespace PacMiner.Game_Objects
{
    internal class Scout : GameObject
    {

        public Scout(Vector2 pos, Texture2D tex, float speed) : base(pos, tex, speed)
        {
            tex = TextureManager.scoutTex;
            this.pos = pos;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
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
                    if (!IsTileWalkable())
                    {
                        movementState = Movement.Stationary;
                    }
                    else
                    {
                        Vector2 newDestination = pos + direction * 32;
                        destination = newDestination;
                    }
                }
            }
            Animation(gameTime);
            vibrationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vibration();
        }
     
    }
}
