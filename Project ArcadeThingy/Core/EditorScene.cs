using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    // TODO: collision
    class EditorScene : Scene
    {
        int mTileSize = BasePlatform.TILE_SIZE;
        List<BasePlatform> mPlatforms = new List<BasePlatform>();
        World mWorld = new World(new Vector2(0, 9.8f));
        BasePlatform mPlatform;
        BasePlatform mSoonPlatform;
        bool mCreatingPlatform = false;
        bool mShowGrid = false;
        Vector2 mStartPosition;
        Vector2 mStartPlatPos;
        ShroomType mShroomType;
        int mType;

        public EditorScene()
        {
            mShroomType = ShroomType.Yellow;
            mPlatform = new ShroomPlatform(mShroomType, new Vector2(32, 16), InputManager.MousePosition(), ref mWorld);
            mType = 0;
        }

        public override void Update(GameTime _GT)
        {
            float mouseX = InputManager.MousePosition().X;
            float mouseY = InputManager.MousePosition().Y;
            float snapX = (mouseX + mTileSize / 2) - mouseX % mTileSize;
            float snapY = (mouseY + mTileSize * 1.5f) - mouseY % mTileSize;

            mWorld.Step((float)_GT.ElapsedGameTime.TotalSeconds);

            if (mPlatform != null)
            {
                mPlatform.Body.Body.Position = new Vector2(snapX, snapY).PixelsToUnits();

                if (mType == 6 && !(mPlatform is ShroomPlatform) || mPlatform is ShroomPlatform && (mPlatform as ShroomPlatform).Type != mShroomType)
                    mPlatform = new ShroomPlatform(mShroomType, new Vector2(32, 16), new Vector2(snapX, snapY), ref mWorld);
                else if (mType != 6 && !(mPlatform is SuperPlatform) || mPlatform is SuperPlatform && (mPlatform as SuperPlatform).Type != mType)
                    mPlatform = new SuperPlatform(mType, new Vector2(32, 16), new Vector2(snapX, snapY), ref mWorld);
            }

            if (mCreatingPlatform)
            {
                var currSize = new Vector2(Math.Max(mTileSize * (Math.Abs((mouseX - mStartPosition.X) / mTileSize)), mTileSize * 2),
                    Math.Max(mTileSize * (Math.Abs((mouseY - mStartPosition.Y) / mTileSize)), mTileSize));
                currSize.X = currSize.X - currSize.X % mTileSize;
                var currPos = new Vector2(mStartPlatPos.X - mStartPlatPos.X % mTileSize + currSize.X / 2, mStartPlatPos.Y - mStartPlatPos.Y % mTileSize + currSize.Y / 2);
                if (mType == 6)
                {
                    currSize.Y = mTileSize;
                    currPos.Y = mStartPlatPos.Y - mStartPlatPos.Y % mTileSize;
                    mSoonPlatform = new ShroomPlatform(mShroomType, currSize, currPos, ref mWorld);
                }
                else
                    mSoonPlatform = new SuperPlatform(mType, currSize, currPos, ref mWorld);
            }
        }

        public override void HandleInput()
        {
            if (InputManager.IsKeyClicked(Keys.Tab))
                mShowGrid = !mShowGrid;

            if (InputManager.IsKeyClicked(Keys.Right))
                mType = (mType == 6) ? 0 : mType + 1;
            else if (InputManager.IsKeyClicked(Keys.Left))
                mType = (mType == 0) ? 6 : mType - 1;

            if (InputManager.IsLeftButtonClicked())
            {
                mStartPosition = InputManager.MousePosition();
                mStartPlatPos = mPlatform.Body.Body.Position.UnitToPixels();
                mCreatingPlatform = true;
                mPlatform = null;
            }
            else if (InputManager.IsLeftButtonReleased())
            {
                mCreatingPlatform = false;
                mPlatforms.Add(mSoonPlatform);
                mSoonPlatform = null;
                if (mType == 7)
                    mPlatform = new ShroomPlatform(mShroomType, new Vector2(32, 16), InputManager.MousePosition(), ref mWorld);
                else
                    mPlatform = new SuperPlatform(mType, new Vector2(32, 16), InputManager.MousePosition(), ref mWorld);
            }
            if (InputManager.IsKeyClicked(Keys.Space))
                mShroomType = (mShroomType == ShroomType.GreenYellow) ? ShroomType.Yellow : mShroomType + 1;
        }

        public override void Draw(SpriteBatch _SB)
        {
            if (mShowGrid)
            {
                for (int x = 0; x < SceneManager.Width / mTileSize; ++x)
                {
                    Vector2 start = new Vector2(x * mTileSize, 0.0f);
                    Vector2 end = new Vector2(start.X, SceneManager.Height);
                    _SB.DrawLine(start, end, 1.0f, Color.Black, 2);
                }
                for (int y = 0; y < SceneManager.Height / mTileSize; ++y)
                {
                    Vector2 start = new Vector2(0.0f, y * mTileSize);
                    Vector2 end = new Vector2(SceneManager.Width, start.Y);
                    _SB.DrawLine(start, end, 1.0f, Color.Black, 2);
                }
            }

            for (int i = 0; i < mPlatforms.Count; ++i)
                mPlatforms[i].Draw(_SB);

            if (mPlatform != null)
                mPlatform.Draw(_SB);

            if (mSoonPlatform != null)
                mSoonPlatform.Draw(_SB);
        }
    }
}