/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : PlayerAudioSystem
 * @created     : Sunday May 26, 2024 23:50:40 CST
 */

namespace EGP.ProjectBoost.ECS.Systems;

using System;

using Godot;
using GodotEGP.Objects.Extensions;
using GodotEGP.Logging;
using GodotEGP.Service;
using GodotEGP.Event.Events;
using GodotEGP.Config;
using GodotEGP.Collections;

using GodotEGP.ECSv4;
using GodotEGP.ECSv4.Components;
using GodotEGP.ECSv4.Systems;
using GodotEGP.ECSv4.Queries;
using EGP.ProjectBoost.ECS.Components;
using EGP.ProjectBoost.Scenes;

public class PlayerAudioSystem : ISystem
{
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		ref PlayerStateComponent playerState = ref query.Results.GetComponent<PlayerStateComponent>(entity);
		ref PlayerNodeComponent playerNode = ref query.Results.GetComponent<PlayerNodeComponent>(entity);

		Player player = core.GetObject<Player>(playerNode.PlayerNodeEntity);

		// thrust triggers sfx
		if (
				(playerState.MainThrusterState == PlayerStateComponent.ThrusterState.On)
				|| (playerState.LeftThrusterState == PlayerStateComponent.ThrusterState.On)
				|| (playerState.RightThrusterState == PlayerStateComponent.ThrusterState.On)
				)
		{
			if (!player.RocketAudio.Playing)
			{
				player.RocketAudio.Play();
			}
		}
		else
		{
			if (player.RocketAudio.Playing)
			{
				player.RocketAudio.Stop();
			}
		}
	}
}
