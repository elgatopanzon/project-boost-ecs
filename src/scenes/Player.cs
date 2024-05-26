/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : Player
 * @created     : Friday May 24, 2024 23:00:52 CST
 */

namespace EGP.ProjectBoost.Scenes;

using System;

using Godot;
using GodotEGP;
using GodotEGP.Objects.Extensions;
using GodotEGP.Logging;
using GodotEGP.Service;
using GodotEGP.Event.Events;
using GodotEGP.Config;
using GodotEGP.Collections;

using GodotEGP.ECSv4;
using EGP.ProjectBoost.ECS.Components;
using System.Linq;

public partial class Player : RigidBody3D
{
	private ECS _ecs;

	public Vector3 Torque { get; set; }

	[Export]
	public float ThrustPower { get; set; } = 1000f;
	[Export]
	public float RotationPower { get; set; } = 100f;

	public Player()
	{
		_ecs = ServiceRegistry.Get<ECS>();
		Torque = new Vector3(0f, 0f, RotationPower);

		this.SubscribeSignal(SignalName.BodyEntered, true, _On_BodyEntered, isHighPriority:true);
	}

	public override void _Ready()
	{
		// create an entity for this player
		EntityHandle e = _ecs.Create();

		// add the player movement component
		e.Set<PlayerNodeComponent>(new() {
			PlayerNodeEntity = _ecs.RegisterObject(this),
			});

		LoggerManager.LogWarning("Player ready!");
	}

	public void _On_BodyEntered(GodotSignal e)
	{
		e.TryGetParam<Node3D>(0, out Node3D node);
		LoggerManager.LogDebug("Body entered", "", "name", node.Name.ToString());

		if (node.IsInGroup("Goal"))
		{
			LoggerManager.LogDebug("Win!");
		}
		else if (node.IsInGroup("Hazard"))
		{
			LoggerManager.LogDebug("Ouch!");
		}
	}
}
