namespace BTFS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SFDGameScriptInterface;

    public class Shootout_Extended : BTFS_Game
    {
        public Shootout_Extended(string _Name = "Shootout Extended") : base(_Name)
        {
            Name = _Name;
            Version = "Pre-Alpha v0.50";
            Features = "- New plugin Doors Generator!\n" +
                       "- New plugin Spectating Mode!\n" +
                       "- Fixed many bugs in script!\n\n";

            CurrentMode = new Modes.Default();
        }
    }
}