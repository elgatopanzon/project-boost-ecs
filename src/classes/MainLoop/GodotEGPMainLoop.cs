/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : GodotEGPMainLoop
 * @created     : Friday May 24, 2024 23:12:54 CST
 */

namespace GodotEGP.MainLoop;

using Godot;
using GodotEGP;
using GodotEGP.Objects.Extensions;
using GodotEGP.Logging;
using GodotEGP.Service;

using GodotEGP.ECSv4;
using GodotEGP.ECSv4.Components;
using EGP.ProjectBoost.ECS.Components;
using EGP.ProjectBoost.ECS.Systems;
using EGP.ProjectBoost.Events;

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
		_ecs.RegisterComponent<GameStateComponent>();

		_ecs.RegisterSystem<InputStateSystem, OnStartupPhase>(_ecs.CreateQuery()
				.Has<InputStateComponent>()
				.Build()
			);
		_ecs.RegisterSystem<PlayerMovementSystem, OnUpdatePhase>(_ecs.CreateQuery()
				.Has<PlayerNodeComponent>()
				.Build()
			);
		_ecs.RegisterSystem<CollisionSystem, PreUpdatePhase>(_ecs.CreateQuery()
				.Has<PlayerNodeComponent>()
				.Build()
			);
		_ecs.RegisterSystem<GameSystem, FinalPhase>(_ecs.CreateQuery()
				.Has<GameStateComponent>()
				.Build()
			);

		// add input state component as singleton component
		_ecs.Set<InputStateComponent>(Entity.CreateFrom(InputStateComponent.Id), new());

		// add game state component as singleton component
		_ecs.Set<GameStateComponent>(Entity.CreateFrom(GameStateComponent.Id), new());
	}

	partial void _On_Ready()
	{
		// deregister default inputmanager service
		ServiceRegistry.Deregister<InputManager>();

		// create audio nodes for SFX
		var audioResources = ServiceRegistry.Get<ResourceManager>().GetResources<AudioStream>();

		AudioStreamPlayer sfxCrash = new();
		sfxCrash.SetMeta("id", "sfx_crash");
		sfxCrash.Stream = audioResources["sfx_crash"].Value;

		AudioStreamPlayer sfxGoal = new();
		sfxGoal.SetMeta("id", "sfx_goal");
		sfxGoal.Stream = audioResources["sfx_goal"].Value;

		ServiceRegistry.Instance.GetTree().Root.AddChild(sfxCrash);
		ServiceRegistry.Instance.GetTree().Root.AddChild(sfxGoal);

		// subscribe to game state events to play audio
		this.Subscribe<GameStateCrashed>((e) => {
			ServiceRegistry.Get<NodeManager>().GetNode<AudioStreamPlayer>("sfx_crash").Play();
		});
		this.Subscribe<GameStateGoal>((e) => {
			ServiceRegistry.Get<NodeManager>().GetNode<AudioStreamPlayer>("sfx_goal").Play();
		});
	}
}
#endif
