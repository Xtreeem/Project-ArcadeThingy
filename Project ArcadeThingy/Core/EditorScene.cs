using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    enum EditorState
    {
        Super,
        Shrooms,
        Coins,
    }
    class EditorScene : Scene
    {
        int mTileSize = PF_Platform_Base.TILE_SIZE;
        List<PF_GameObj> mObjects;
        World mWorld = new World(new Vector2(0, 9.8f));
        PF_GameObj mObject;
        PF_GameObj mSoonObject;
        bool mCreatingPlatform = false;
        bool mShowGrid = false;
        Vector2 mStartPosition;
        Vector2 mStartPlatPos;
        Platform_Type_Shroom mShroomType;
        Platform_Type_Super mSuperType;
        new EditorState mState;

        public EditorScene()
        {
            mState = EditorState.Shrooms;
            mShroomType = Platform_Type_Shroom.Yellow;
            mSuperType = Platform_Type_Super.One;
            mObject = new PF_Platform_Shroom(InputManager.MousePosition(), new Vector2(2, 1), mWorld, mShroomType);
            mObjects = new List<PF_GameObj>();
        }

        private void ReadFromFile()
        {
            mObjects.Clear();
            mObjects.AddRange(FileUtils.GetPlatforms(mWorld));
        }

        public override void Update(GameTime _GT)
        {
            float mouseX = InputManager.MousePosition().X;
            float mouseY = InputManager.MousePosition().Y;
            float snapX = (mouseX) - mouseX % mTileSize;
            float snapY = (mouseY) - mouseY % mTileSize;

            if (mObject != null)
            {
                mObject.Body.Position = new Vector2(snapX, snapY);

                if (mState == EditorState.Shrooms && !(mObject is PF_Platform_Shroom) || mObject is PF_Platform_Shroom && (mObject as PF_Platform_Shroom).Type != mShroomType)
                    mObject = new PF_Platform_Shroom(new Vector2(snapX, snapY), new Vector2(2, 1), mWorld, mShroomType);
                else if (mState == EditorState.Super && !(mObject is PF_Platform_Super) || mObject is PF_Platform_Super && (mObject as PF_Platform_Super).Type != mSuperType)
                    mObject = new PF_Platform_Super(new Vector2(snapX, snapY), new Vector2(2, 1), mWorld, mSuperType);
                else if (mState == EditorState.Coins && !(mObject is PF_PowerUps_Coin))
                    mObject = new PF_PowerUps_Coin(mWorld, new Vector2(snapX, snapY), new Vector2(32, 32), 0.0);
            }

            if (mCreatingPlatform)
            {
                var currSize = new Vector2(Math.Max(((mouseX - mStartPosition.X) / mTileSize), 2),
                                                      Math.Max(((mouseY - mStartPosition.Y) / mTileSize), 1));
                var currPos = new Vector2(mStartPlatPos.X - mStartPlatPos.X % mTileSize + (currSize.X / 2 * mTileSize), mStartPlatPos.Y - mStartPlatPos.Y % mTileSize + (currSize.Y / 2 * mTileSize));
                if (mState == EditorState.Shrooms)
                {
                    currSize.Y = 1;
                    currPos.Y = mStartPlatPos.Y - mStartPlatPos.Y % mTileSize + currSize.Y / 2;
                    mSoonObject = new PF_Platform_Shroom(currPos, currSize, mWorld, mShroomType);
                }
                else if (mState == EditorState.Super)
                    mSoonObject = new PF_Platform_Super(currPos, currSize, mWorld, mSuperType);
            }
        }

        public override void HandleInput()
        {
            if (InputManager.IsKeyClicked(Keys.Tab))
                mShowGrid = !mShowGrid;
            if (InputManager.IsKeyClicked(Keys.S))
                FileUtils.SavePlatforms(mObjects);
            if (InputManager.IsKeyClicked(Keys.L))
                ReadFromFile();

            if (InputManager.IsKeyClicked(Keys.Right))
            {
                if (mState == EditorState.Shrooms)
                    mShroomType = (mShroomType == Platform_Type_Shroom.GreenYellow) ? 0 : mShroomType + 1;
                else if (mState == EditorState.Super)
                    mSuperType = (mSuperType == Platform_Type_Super.Six) ? 0 : mSuperType + 1;
            }
            else if (InputManager.IsKeyClicked(Keys.Left))
            {
                if (mState == EditorState.Shrooms)
                    mShroomType = (mShroomType == Platform_Type_Shroom.Yellow) ? Platform_Type_Shroom.GreenYellow : mShroomType - 1;
                else if (mState == EditorState.Super)
                    mSuperType = (mSuperType == Platform_Type_Super.One) ? Platform_Type_Super.Six : mSuperType - 1;
            }
            else if (InputManager.IsKeyClicked(Keys.Up))
                mState = (mState == EditorState.Coins) ? EditorState.Super : mState + 1;
            else if (InputManager.IsKeyClicked(Keys.Down))
                mState = (mState == EditorState.Super) ? EditorState.Coins : mState - 1;

            if (InputManager.LeftButtonClicked())
            {
                if (mState == EditorState.Coins)
                {
                    mObjects.Add(new PF_PowerUps_Coin(mWorld, InputManager.MousePosition(), new Vector2(32, 32), 0.0));
                    return;
                }

                mStartPosition = InputManager.MousePosition();
                mStartPlatPos = mObject.Body.Position;
                mCreatingPlatform = true;
                mObject = null;
            }

            if (InputManager.IsLeftButtonReleased())
            {
                if (mState == EditorState.Coins)
                    return;

                mCreatingPlatform = false;
                mObjects.Add(mSoonObject);
                mSoonObject = null;

                if (mState == EditorState.Shrooms)
                    mObject = new PF_Platform_Shroom(InputManager.MousePosition(), new Vector2(2, 1), mWorld, mShroomType);
                else if (mState == EditorState.Super)
                    mObject = new PF_Platform_Super(InputManager.MousePosition(), new Vector2(2, 1), mWorld, mSuperType);

            }

            if (InputManager.IsKeyPressed(Keys.LeftControl) && InputManager.IsKeyClicked(Keys.Z))
                if (mObjects.Count > 0) mObjects.RemoveAt(mObjects.Count - 1);
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

            for (int i = 0; i < mObjects.Count; ++i)
                mObjects[i].Draw(_SB);

            if (mObject != null)
                mObject.Draw(_SB);

            if (mSoonObject != null)
                mSoonObject.Draw(_SB);
        }
    }
}