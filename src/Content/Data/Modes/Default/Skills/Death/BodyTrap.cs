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
    using System;
    using SFDGameScriptInterface;

    namespace Modes.Data.Skills
    {
        class BodyTrap : Skill
        {
            public BodyTrap(Callbacks cb) : base(cb) { }

            //Bad code. Needs improve.
            void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
            {
                ushort a = 0;

                Events.UpdateCallback.Start((float e) =>
                {
                    if (player.IsRemoved == false)
                    {
                        foreach (IObject obj in GameScriptInterface.Game.GetObjectsByArea(player.GetAABB(), PhysicsLayer.Active))
                        {
                            if (obj.UniqueID != player.UniqueID)
                            {
                                if (obj.GetBodyType() == BodyType.Dynamic)
                                {
                                    BTFS_Game.Game.PlayEffect("STM", player.GetWorldPosition(), 0f, 0f, true);
                                    
                                    obj.TrackAsMissile(true);

                                    switch (obj.GetFaceDirection())
                                    {
                                        case 1:
                                            obj.SetLinearVelocity(new Vector2(80f, 8f));
                                            break;
                                        case -1:
                                            obj.SetLinearVelocity(new Vector2(-80f, 3f));
                                            break;
                                    }

                                    if (obj is IPlayer)
                                    {
                                        IPlayer tmp_p = (IPlayer) obj;
                                        tmp_p.SetInputEnabled(false);
                                        tmp_p.AddCommand(new PlayerCommand(PlayerCommandType.Fall));
                                        Events.UpdateCallback.Start((x) => tmp_p.SetInputEnabled(true), 0u, 1);
                                        GameScriptInterface.Game.PlaySound("Wilhelm", new Vector2(), 100);
                                        GameScriptInterface.Game.PlaySound("CartoonScream", new Vector2(), 100);
                                    }

                                    BTFS_Game.Game.PlayEffect("TR_F", obj.GetWorldPosition(), 0f, 0f, true);
                                }

                            }
                        }
                        //GameScriptInterface.Game.WriteToConsole ("Body trap thread is work");
                    }
                    else
                    {
                        a = 1;
                        return;
                    }
                }, 30, a);
            }

            public override void Activate(IPlayer player)
            {
                SetOwner(player);
                _cb.OnPlayerDeath.AddRoute(GetOwner(), (Action<IPlayer, PlayerDeathArgs>)OnPlayerDeath);
            }
        }
    }
}
