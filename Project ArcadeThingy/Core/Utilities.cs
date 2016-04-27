using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public static class Utilities
    {
        public const float Gravity = 0.98f;
        public static Random Random = new Random();
        public static float NextFloat(float min, float max)
        {
            if (max < min)
                throw new ArgumentException("max cannot be less than min");
            float t = (float)Random.NextDouble() * (max - min) + min;
            return t;
        }
        public static void DrawLine(this SpriteBatch _SB, Vector2 _Begin, Vector2 _End, float _DepthLayer, Color _Color, int _Width = 1)
        {
            Rectangle r = new Rectangle((int)_Begin.X, (int)_Begin.Y, (int)(_End - _Begin).Length() + _Width, _Width);
            Vector2 v = Vector2.Normalize(_Begin - _End);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (_Begin.Y > _End.Y) angle = MathHelper.TwoPi - angle;
            _SB.Draw(ContentManager.Point, r, null, _Color, angle, Vector2.Zero, SpriteEffects.None, _DepthLayer);
        }
    }
}
