namespace Shootout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Diagnostics;
    using SFDGameScriptInterface;

    public class T_Game : SEE_Game
    {
        public T_Game (string Name) : base (Name)
        {
            Name = "Template Game";
            SetDescription (Name + ": ", "This only template for your own modes! You can add your plugins, characters, and skills to game modes!");
            CurrentMode = new Modes.Template ("Template Mode", true);
        }

        public void Start ()
        {
            long kbAfter1 = GC.GetTotalMemory(false) / (1024 * 1024);
            SEE_Game.SendMessageToHost(Color.Green, Description);
        }
    }
}