/* Copyright (C) 2022 Daniil Alexeev

   This file is part of Shootout.

   Shootout is free software; you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation; either version 3, or (at your option)
   any later version.

   Shootout is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with Shootout; if not, see <https://www.gnu.org/licenses>.

   Additional permission under GNU GPL version 3 section 7

   If you modify Shootout, or any covered work, by linking or combining
   it with MythoLogic Interactive's Superfighters Deluxe, the licensors
   of this Program grant you additional permission to convey the resulting
   work.  */

namespace BTFS
{
    using SFDGameScriptInterface;
    using System.Collections.Generic;
    using System;

    namespace Modes.Plugins
    {
        public partial class DeadList : Plugin
        {
            // This plugin based on old Shootout feature (last version).
            bool StopDeanonEvent = false;

            Dictionary <IUser, string> p_RealNames = new Dictionary<IUser ,string>();

            List <string> DeanonKilledPlayers = new List <string>();
            List <string> p_WasSpawned = new List<string>();

            public DeadList ()
            {
                this.Enable = true;
                this.Name = "Dead List";
                SetDescription ("Output list of all dead players! Who dead first, lose! And he has last position in top Dead List Just try to win! And not dead!");
            }

            public override void OnStartup ()
            {
                Events.PlayerDeathCallback.Start(OnPlayerDeath);

                foreach (Character chr in Characters.List.Playable)
                {
                    if (chr.WasSpawned () == true)
                        p_RealNames.Add (chr.GetUser (), chr.GetUser().Name);
                }
            }

            public override void OnGameover ()
            {
                DeanonEventFunction();
            }

            public void DeanonEventFunction()
            {
                int ndp = 0;

                foreach(IPlayer plr in Game.GetPlayers())
                {
                    if (plr.GetCharacter() != null && plr != null && plr.IsDead != true)
                    {
                        DeanonKilledPlayers.Insert(0, p_RealNames[plr.GetUser()] + "(" + plr.GetCharacter().GetName() + ")");
                        ndp++;
                    }
                }

                if (ndp > 1)
                    StopDeanonEvent = true;

                if (StopDeanonEvent == false)
                {
                    Game.ShowChatMessage("Top Dead List".ToUpper(), Color.Yellow);

                    for (int i = 0; i < DeanonKilledPlayers.Count; ++i)
                    {
                        if(i != 0) Game.ShowChatMessage((i) + ". " + DeanonKilledPlayers[i], Color.Yellow);
                    }

                    StopDeanonEvent = true;
                }
            }

            public void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
            {
                string tmp;

                if (args.Killed) {
                    if(player.GetCharacter() != null && player != null && player.GetUser() != null) {
                        tmp = p_RealNames[player.GetUser()] + "(" + player.GetCharacter().GetName() + ")";

                        if (DeanonKilledPlayers.Contains(tmp) == false)
                            DeanonKilledPlayers.Insert(0, tmp);

                        tmp = null;
                    }
                }
                if (args.Removed) {
                    if(player.GetUser() != null) {
                        tmp = p_RealNames[player.GetUser()] + "(" + player.GetCharacter().GetName() + ")";

                        if (DeanonKilledPlayers.Contains(tmp) == false)
                            DeanonKilledPlayers.Insert(0, tmp);

                        tmp = null;
                    }
                }
            }
        }
    }
}
