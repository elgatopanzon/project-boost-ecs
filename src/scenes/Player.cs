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
using System.Collections.Generic;

public partial class Player : RigidBody3D
{
	private ECS _ecs;

	public Vector3 Torque { get; set; }

	[Export]
	public float ThrustPower { get; set; } = 1000f;
	[Export]
	public float RotationPower { get; set; } = 100f;

	private EntityHandle e;
	private Entity _nodeEntity;

	public Queue<Node> Collisions { get; set; }

	public AudioStreamPlayer3D RocketAudio { get; set; }
	public GpuParticles3D BoosterParticles { get; set; }
	public GpuParticles3D LeftBoosterParticles { get; set; }
	public GpuParticles3D RightBoosterParticles { get; set; }
	public GpuParticles3D CrashParticles { get; set; }
	public GpuParticles3D GoalParticles { get; set; }

	public Player()
	{
		_ecs = ServiceRegistry.Get<ECS>();
		Torque = new Vector3(0f, 0f, RotationPower);

		Collisions = new();

		this.SubscribeSignal(SignalName.BodyEntered, true, _On_BodyEntered, isHighPriority:true);
	}

	public override void _EnterTree()
	{
		// create an entity for this player
		e = _ecs.Create();

		_nodeEntity = _ecs.RegisterObject(this);

		// add the player movement component
		e.Set<PlayerNodeComponent>(new() {
			PlayerNodeEntity = _nodeEntity,
			});
		// add player state component
		e.Set<PlayerStateComponent>(new());

		LoggerManager.LogWarning("Player nodeId", "", "nodeEntity", _nodeEntity);
		LoggerManager.LogWarning("Player enter tree!");
	}

	public override void _ExitTree()
	{
		_ecs.DestroyObject(_nodeEntity);

		LoggerManager.LogWarning("Player leave tree!");
	}

	public override void _Ready()
	{
		// set rocket audio node
		RocketAudio = "rocket_audio".Node<AudioStreamPlayer3D>();
		BoosterParticles = "booster_particles".Node<GpuParticles3D>();
		LeftBoosterParticles = "booster_particles_left".Node<GpuParticles3D>();
		RightBoosterParticles = "booster_particles_right".Node<GpuParticles3D>();
		CrashParticles = "crash_particles".Node<GpuParticles3D>();
		GoalParticles = "goal_particles".Node<GpuParticles3D>();

		LoggerManager.LogWarning("Player ready!");
	}

	public void _On_BodyEntered(GodotSignal e)
	{
		e.TryGetParam<Node3D>(0, out Node3D node);

		Collisions.Enqueue(node);
	}
}
