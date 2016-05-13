using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace Project_ArcadeThingy
{
    class MainMenuScene : MenuScene
    {
        public MainMenuScene() : base()
        {
            EntryDesc menuDesc = new EntryDesc(ContentManager.Font, new Vector2(SceneManager.Width / 2, SceneManager.Height / 2));
            menuDesc.Color = Color.Coral;
            menuDesc.SelectedColor = Color.LightBlue;

            Set_Desc(menuDesc);
            AddEntry("Start", Start);
            AddEntry("Level Editor", Edit);
            AddEntry("Exit", Close);
        }

        private void Start()
        {
            SceneManager.AddScene(new GameScene());
        }

        private void Edit()
        {
            SceneManager.AddScene(new EditorScene());
        }
    }
}