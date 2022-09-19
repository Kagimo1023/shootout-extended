namespace Shootout
{
    using SFDGameScriptInterface;

    static class PlayerExtensions
    {
        public static void Apply (this IPlayer player, Effect effect) { effect.Activate (player);}
        
        public static Character GetCharacter (this IPlayer player)
        {
            Character temp = null;

            foreach (Character tmp in Modes.Characters.List.Playable)
            {
                if (tmp != null && tmp.GetPlayer () != null)
                {
                    if (tmp.GetPlayer () == player)
                    {
                        temp = tmp;
                    }
                }
            }

            return temp;
        }
    }
}
