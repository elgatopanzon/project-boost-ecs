/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : GodotEGPMainLoop
 * @created     : Friday May 24, 2024 23:12:54 CST
 */

namespace GodotEGP.MainLoop;

using Godot;
using GodotEGP;
using GodotEGP.Logging;
using GodotEGP.Service;

using GodotEGP.ECSv4;
using GodotEGP.ECSv4.Components;
using EGP.ProjectBoost.ECS.Components;
using EGP.ProjectBoost.ECS.Systems;

#if GODOT
public partial class GodotEGPMainLoop : SceneTree
{
	private ECS _ecs;

	partial void _On_Initialize()
	{
		LoggerManager.LogInfo("Project boost!");

		// setup ECS stuff
		_ecs = ServiceRegistry.Get<ECS>();

		// register components and systems
		_ecs.RegisterComponent<PlayerNodeComponent>();
		_ecs.RegisterComponent<InputStateComponent>();

		_ecs.RegisterSystem<InputStateSystem, OnUpdatePhase>(_ecs.CreateQuery()
				.Has<InputStateComponent>()
				.Build()
			);
		_ecs.RegisterSystem<PlayerMovementSystem, FinalPhase>(_ecs.CreateQuery()
				.Has<PlayerNodeComponent>()
				.Build()
			);

		// add input state component as singleton component
		_ecs.Set<InputStateComponent>(Entity.CreateFrom(InputStateComponent.Id), new());
	}

	partial void _On_Ready()
	{
		// deregister default inputmanager service
		ServiceRegistry.Deregister<InputManager>();
	}
}
#endif
