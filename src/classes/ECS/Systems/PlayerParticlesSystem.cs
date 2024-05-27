/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : PlayerParticlesSystem
 * @created     : Monday May 27, 2024 13:49:55 CST
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

public class PlayerParticlesSystem : ISystem
{
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		ref PlayerStateComponent playerState = ref query.Results.GetComponent<PlayerStateComponent>(entity);
		ref PlayerNodeComponent playerNode = ref query.Results.GetComponent<PlayerNodeComponent>(entity);

		Player player = core.GetObject<Player>(playerNode.PlayerNodeEntity);

		// main thruster
		player.BoosterParticles.Emitting = false;
		player.LeftBoosterParticles.Emitting = false;
		player.RightBoosterParticles.Emitting = false;

		if (playerState.MainThrusterState == PlayerStateComponent.ThrusterState.On)
		{
			player.BoosterParticles.Emitting = true;
		}

		// left and right thrusters
		if (playerState.RightThrusterState == PlayerStateComponent.ThrusterState.On)
		{
			player.RightBoosterParticles.Emitting = true;
		}
		if (playerState.LeftThrusterState == PlayerStateComponent.ThrusterState.On)
		{
			player.LeftBoosterParticles.Emitting = true;
		}
	}
}
