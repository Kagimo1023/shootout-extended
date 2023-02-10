namespace BTFS
{
    namespace Modes.Plugins
    {
        public static class List
        {
            public static Plugin [] Enabled = {
                new Modes.Plugins.DamageAmount(),
                new Modes.Plugins.DeadList()
            };
        }
    }
}