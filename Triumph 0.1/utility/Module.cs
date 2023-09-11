using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Triumph_0._1
{
    public class Module
    {
        const int GAMEKILLER = 1;

        public struct ModuleInfo
        {
            public int id;
            public string title;
            public string description;
            public string image;
            public string icon;
            public string button;

            public ModuleInfo(int id, string ti, string de, string im, string ic, string btn)
            {
                this.id = id;
                this.title = ti;
                this.description = de;
                this.image = im;
                this.icon = ic;
                this.button = btn;
            }

        }

        const int N = 3;
        public ModuleInfo[] mainModules = new ModuleInfo[N];
        public Dictionary<int, ModuleInfo> allModules = new Dictionary<int, ModuleInfo>();

        public PageGK pageGK = null;

        public Module()
        {
            allModules.Add(GAMEKILLER, new ModuleInfo(GAMEKILLER, "Game Killer", "A ruthless killer to take you out of the pain of the game.", "images/killGame.webp", "MicrosoftXboxControllerOff", "come on!"));

            if (allModules.ContainsKey(GAMEKILLER))
            {
                mainModules[0] = allModules[GAMEKILLER];
            }
        }

        public void Execute(int id, System.Windows.DependencyObject sender)
        {
            switch (id)
            {
                case GAMEKILLER:
                    if(pageGK == null)
                        pageGK = new PageGK();
                    NavigationService.GetNavigationService(sender).Navigate(this.pageGK);
                    break;
            }
        }
    }
}
