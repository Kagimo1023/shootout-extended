namespace Shootout
{
    using SFDGameScriptInterface;

    namespace Modes
    {
        public class Template : Mode
        {
            public Template(string Name, bool Enable) : base(Name, Enable)
            {
                SetName("Template Mode");
            }

            public override void StartUp(bool Enable)
            {
                if (Enable)
                {
                    BTFS_Callbacks.Data.InitializeCallbacks();
                }
            }

            public override bool CheckSettings(object[] Settings)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
