/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : PlayerMovementSystem
 * @created     : Saturday May 25, 2024 15:33:59 CST
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

public struct PlayerMovementSystem : ISystem
{
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		ref PlayerStateComponent playerState = ref query.Results.GetComponent<PlayerStateComponent>(entity);
		ref PlayerNodeComponent playerNode = ref query.Results.GetComponent<PlayerNodeComponent>(entity);

		// get the Node3D object and process position
		Player player = core.GetObject<Player>(playerNode.PlayerNodeEntity);

		// main thruster
		if (playerState.MainThrusterState == PlayerStateComponent.ThrusterState.On)
		{
			player.ApplyCentralForce(player.Basis.Y * (float) deltaTime * player.ThrustPower);
		}

		// left and right thrusters
		Vector3 torque = Vector3.Zero;
		if (playerState.RightThrusterState == PlayerStateComponent.ThrusterState.On)
		{
			torque += player.Torque;
		}
		if (playerState.LeftThrusterState == PlayerStateComponent.ThrusterState.On)
		{
			torque += -player.Torque;
		}

		player.ApplyTorque(torque * (float) deltaTime);
	}
}
