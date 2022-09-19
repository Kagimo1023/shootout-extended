namespace Shootout
{
    using SFDGameScriptInterface;
    using System;

    public class Plugin
    {
        public string Name { get; set; }
        public bool Enable { get; set; }
        
        private float OnUpdateSleep = 0;
        private ushort OnUpdateCycles = 1;

        public string [] Description = null;

        public static IGame Game;

        public Plugin() {}

        public void Information () { SEE_Game.SendMessageToAll(Color.Green, Description); }
        public void Information (IUser user){ SEE_Game.SendMessageToPlayer(user, Color.Green, Description); }
        
        public float GetSleepOnUpdate(){ return OnUpdateSleep; }
        public ushort GetCyclesOnUpdate () { return OnUpdateCycles; }
        public string [] GetDescription () { return Description; }
        
        public void SetSleepOnUpdate (float ms) { OnUpdateSleep = ms; }
        public void SetCyclesOnUpdate (ushort c) { OnUpdateCycles = c; }
        public void SetDescription (params string [] str) { Description = str; }
            
        public virtual void OnStartup () {}
        public virtual void AfterStartup () {}
        public virtual void OnShutdown () {}
        public virtual void OnUpdate (float ms) {}
        public virtual void OnGameover () {}
//         public virtual void OnCharacterSpawn () { } // Specific shootout extended implementation
    }
}
