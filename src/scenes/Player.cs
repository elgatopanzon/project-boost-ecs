/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : Player
 * @created     : Friday May 24, 2024 23:00:52 CST
 */

namespace EGP.ProjectBoost.scenes;

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

public partial class Player : RigidBody3D
{
	private ECS _ecs;

	public Player()
	{
		_ecs = ServiceRegistry.Get<ECS>();
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
}
