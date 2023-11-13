using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMiner.Managers
{
    internal class ScoreManager
    {
        private int score;
        private int scoreDecrease = 1;
        private float lastScoreDecrease = 0;
        private float scoreDecreaseInterval = 1f;


        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
