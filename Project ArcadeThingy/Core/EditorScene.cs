using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    class EditorScene : Scene
    {
        int mTileSize = PF_Platform_Base.TILE_SIZE;
        List<PF_Platform_Base> mPlatforms;
        World mWorld = new World(new Vector2(0, 9.8f));
        PF_Platform_Base mPlatform;
        PF_Platform_Base mSoonPlatform;
        bool mCreatingPlatform = false;
        bool mPlacingShrooms = true;
        bool mShowGrid = false;
        Vector2 mStartPosition;
        Vector2 mStartPlatPos;
        Platform_Type_Shroom mShroomType;
        Platform_Type_Super mSuperType;

        public EditorScene()
        {
            mPlatforms = new List<PF_Platform_Base>();
            mShroomType = Platform_Type_Shroom.Yellow;
            mPlatform = new PF_Platform_Shroom(InputManager.MousePosition(), new Vector2(2, 1), mWorld, mShroomType);
            mSuperType = 0;

            //ReadFromFile();
        }

        private void ReadFromFile()
        {
            mPlatforms.Clear();
            mPlatforms.AddRange(FileUtils.GetPlatforms(mWorld));
        }

        public override void Update(GameTime _GT)
        {
            float mouseX = InputManager.MousePosition().X;
            float mouseY = InputManager.MousePosition().Y;
            float snapX = (mouseX ) - mouseX % mTileSize;
            float snapY = (mouseY ) - mouseY % mTileSize;

            if (mPlatform != null)
            {
                mPlatform.Body.Position = new Vector2(snapX, snapY);

                if (mPlacingShrooms && !(mPlatform is PF_Platform_Shroom) || mPlatform is PF_Platform_Shroom && (mPlatform as PF_Platform_Shroom).Type != mShroomType)
                    mPlatform = new PF_Platform_Shroom(new Vector2(snapX, snapY), new Vector2(2, 1), mWorld, mShroomType);
                else if (!mPlacingShrooms && !(mPlatform is PF_Platform_Super) || mPlatform is PF_Platform_Super && (mPlatform as PF_Platform_Super).Type != mSuperType)
                    mPlatform = new PF_Platform_Super(new Vector2(snapX, snapY), new Vector2(2, 1), mWorld, mSuperType);
            }

            if (mCreatingPlatform)
            {
                var currSize = new Vector2(Math.Max(((mouseX - mStartPosition.X) / mTileSize), 2),
                                                      Math.Max(((mouseY - mStartPosition.Y) / mTileSize), 1));
                var currPos = new Vector2(mStartPlatPos.X - mStartPlatPos.X % mTileSize + (currSize.X / 2 * mTileSize), mStartPlatPos.Y - mStartPlatPos.Y % mTileSize + (currSize.Y / 2 * mTileSize));
                if (mPlacingShrooms)
                {
                    currSize.Y = 1;
                    currPos.Y = mStartPlatPos.Y - mStartPlatPos.Y % mTileSize + currSize.Y / 2;
                    mSoonPlatform = new PF_Platform_Shroom(currPos, currSize, mWorld, mShroomType);
                }
                else
                    mSoonPlatform = new PF_Platform_Super(currPos, currSize, mWorld, mSuperType);
            }
        }

        public override void HandleInput()
        {
            if (InputManager.IsKeyClicked(Keys.Tab))
                mShowGrid = !mShowGrid;
            if (InputManager.IsKeyClicked(Keys.S))
                FileUtils.SavePlatforms(mPlatforms);
            if (InputManager.IsKeyClicked(Keys.L))
                ReadFromFile();

            if (InputManager.IsKeyClicked(Keys.Right))
            {
                if (mPlacingShrooms)
                    mShroomType = (mShroomType == Platform_Type_Shroom.GreenYellow) ? 0 : mShroomType + 1;
                else
                    mSuperType = (mSuperType == Platform_Type_Super.Six) ? 0 : mSuperType + 1;
            }
            else if (InputManager.IsKeyClicked(Keys.Left))
            {
                if (mPlacingShrooms)
                    mShroomType = (mShroomType == Platform_Type_Shroom.Yellow) ? Platform_Type_Shroom.GreenYellow : mShroomType - 1;
                else
                    mSuperType = (mSuperType == Platform_Type_Super.One) ? Platform_Type_Super.Six : mSuperType - 1;
            }
            else if (InputManager.IsKeyClicked(Keys.Up) || InputManager.IsKeyClicked(Keys.Down))
                mPlacingShrooms = !mPlacingShrooms;

            if (InputManager.LeftButtonClicked())
            {
                mStartPosition = InputManager.MousePosition();
                mStartPlatPos = mPlatform.Body.Position;
                mCreatingPlatform = true;
                mPlatform = null;
            }
            if (InputManager.IsLeftButtonReleased())
            {
                mCreatingPlatform = false;
                mPlatforms.Add(mSoonPlatform);
                mSoonPlatform = null;

                if (mPlacingShrooms)
                    mPlatform = new PF_Platform_Shroom(InputManager.MousePosition(), new Vector2(2, 1), mWorld, mShroomType);
                else
                    mPlatform = new PF_Platform_Super(InputManager.MousePosition(), new Vector2(2, 1), mWorld, mSuperType);
            }

            if (InputManager.IsKeyPressed(Keys.LeftControl) && InputManager.IsKeyClicked(Keys.Z))
                if (mPlatforms.Count > 0) mPlatforms.RemoveAt(mPlatforms.Count - 1);
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