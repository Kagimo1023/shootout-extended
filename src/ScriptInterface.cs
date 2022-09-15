#if standalone
using SFDGameScriptInterface;

public partial class ShootoutScriptInterface : GameScriptInterface
{
    public ShootoutScriptInterface (IGame game) : base(game) {}
#endif
    Shootout.Shootout shootout;

    public void OnStartup ()
    {
        shootout = new Shootout.Shootout (Game);
        shootout.Start ();
        shootout.RunOnStartupPlugins(Shootout.Shootout.GetPluginList());
        shootout.RunOnUpdatePlugins(Shootout.Shootout.GetPluginList());
        Events.UpdateCallback.Start(OnUpdate, 10);
    }

    public void OnUpdate (float ms) => OnGameover();

    public void OnGameover () {
        if(Game.IsGameOver) shootout.RunOnGameoverPlugins(Shootout.Shootout.GetPluginList());
    }

    public void AfterStartup () => shootout.RunAfterStartupPlugins (Shootout.Shootout.GetPluginList());
    public void OnShutdown () => shootout.RunOnShutdownPlugins(Shootout.Shootout.GetPluginList());
}
