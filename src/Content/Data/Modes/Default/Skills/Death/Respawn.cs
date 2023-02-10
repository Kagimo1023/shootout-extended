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

        class Respawn : Skill
        {
            float _timer;

            public Respawn(Callbacks cb, float timer) : base(cb)
            {
                _timer = timer;
            }

            void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
            {
                Character _character = player.GetCharacter();

                if (args.Removed != true)
                {
                    IUser user = GetOwner().GetUser();
                    //                     ushort a = 1;
                    bool _wasAlive = false;

                    int faceDirection;

                    _character.SetUser(user);

                    _character.SetOnStartShowDialogue(false);
                    _character.SetOnStartShowDescription(false);

                    Events.UpdateCallback.Start((float x) =>
                    {
                        if (_wasAlive == false)
                        {
                            if (player.IsRemoved == false && _character.GetUser() != null)
                            {
                                Vector2 pos = player.GetWorldPosition();

                                faceDirection = player.FacingDirection;

                                _character.SetOldPlayer(player);
                                _character.GetOldPlayer().Remove();

                                _character.CreateCharacter(pos);

                                _character.GetPlayer().SetFaceDirection(faceDirection);

                                PlayerModifiers mod = player.GetModifiers();

                                _character.GetPlayer().SetModifiers(mod);

                                BTFS_Game.ShowCFTX("I AM ALIVE!", _character.GetPlayer());

                                _wasAlive = true;

                                return;
                            }

                            _wasAlive = true;

                            return;
                        }
                    }, (uint)_timer, 1);
                }
            }

            public override void Activate(IPlayer player)
            {
                SetOwner(player);
                _cb.OnPlayerDeath.AddRoute(GetOwner(), (Action<IPlayer, PlayerDeathArgs>)OnPlayerDeath);
            }
        }
    }
}
