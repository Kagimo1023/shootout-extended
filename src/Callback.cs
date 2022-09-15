namespace Shootout
{
    using SFDGameScriptInterface;
    using System;
    using System.Collections.Generic;
    
    public class Callbacks
    {
        public Callback<IPlayer> OnPlayerDamage;
        public Callback<IPlayer> OnPlayerDeath;
        public Callback<IPlayer> OnPlayerKeyInput;
        public Callback<IPlayer> OnObjectCreated;
    }
    
    public class Callback<T>
        where T : IObject
    {
        protected Dictionary<int, List<Delegate>> _routes = new Dictionary<int, List<Delegate>> ();
        //protected Dictionary<int, List<float>> _routes_up = new Dictionary<int, List<float>> ();

        public void AddRoute (T match, MulticastDelegate target)
        {
            if (!_routes.ContainsKey (match.UniqueID))
                _routes[match.UniqueID] = new List<Delegate> ();
            _routes[match.UniqueID].Add (target);
        }

        public void RemoveRoute (T match, MulticastDelegate target)
        {
            if (!_routes.ContainsKey (match.UniqueID))
                _routes[match.UniqueID].Remove (target);
        }
    }
    
    public class PlayerDamageCallback : Callback<IPlayer>
    {
        public void Run (IPlayer player, PlayerDamageArgs args)
        {
            List<Delegate> route;
            if (_routes.TryGetValue (player.UniqueID, out route))
                foreach (Action<IPlayer, PlayerDamageArgs> m in route)
                    m (player, args);
        }

        public void Stop (IPlayer player, PlayerDamageArgs args)
        {
            List<Delegate> route;
            if (_routes.TryGetValue (player.UniqueID, out route))
                foreach (Action<IPlayer, PlayerDamageArgs> m in route)
                    m (player, args);
        }
    }

    public class UpdateCallback : Callback<IPlayer>
    {
        List <IPlayer> UpdateObjects = new List <IPlayer> ();

        public void Run (float args)
        {
            List<Delegate> route;

            foreach (IPlayer player in UpdateObjects)
            {
                if (player != null)
                {
                    if (_routes.TryGetValue (player.UniqueID, out route))
                    {
                        foreach (Action <IPlayer, float> m in route)
                        {
                            m (player, args);
                        }
                    }
                }
                else
                {
                    GameScriptInterface.Game.WriteToConsole ("Failed to update ", player.Name);
                }
            }
        }

        public void HandlePlayer (IPlayer player)
        {
            foreach (IPlayer plr in UpdateObjects)
            {
                if (plr == player)
                {
                    return;
                }
            }

            if (UpdateObjects.Contains (player) == false)
            {
                //GameScriptInterface.Game.WriteToConsole (player.Name + " was add to Update cycle!");
                UpdateObjects.Add (player);
            }
            else
            {
                return;
            }
        }
    }

    public class PlayerDeathCallback : Callback<IPlayer>
    {
        public void Run (IPlayer player, PlayerDeathArgs args)
        {
            List<Delegate> route;
            if (_routes.TryGetValue (player.UniqueID, out route))
                foreach (Action<IPlayer, PlayerDeathArgs> m in route)
                    m (player, args);
        }
    }

    public class PlayerKeyInputCallback : Callback<IPlayer>
    {
        public void Run (IPlayer player, VirtualKeyInfo[] args)
        {
            List<Delegate> route;
            if (_routes.TryGetValue (player.UniqueID, out route))
                foreach (Action<IPlayer, VirtualKeyInfo[]> m in route)
                    m (player, args);
        }
    }
}
