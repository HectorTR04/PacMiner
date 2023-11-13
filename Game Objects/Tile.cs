using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMiner.Managers;
using PacMiner.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PacMiner.Managers.GameStateManager;

namespace PacMiner.GameObjects
{
    public class Tile
    {
        public Vector2 pos;
        public Texture2D tex;
        public bool notWalkable;

        public Tile(Vector2 pos, Texture2D tex, bool notWalkable)
        {
            this.pos = pos;
            this.tex = tex;
            this.notWalkable = notWalkable;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
