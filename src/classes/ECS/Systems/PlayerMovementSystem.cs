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

public struct PlayerMovementSystem : ISystem
{
	private float _thrustPower = 1000f;
	private float _rotationPower = 100f;
	private Vector3 _torque;

	public PlayerMovementSystem()
	{
		_torque = new Vector3(0f, 0f, _rotationPower);
	}

	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		ref InputStateComponent inputState = ref core.Get<InputStateComponent>(Entity.CreateFrom(InputStateComponent.Id));
		ref PlayerNodeComponent playerNode = ref query.Results.GetComponent<PlayerNodeComponent>(entity);

		// get the Node3D object and process position
		RigidBody3D player = core.GetObject<RigidBody3D>(playerNode.PlayerNodeEntity);

		// thrust
		if (inputState.ThrustPressed)
		{
			player.ApplyCentralForce(player.Basis.Y * (float) deltaTime * _thrustPower);
		}

		// left and right rotation
		if (inputState.LeftPressed)
		{
			player.ApplyTorque((_torque * (float) deltaTime));
		}
		else if (inputState.RightPressed)
		{
			player.ApplyTorque(-(_torque * (float) deltaTime));
		}
	}
}
