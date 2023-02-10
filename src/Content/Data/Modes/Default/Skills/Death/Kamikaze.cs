/* Copyright (C) 2022 Daniil Alexeev.

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
    namespace Modes.Data.Skills
    {
        using System;
        using SFDGameScriptInterface;

        class Kamikaze : Skill
        {
            bool IsExplode = false;
            string _str;
            uint _delay;

            public Kamikaze(Callbacks cb, string str, uint delay = 10) : base(cb)
            {
                _str = str;
                _delay = delay;
            }

            void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
            {
                if (!IsExplode)
                    Events.UpdateCallback.Start((float x) =>
                    {
                        BTFS_Game.Game.PlayEffect("CFTXT", GetOwner().GetWorldPosition(), _str);
                        IObjectExplosionTrigger explosion = (IObjectExplosionTrigger)GameScriptInterface.Game.CreateObject("ExplosionTrigger", GetOwner().GetWorldPosition());
                        explosion.Trigger();
                        explosion.SetEnabled(true);
                        explosion.Remove();

                        IsExplode = true;
                    }, _delay, 1);
            }

            public override void Activate(IPlayer player)
            {
                SetOwner(player);
                _cb.OnPlayerDeath.AddRoute(player, (Action<IPlayer, PlayerDeathArgs>)OnPlayerDeath);
            }
        }
    }
}
