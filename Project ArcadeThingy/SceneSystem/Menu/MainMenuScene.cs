using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class MainMenuScene : MenuScene
    {
        ParticleSystem mPSystem;
        public MainMenuScene() : base()
        {
            EntryDesc menuDesc = new EntryDesc(ContentManager.Font, new Vector2(SceneManager.Width / 2, SceneManager.Height / 2));
            menuDesc.Color = Color.Coral;
            menuDesc.SelectedColor = Color.LightBlue;

            Set_Desc(menuDesc);
            AddEntry("Start", Start);
            AddEntry("Level Editor", Edit);
            AddEntry("Exit", Close);

            mPSystem = new ParticleSystem(ParticlePreSet.Menu, mSelectedEntry.Positon);
        }

        public override void Update(GameTime _GT)
        {
            mPSystem.Update(_GT, mSelectedEntry.Positon);
            base.Update(_GT);
        }

        public override void Draw(SpriteBatch _SB)
        {
            mPSystem.Draw(_SB);
            base.Draw(_SB);
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