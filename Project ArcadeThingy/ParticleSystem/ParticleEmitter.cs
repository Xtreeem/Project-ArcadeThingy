using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSystemTest
{
    class ParticleEmitter
    {
        private List<Particle> mParticles;
        private List<Texture2D> mTextures;
        private double mBurstTimer = 0;

        public Vector2 mPos { get; set; }
        public double mTimePerBurst { get; set; } = 0.25f;
        public int mParticlesPerBurst { get; set; } = 1;
        public Vector2 VelocitySpanX { get; set; } = new Vector2(-10, 10);
        public Vector2 VelocitySpanY { get; set; } = new Vector2(-10, 10);
        public Vector3 ColorRGBMin { get; set; } = new Vector3(0, 0, 0);
        public Vector3 ColorRGBMax { get; set; } = new Vector3(255, 255, 255);
        public float OpacityMin { get; set; } = 0.1f;
        public float OpacityRandomMin { get; set; } = 0.05f;
        public float OpacityRandomMax { get; set; } = 0.25f;
        public float ScaleMin { get; set; } = 0.1f;
        public float ScaleRandomMin { get; set; } = 0.05f;
        public float ScaleRandomMax { get; set; } = 0.25f;
        public Vector2 AngleVelocityMinMax { get; set; } = new Vector2(-0.1f, 0.1f);
        public float MinParticleLife { get; set; } = 0.2f;
        public float ParticleTTLRandomMin { get; set; } = -0.2f;
        public float ParticleTTLRandomMax { get; set; } = 3.2f;
        public float ParticleDepth { get; set; } = 0.2f;
        public bool MonoColor { get; set; } = false;
        public bool Activated { get; set; } = false;


        public ParticleEmitter(List<Texture2D> _Textures, Vector2 _Pos)
        {
            this.mPos = _Pos;
            this.mTextures = _Textures;
            this.mParticles = new List<Particle>();
            this.mBurstTimer = mTimePerBurst;
        }

        public void Update(GameTime _GT)
        {
            if (!Activated) return;
            mBurstTimer += _GT.ElapsedGameTime.TotalSeconds;
            if (mBurstTimer >= mTimePerBurst)
            {
                for (int i = 0; i < mParticlesPerBurst; i++)
                    mParticles.Add(GenerateParticle());
                mBurstTimer = 0;
            }

            for (int i = 0; i < mParticles.Count; i++)
            {
                mParticles[i].Update(_GT);
                if (mParticles[i].TimeToLive <= 0)
                {
                    mParticles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch _SB)
        {
            if (!Activated) return;
            for (int i = mParticles.Count - 1; i > 0; i--)
                mParticles[i].Draw(_SB);
        }

        private Particle GenerateParticle()
        {
            Texture2D tex = mTextures[GlobalStatics.Random.Next(mTextures.Count)];
            Vector2 vel = new Vector2(GlobalStatics.NextFloat(VelocitySpanX.X, VelocitySpanX.Y), GlobalStatics.NextFloat(VelocitySpanY.X, VelocitySpanY.Y));
            float angularVel = GlobalStatics.NextFloat(AngleVelocityMinMax.X, AngleVelocityMinMax.Y);
            Color color;
            if (MonoColor)
            {
                float t = GlobalStatics.NextFloat(ColorRGBMin.X, ColorRGBMax.X);
                float o = OpacityMin + GlobalStatics.NextFloat(OpacityRandomMin, OpacityRandomMax);
                color = new Color(t, t, t, o);
            }
            else
                color = new Color(GlobalStatics.NextFloat(ColorRGBMin.X, ColorRGBMax.X),
                        GlobalStatics.NextFloat(ColorRGBMin.Y, ColorRGBMax.Y),
                        GlobalStatics.NextFloat(ColorRGBMin.Z, ColorRGBMax.Z),
                        OpacityMin + GlobalStatics.NextFloat(OpacityRandomMin, OpacityRandomMax));
            float scale = (float)GlobalStatics.NextFloat(ScaleRandomMin, ScaleRandomMax) + ScaleMin;
            float timeToLive = MinParticleLife + GlobalStatics.NextFloat(ParticleTTLRandomMin, ParticleTTLRandomMax);
            return new Particle(tex, mPos, vel, 0, angularVel, color, scale, timeToLive, ParticleDepth, true);
        }

        private Color GenerateSmokeColor()
        {
            float t = GlobalStatics.NextFloat(0.2f, 0.6f);
            return new Color(new Vector3(t, t, t));
        }
    }
}
