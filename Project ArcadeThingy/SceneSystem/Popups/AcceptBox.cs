using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    class AcceptBox : MessageBox
    {
        public AcceptBox(EntryDesc _Desc, Vector2 _Size) : base(ContentManager.Dot, _Desc, _Size)
        {
            AddEntry("Back", Close);
        }
    }
}