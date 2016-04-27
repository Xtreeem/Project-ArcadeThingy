using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public enum ParticlePreSet
    {
        Random,
        Cuteness,
        Fire

    }

    class ParticleSystem
    {
        //Settings
        public bool Active { get; set; } = true;

        //Internals
        List<ParticleEmitter> mEmitters = new List<ParticleEmitter>();
        Vector2 mPos;

        public void Update(GameTime _GT, Vector2 _Pos)
        {
            if (!Active) return;
            if (_Pos != Vector2.Zero)
                foreach (ParticleEmitter E in mEmitters)
                    E.mPos = _Pos;
            foreach (ParticleEmitter E in mEmitters)
                E.Update(_GT);
        }

        public void Update(GameTime _GT)
        {
            Update(_GT, Vector2.Zero);
        }

        public void Draw(SpriteBatch _SB)
        {
            foreach (ParticleEmitter E in mEmitters)
                E.Draw(_SB);
        }

        public ParticleSystem(ParticlePreSet _Input, Vector2 _Pos)
        {
            mPos = _Pos;
            switch (_Input)
            {
                case ParticlePreSet.Random:
                    break;
                case ParticlePreSet.Cuteness:
                    break;
                case ParticlePreSet.Fire:
                    SetUpFire();
                    break;
                default:
                    break;
            }
        }

        private void SetUpFire()
        {
            GenerateSmokeEmitter(mPos);
            GenerateFireEmitter(mPos);
        }

        private void GenerateSmokeEmitter(Vector2 _Pos)
        {
            ParticleEmitter t = new ParticleEmitter(ContentManager.Particles, mPos);
            t.AngleVelocityMinMax = new Vector2(-0.2f, 0.2f);
            t.ColorRGBMin = new Vector3(0.0f, 0.0f, 0.0f);
            t.ColorRGBMax = new Vector3(0.2f, 0.2f, 0.2f);
            t.MinParticleLife = 0.5f;
            t.mParticlesPerBurst = 15;
            t.mPos = _Pos;
            t.mTimePerBurst = 0.01f;
            t.OpacityMin = 0.1f;
            t.OpacityRandomMax = 0.25f;
            t.OpacityRandomMin = 0.05f;
            t.ParticleDepth = 0.9f;
            t.MinParticleLife = 0.4f;
            t.ParticleTTLRandomMin = 0.2f;
            t.ParticleTTLRandomMax = 1.5f;
            t.VelocitySpanX = new Vector2(-10, 10);
            t.VelocitySpanY = new Vector2(-10, 10);
            t.ScaleMin = 0.3f;
            t.ScaleRandomMin = 0.1f;
            t.ScaleRandomMax = 0.3f;
            t.MonoColor = true;
            t.Activated = true;
            mEmitters.Add(t);
        }
        private void GenerateFireEmitter(Vector2 _Pos)
        {
            ParticleEmitter t = new ParticleEmitter(ContentManager.Particles, mPos);
            t.AngleVelocityMinMax = new Vector2(-0.2f, 0.2f);
            t.ColorRGBMin = new Vector3(1.0f, 0.2f, 0.0f);
            t.ColorRGBMax = new Vector3(1.0f, 0.6f, 0.0f);
            t.MinParticleLife = 0.3f;
            t.mParticlesPerBurst = 5;
            t.mPos = _Pos;
            t.mTimePerBurst = 0.01f;
            t.OpacityMin = 0.1f;
            t.OpacityRandomMax = 0.25f;
            t.OpacityRandomMin = 0.05f;
            t.ParticleDepth = 0.9f;
            t.MinParticleLife = 0.1f;
            t.ParticleTTLRandomMin = 0.2f;
            t.ParticleTTLRandomMax = 0.8f;
            t.VelocitySpanX = new Vector2(-10, 10);
            t.VelocitySpanY = new Vector2(-10, 10);
            t.ScaleMin = 0.2f;
            t.ScaleRandomMin = 0.05f;
            t.ScaleRandomMax = 0.15f;
            t.MonoColor = false;
            t.Activated = true;
            mEmitters.Add(t);
        }

    }
}
