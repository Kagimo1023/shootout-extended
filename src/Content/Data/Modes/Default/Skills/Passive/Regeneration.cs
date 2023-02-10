/* Copyright (C) 2022 Daniil Alexeev.
 * Idea by: Opthafuth

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
    using System;

    namespace Modes.Data.Skills
    {
        class Regeneration : Skill
        {
            private float regenInterval;
            private float totalElapsed = 0;
            private float regenHealth;

            private string _game_effect;

            public Regeneration(Callbacks cb, float timer, float regenHealth, string game_effect = "") : base(cb)
            {
                Name = "Regeneration";
                regenInterval = timer;
                this.regenHealth = regenHealth;
                _game_effect = game_effect;
            }

            void UpdatePlayer(IPlayer player, float e)
            {
                totalElapsed += e;

                if (totalElapsed > regenInterval)
                {
                    PlayerModifiers mod = player.GetModifiers();
                    mod.CurrentHealth += regenHealth;
                    SetModifiers(mod);
                    BTFS_Game.ShowGameEffect(_game_effect, player);
                    totalElapsed = 0;
                }
            }

            public override void Activate(IPlayer player)
            {
                SetOwner(player);

                ((UpdateCallback)_cb.OnObjectCreated).HandlePlayer(player);

                //Events.UpdateCallback.Start (((UpdateCallback)_cb.OnPlayerUpdate).Run);
                _cb.OnObjectCreated.AddRoute(player, (Action<IPlayer, float>)UpdatePlayer);
                //_cb.OnPlayerCreated.AddRoute (GetOwner (), (Action <IPlayer, IObject[]> ) UpdatePlayer);
            }
        }
    }
}
