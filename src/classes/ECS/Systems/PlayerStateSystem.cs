/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : PlayerStateSystem
 * @created     : Monday May 27, 2024 12:40:57 CST
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

public class PlayerStateSystem : ISystem
{
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		// get the singleton component data for the input state
		ref InputStateComponent inputState = ref core.Get<InputStateComponent>(Entity.CreateFrom(InputStateComponent.Id));
		ref PlayerStateComponent playerState = ref query.Results.GetComponent<PlayerStateComponent>(entity);
		ref PlayerNodeComponent playerNode = ref query.Results.GetComponent<PlayerNodeComponent>(entity);

		Player player = core.GetObject<Player>(playerNode.PlayerNodeEntity);

		// update player state from input state
		playerState.MainThrusterState = (inputState.ThrustPressed) ? PlayerStateComponent.ThrusterState.On :  PlayerStateComponent.ThrusterState.Off;
		playerState.LeftThrusterState = (inputState.RightPressed) ? PlayerStateComponent.ThrusterState.On :  PlayerStateComponent.ThrusterState.Off;
		playerState.RightThrusterState = (inputState.LeftPressed) ? PlayerStateComponent.ThrusterState.On :  PlayerStateComponent.ThrusterState.Off;

		// if we're going to destroy the player, let's do it before reloading
		// the scene
		if (playerState.Crashed || playerState.Goal)
		{
			player.SetProcess(false); // disable processing of the player
			core.Destroy(entity);

			LoggerManager.LogDebug("Destroying player entity");
		}
	}
}
