namespace Shootout
{
    using SFDGameScriptInterface;
    
    namespace Modes.BTFS_Callbacks
    {
        public static class Data
        {
            public static Callbacks cbs;

            public static void InitializeCallbacks ()
            {
                cbs = new Callbacks();
                
                cbs.OnPlayerDamage = new PlayerDamageCallback ();
                cbs.OnPlayerDeath = new PlayerDeathCallback ();
                cbs.OnPlayerKeyInput = new PlayerKeyInputCallback ();
                cbs.OnObjectCreated = new UpdateCallback ();

                StartCallbacks();
            }

            public static void StartCallbacks()
            {
                Events.PlayerDamageCallback.Start (((PlayerDamageCallback)cbs.OnPlayerDamage).Run);
                Events.PlayerDeathCallback.Start (((PlayerDeathCallback)cbs.OnPlayerDeath).Run);
                Events.PlayerKeyInputCallback.Start (((PlayerKeyInputCallback)cbs.OnPlayerKeyInput).Run);
                Events.UpdateCallback.Start (((UpdateCallback)cbs.OnObjectCreated).Run);
            }
        }
    }
}
