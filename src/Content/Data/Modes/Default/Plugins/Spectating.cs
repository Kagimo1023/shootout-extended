/* Copyright (C) 2022 Daniil Alexeev
 *
 T h*is file is part of Shootout.

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
		public class Spectating : Plugin
		{
			List <IPlayer> Spectators = new List <IPlayer> ();
			List <IPlayer> FollowMode = new List <IPlayer> ();
			List <IPlayer> CanFollow = new List <IPlayer> ();

			public Spectating ()
			{
				this.Enable = true;
				this.Name = "Spectating";
				SetDescription ("Can move player camera in spectator mode!");
                SetSleepOnUpdate(200); // Set interval on update
                SetCyclesOnUpdate(0); // infinite on update!
			}

			public override void OnStartup()
			{
				IObject obj = Game.CreateObject("InvisibleBlock", new Vector2(Game.GetCameraMaxArea().BottomLeft.X, Game.GetCameraMaxArea().BottomLeft.Y + 100000));

				obj.SetSizeFactor(new Point (1000, 0));

				foreach (IPlayer player in Game.GetPlayers())
				{
					player.SetCameraSecondaryFocusMode(CameraFocusMode.Ignore);
				}

				Events.PlayerDeathCallback.Start(OnPlayerDeath);
				Events.PlayerKeyInputCallback.Start(Input);
			}

			public override void OnUpdate (float ms)
			{
				foreach (IPlayer player in Game.GetPlayers())
				{
					if (!player.IsDead && !Spectators.Contains(player) && !CanFollow.Contains(player) && player.GetUser() != null)
					{
						CanFollow.Add(player);
					}
				}
			}

			public void Input(IPlayer p, VirtualKeyInfo[] keyEvents)
			{
				for(int i = 0; i < keyEvents.Length; i ++)
					if(keyEvents[i].Event == VirtualKeyEvent.Pressed && Spectators.Contains(p))
					{
						switch(keyEvents[i].Key)
						{
							case VirtualKey.AIM_RUN_RIGHT:
								if(p.GetWorldPosition().X <= Game.GetCameraMaxArea().BottomRight.X)
								{
									if (!FollowMode.Contains(p))
									{
										p.SetWorldPosition(p.GetWorldPosition() + new Vector2(20,0));
										if(p.KeyPressed(VirtualKey.SPRINT))p.SetWorldPosition(p.GetWorldPosition() + new Vector2(20,0));
									}
								}
								break;
							case VirtualKey.AIM_RUN_LEFT:
								if(p.GetWorldPosition().X >= Game.GetCameraMaxArea().BottomLeft.X)
								{
									if (!FollowMode.Contains(p))
									{
										p.SetWorldPosition(p.GetWorldPosition() + new Vector2(-20,0));
										if(p.KeyPressed(VirtualKey.SPRINT))p.SetWorldPosition(p.GetWorldPosition() + new Vector2(-20,0));
									}
								}
								break;
							case VirtualKey.ACTIVATE:
								Game.ShowChatMessage("Follow mode work in progress!", Color.Magenta, p.UserIdentifier);
								break;
						}
					}
			}

			public void OnPlayerDeath (IPlayer player, PlayerDeathArgs args)
			{
				if (args.Killed && !player.IsBot && player.GetUser() != null && !Spectators.Contains(player))
				{
					CanFollow.Remove(player);

					string Name = player.GetUser().Name;

					IUser user = player.GetUser();
					Vector2 p_pos = player.GetWorldPosition();

					IPlayer spectator = Game.CreatePlayer(new Vector2(p_pos.X, Game.GetCameraMaxArea().BottomLeft.Y + 100010));

					spectator.SetInputMode(PlayerInputMode.ReadOnly);
					spectator.SetCameraSecondaryFocusMode(CameraFocusMode.Ignore);
					spectator.SetNametagVisible(false);
					spectator.SetBotName(Name);
					spectator.SetUser(user);
					Spectators.Add(spectator);
					Game.ShowChatMessage(string.Format("Welcome to spectator mode! You can manually scroll camera when you are dead! Just press {0}, {1} for change camera position. For follow player press {2}!", VirtualKey.AIM_RUN_LEFT, VirtualKey.AIM_RUN_RIGHT, VirtualKey.ACTIVATE), Color.Magenta, spectator.UserIdentifier);
				}
			}
		}
	}
}
