/* Copyright (C) 2022 Ivan Kozlov.
 * Copyright (C) 2022 Daniil Alexeev.

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
        // class Compression : Skill
        // {
            // private readonly string[] _description =
            // {
            //     "On damage taken player is smaller on 0.1%"
            // };

            // private bool _show_description;

            // float _factor;

            // public Compression(Callbacks cb, float factor = 1, bool show_description = true) : base(cb)
            // {
            //     Name = "Compression";
            //     _factor = factor;
            //     _show_description = show_description;
            // }

            // public void ShowDescription(UserMessageCallbackArgs args)
            // {
            //     GameScriptInterface.Game.ShowChatMessage("Passive Skill Name: " + Name, Color.Green, args.User.UserIdentifier);
            //     foreach (string str in _description)
            //         GameScriptInterface.Game.ShowChatMessage(str, Color.Yellow, args.User.UserIdentifier);
            // }

            // void OnPlayerDamage(IPlayer player, PlayerDamageArgs args)
            // {
            //     PlayerModifiers modifiers = player.GetModifiers();
            //     modifiers.SizeModifier -= (float)Global.GetPercent((int)modifiers.MaxHealth, (int)args.Damage) / 100;
            //     player.SetModifiers(modifiers);
            // }

            // public override void Activate(IPlayer player)
            // {
            //     _cb.OnPlayerDamage.AddRoute(player, (Action<IPlayer, PlayerDamageArgs>)OnPlayerDamage);
            // }
        // }
    }
}
