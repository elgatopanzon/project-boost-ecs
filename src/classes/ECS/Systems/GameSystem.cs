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

public struct GameSystem : ISystem
{
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		ref GameStateComponent gameState = ref core.Get<GameStateComponent>(Entity.CreateFrom(GameStateComponent.Id));

		// process game state
		if (gameState.Crashed)
		{
			LoggerManager.LogDebug("Crashed!");

			gameState.Crashed = false;
			gameState.Restart = true;
		}
		if (gameState.Goal)
		{
			LoggerManager.LogDebug("Goal!");

			gameState.Goal = false;
			gameState.Finished = true;
		}

		if (gameState.Restart)
		{
			LoggerManager.LogDebug("Restarting");
			gameState.Restart = false;

			// reload the scene
			ServiceRegistry.Get<SceneManager>().ReloadCurrentScene();
		}
		if (gameState.Finished)
		{
			LoggerManager.LogDebug("Finished");
			gameState.Finished = false;

			// load the next level
			ServiceRegistry.Get<SceneManager>().LoadScene(gameState.NextLevelSceneId);
		}
	}
}

