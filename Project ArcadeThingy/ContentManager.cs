using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public static class ContentManager
    {
        public static List<Texture2D> Particles { get; private set; }
        public static Texture2D Point { get; private set; }
        public static Texture2D BasicCharacter { get; private set; }
        public static Texture2D BasicPlatform { get; private set; }
        public static Texture2D MarioSheet { get; private set; }
        public static Texture2D ShroomPlatform { get; private set; }


        public static void Load(Microsoft.Xna.Framework.Content.ContentManager _Content)
        {
            Particles = new List<Texture2D>();
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire1"));
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire2"));
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire3"));

            Point = _Content.Load<Texture2D>("Point");
            BasicCharacter = _Content.Load<Texture2D>("BasicCharacter");
            MarioSheet = _Content.Load<Texture2D>("SpriteSheets\\MarioSheet");
            ShroomPlatform = _Content.Load<Texture2D>("Platform\\Shrooms");
            BasicPlatform = _Content.Load<Texture2D>("BasicPlatform");


        } 
    }
}
