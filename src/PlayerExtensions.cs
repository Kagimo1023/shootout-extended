namespace BTFS
{
    using SFDGameScriptInterface;
    using System.Collections.Generic;

    static class PlayerExtensions
    {
        public static void Apply(this IPlayer player, Skill skill) { skill.Activate(player); }

        public static Character GetCharacter(this IPlayer player)
        {
            Character temp = null;

            foreach (Character tmp in Modes.Characters.List.Playable)
            {
                if (tmp != null && tmp.GetPlayer() != null)
                {
                    if (tmp.GetPlayer() == player)
                    {
                        temp = tmp;
                    }
                }
            }

            return temp;
        }
    }

    public class CharacterTeam
    {
        // public string Name { get; }
        
        IObjectAlterCollisionTile objalt;
        
        public CharacterTeam (string Name, params Character [] characters)
        {
            CreateObjAlt();
            
            // this.Name = Name;

            foreach (Character character in characters)
                AddCharacterToTeam(character);

            // if (objalt.GetTargetObjects().Length != 0)
                
        }

        public void AddCharacterToTeam (Character character)
        {
            objalt.AddTargetObject(character.GetPlayer());
        }

        public void UpdateTeam ()
        {
            Events.UpdateCallback.Start((x) => objalt.SetDisablePlayerMelee(false), 500, 0);
            Events.UpdateCallback.Start((x) => objalt.SetDisablePlayerMelee(true), 500, 0);
        }
        
        public void CreateObjAlt ()
        {
            objalt = (IObjectAlterCollisionTile) GameScriptInterface.Game.CreateObject("AlterCollisionTile");
        }
    }
}