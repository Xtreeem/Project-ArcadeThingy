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
        

        public static void Load(Microsoft.Xna.Framework.Content.ContentManager _Content)
        {
            Particles = new List<Texture2D>();
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire1"));
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire2"));
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire3"));
        } 
    }
}
