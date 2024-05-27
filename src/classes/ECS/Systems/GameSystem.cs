/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : GameSystem
 * @created     : Sunday May 26, 2024 00:20:54 CST
 */

namespace EGP.ProjectBoost.ECS.Systems;

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
using GodotEGP.ECSv4.Components;
using GodotEGP.ECSv4.Systems;
using GodotEGP.ECSv4.Queries;
using EGP.ProjectBoost.ECS.Components;
using EGP.ProjectBoost.Scenes;
using EGP.ProjectBoost.Events;

public struct GameSystem : ISystem
{
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		ref GameStateComponent gameState = ref core.Get<GameStateComponent>(Entity.CreateFrom(GameStateComponent.Id));

		// process game state
		if (gameState.Crashed)
		{
			LoggerManager.LogDebug("Crashed!");

			this.Emit<GameStateCrashed>();

			gameState.Crashed = false;
			gameState.Restart = true;
		}
		if (gameState.Goal)
		{
			LoggerManager.LogDebug("Goal!");

			this.Emit<GameStateGoal>();

			gameState.Goal = false;
			gameState.Finished = true;
		}

		if (gameState.Restart)
		{
			gameState.Restart = false;

			// reload the scene
			var tween = ServiceRegistry.Instance.CreateTween();
			tween.TweenInterval(1.0);

			// set the tween callback to reload the scene
			tween.TweenCallback(Callable.From(CreateRestartTransition()));
		}
		if (gameState.Finished)
		{
			gameState.Finished = false;

			// load the next level
			// create a tween to wait
			var tween = ServiceRegistry.Instance.CreateTween();
			tween.TweenInterval(1.0);

			string nextLevelId = gameState.NextLevelSceneId;

			// set the tween callback to load the new scene
			tween.TweenCallback(Callable.From(CreateLevelTransition(nextLevelId)));
		}
	}

	public Action CreateRestartTransition()
	{
		Action transition = () => {
			LoggerManager.LogDebug("Restarting");
			ServiceRegistry.Get<SceneManager>().ReloadCurrentScene();
		};

		return transition;
	}

	public Action CreateLevelTransition(string levelId)
	{
		Action transition = () => {
			LoggerManager.LogDebug("Finished");
			ServiceRegistry.Get<SceneManager>().LoadScene(levelId);
		};

		return transition;
	}
}

