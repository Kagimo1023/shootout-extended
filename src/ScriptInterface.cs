#if standalone
using SFDGameScriptInterface;
using System;

public partial class ShootoutScriptInterface : GameScriptInterface
{
    public ShootoutScriptInterface (IGame game) : base(game) {}
#endif

    Shootout.T_Game t_Game = new Shootout.T_Game("Template Game");

    public void OnStartup ()
    {
        t_Game.Start();
        if (t_Game.CurrentMode != null) {
            t_Game.CurrentMode.StartUp(true);
            t_Game.CurrentMode.RunOnStartupPlugins(t_Game.CurrentMode.GetPlugins());
            t_Game.CurrentMode.RunOnUpdatePlugins(t_Game.CurrentMode.GetPlugins());
            Events.UpdateCallback.Start(OnUpdate, 1);
        }
    }

    public void OnUpdate (float ms) {
        OnGameover();
    }

    public void OnGameover () {
        if(Game.IsGameOver && t_Game.CurrentMode != null)
            t_Game.CurrentMode.RunOnGameoverPlugins(t_Game.CurrentMode.GetPlugins());
    }

    public void AfterStartup () {
        if (t_Game.CurrentMode != null)
            t_Game.CurrentMode.RunAfterStartupPlugins(t_Game.CurrentMode.GetPlugins());
    }

    public void OnShutdown () {
        if (t_Game.CurrentMode != null)
            t_Game.CurrentMode.RunOnShutdownPlugins(t_Game.CurrentMode.GetPlugins());
    }
}
