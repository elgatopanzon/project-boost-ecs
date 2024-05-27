/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : InputStateSystem
 * @created     : Saturday May 25, 2024 15:25:16 CST
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

public class InputStateSystem : ISystem
{
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		// get the singleton component data for the input state
		ref InputStateComponent inputState = ref core.Get<InputStateComponent>(Entity.CreateFrom(InputStateComponent.Id));

		// update state
		inputState.ThrustPressed = Input.IsActionPressed("boost");
		inputState.LeftPressed = Input.IsActionPressed("rotate_left");
		inputState.RightPressed = Input.IsActionPressed("rotate_right");
	}
}
