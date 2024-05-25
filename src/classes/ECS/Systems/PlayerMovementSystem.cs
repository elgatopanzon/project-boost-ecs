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
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		ref InputStateComponent inputState = ref core.Get<InputStateComponent>(Entity.CreateFrom(InputStateComponent.Id));
		ref PlayerNodeComponent playerNode = ref query.Results.GetComponent<PlayerNodeComponent>(entity);

		// get the Node3D object and process position
		Node3D node = core.GetObject<Node3D>(playerNode.PlayerNodeEntity);

		// thrust
		if (inputState.ThrustPressed)
		{
			Vector3 pos = node.Position;
			node.Position = pos with {
				Y = pos.Y + (float) deltaTime,
			};
		}

		// left and right rotation
		if (inputState.LeftPressed)
		{
			node.RotateZ((float) deltaTime);
		}
		else if (inputState.RightPressed)
		{
			node.RotateZ((float) -deltaTime);
		}
	}
}
