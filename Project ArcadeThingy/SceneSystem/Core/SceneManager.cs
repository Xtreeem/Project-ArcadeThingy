using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    public static class SceneManager
    {
        static List<Scene> mScenes = new List<Scene>();    // list of active scenes
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        // for testing - will use contentmanager
        public static Texture2D Background { get; private set; }

        public static void Initialize(int _Width, int _Height)
        {
            Width = _Width;
            Height = _Height;

            Background = ContentManager.MenuBackground;
        }

        /// <summary>
        /// Adds new scene to list. If popup is true, previous scene will still be drawn but not updated.
        /// </summary>
        /// <param name="_NewScene">New scene to add.</param>
        /// <param name="_IsPopup">If new scene will be a popup.</param>
        public static void AddScene(Scene _NewScene, bool _IsPopup = false)
        {
            if (mScenes.Count > 0)
            {
                Scene topScene = mScenes[mScenes.Count - 1];

                if (_IsPopup)
                    topScene.Set_IsCovered(true);
                else
                    topScene.GoIdle();
            }

            mScenes.Add(_NewScene);
        }

        /// <summary>
        /// Dont call this to close a scene, it is automatically called when a scene closes.
        /// </summary>
        /// <param name="_Scene">Scene to remove.</param>
        public static void CloseScene(Scene _Scene)
        {
            int index = mScenes.IndexOf(_Scene) - 1;
            if (index >= 0)
            {
                if (!mScenes[index].IsActive)
                    mScenes[index].Wake();
            }
        }

        public static bool Update(GameTime _GT)
        {
            if (mScenes.Count <= 0)
                return false;

            if (InputManager.IsKeyClicked(Microsoft.Xna.Framework.Input.Keys.Escape))
                mScenes[mScenes.Count - 1].Close();

            for (int i = 0; i < mScenes.Count; ++i)
            {
                Scene scene = mScenes[i];

                // if finished transitioning and is about to close
                if (scene.IsTransitioning && scene.HandleTransition(_GT) && scene.IsClosing)
                {
                    // uncover scene behind closing scene
                    if (i - 1 >= 0)
                        mScenes[i - 1].Set_IsCovered(false);

                    mScenes.Remove(scene);
                    continue;
                }

                if (!scene.IsActive || scene.IsCovered)
                    continue;

                scene.HandleInput();
                scene.Update(_GT);
            }

            return true;
        }

        public static void Draw(SpriteBatch _SB)
        {
            _SB.Draw(Background, new Rectangle(0, 0, Width, Height), Color.White);

            for (int i = 0; i < mScenes.Count; ++i)
            {
                Scene scene = mScenes[i];

                if (!scene.IsTransitioning && !scene.IsActive && !scene.IsPopup)
                    continue;

                scene.Draw(_SB);
            }
        }

        public static void FadeToBlack(SpriteBatch _SB, float _Percentage)
        {
            _SB.Draw(ContentManager.Dot, new Rectangle(0, 0, Width, Height), Color.Black * _Percentage);
        }
    }
}