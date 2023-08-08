using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triumph_0._1
{
    internal class Func
    {
        const int GAMEKILLER = 1;

        struct FuncInfo
        {
            public string title;
            public string description;
            public string image;
            public string icon;
            public string button;

            public FuncInfo(string ti, string de, string im, string ic, string btn)
            {
                title = ti;
                description = de;
                image = im;
                icon = ic;
                button = btn;
            }
        }

        const int N = 3;
        private FuncInfo[] homeFunc = new FuncInfo[N];
        private Dictionary<int, FuncInfo> allFunc = new Dictionary<int, FuncInfo>();

        public Func()
        {
            allFunc.Add(GAMEKILLER, new FuncInfo("Game Killer", "A ruthless killer to take you out of the pain of the game.", "images/killGame.webp", "MicrosoftXboxControllerOff", "come on!"));
        }
    }
}
