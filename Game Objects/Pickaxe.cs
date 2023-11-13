using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PacMiner.BaseClasses;
using PacMiner.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMiner.Game_Objects
{
    internal class Pickaxe : Collectibles
    {
        public Pickaxe(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            tex = TextureManager.pickaxeTex;
            this.pos = pos;
        }
    }
}
