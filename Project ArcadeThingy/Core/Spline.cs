using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    public class Spline
    {
        public List<Vector2> Positions { get; private set; }
        public List<Vector2> mBasePositions { get; private set; }
        float mDensity;

        public Spline(float _Density = 1.0f)
        {
            mDensity = _Density;
            Positions = new List<Vector2>();
            mBasePositions = new List<Vector2>();
        }

        public void Pop()
        {
            if (mBasePositions.Count <= 0)
                return;

            mBasePositions.RemoveAt(mBasePositions.Count - 1);
            UpdateSpline();
        }

        public void AddPosition(Vector2 _Position)
        {
            mBasePositions.Add(_Position);
            UpdateSpline();
        }

        public void AddRange(IEnumerable<Vector2> _Collection)
        {
            mBasePositions.AddRange(_Collection);
            UpdateSpline();
        }

        public void Clear()
        {
            mBasePositions.Clear();
            UpdateSpline();
        }

        private void UpdateSpline()
        {
            Positions.Clear();

            if (mBasePositions.Count < 4)
                return;

            for (int i = 0; i < mBasePositions.Count - 3; ++i)
            {
                Vector2 a = mBasePositions[i];
                Vector2 b = mBasePositions[i + 1];
                Vector2 c = mBasePositions[i + 2];
                Vector2 d = mBasePositions[i + 3];

                float amount = Vector2.Distance(b, c) * mDensity;

                for (int j = 0; j < amount; ++j)
                    Positions.Add(Vector2.CatmullRom(a, b, c, d, j / amount));
            }
        }

        public void DrawSpline(SpriteBatch _SB, Texture2D _Texture, Color color, float _Scale, int _DrawEvery = 1)
        {
            Vector2 origin = new Vector2(_Texture.Width / 2 * _Scale, _Texture.Height / 2 * _Scale);
            for (int i = 0; i < Positions.Count; i += _DrawEvery)
                _SB.Draw(_Texture, Positions[i], null, color, 0.0f, origin, _Scale, SpriteEffects.None, 0.5f);
        }

        public void DrawPositions(SpriteBatch _SB, Texture2D _Texture, Color color, float _Scale)
        {
            Vector2 origin = new Vector2(_Texture.Width / 2 * _Scale, _Texture.Height / 2 * _Scale);
            for (int i = 0; i < mBasePositions.Count; ++i)
                _SB.Draw(_Texture, mBasePositions[i], null, color, 0.0f, origin, _Scale, SpriteEffects.None, 1.0f);
        }
    }
}