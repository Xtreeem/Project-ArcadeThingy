using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public static class Utilities
    {
        public static Random Random = new Random();
        public static float NextFloat(float min, float max)
        {
            if (max < min)
                throw new ArgumentException("max cannot be less than min");
            float t = (float)Random.NextDouble() * (max - min) + min;
            return t;
        }
    }
}
