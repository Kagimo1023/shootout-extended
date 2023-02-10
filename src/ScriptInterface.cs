#if standalone
using SFDGameScriptInterface;
using System;

public partial class ShootoutScriptInterface : GameScriptInterface
{
    public ShootoutScriptInterface (IGame game) : base(game) {}
#endif
    BTFS.Shootout_Extended shootout_Extended = new BTFS.Shootout_Extended();

    public void OnStartup ()
    {
        if (BTFS.BTFS_Game.CurrentMode != null) {
            BTFS.BTFS_Game.CurrentMode.StartUp(true);
            BTFS.BTFS_Game.CurrentMode.RunOnStartupPlugins(BTFS.BTFS_Game.CurrentMode.GetPlugins());
            BTFS.BTFS_Game.CurrentMode.RunOnUpdatePlugins(BTFS.BTFS_Game.CurrentMode.GetPlugins());
            Events.UpdateCallback.Start(OnUpdate, 1);
        }
    }

    public void OnUpdate (float ms) {
        OnGameover();
    }

    public void OnGameover () {
        if(Game.IsGameOver && BTFS.BTFS_Game.CurrentMode != null)
            BTFS.BTFS_Game.CurrentMode.RunOnGameoverPlugins(BTFS.BTFS_Game.CurrentMode.GetPlugins());
    }

    public void AfterStartup () {
        if (BTFS.BTFS_Game.CurrentMode != null)
            BTFS.BTFS_Game.CurrentMode.RunAfterStartupPlugins(BTFS.BTFS_Game.CurrentMode.GetPlugins());
    }

    public void OnShutdown () {
        if (BTFS.BTFS_Game.CurrentMode != null)
            BTFS.BTFS_Game.CurrentMode.RunOnShutdownPlugins(BTFS.BTFS_Game.CurrentMode.GetPlugins());
    }
}
