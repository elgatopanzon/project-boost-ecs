/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : CollisionSystem
 * @created     : Sunday May 26, 2024 00:09:17 CST
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

using Godot;
using GodotEGP;
using GodotEGP.ECSv4;
using GodotEGP.ECSv4.Components;
using GodotEGP.ECSv4.Systems;
using GodotEGP.ECSv4.Queries;
using EGP.ProjectBoost.ECS.Components;
using EGP.ProjectBoost.Scenes;

public class CollisionSystem : ISystem
{
	public void Update(Entity entity, int index, SystemInstance system, double deltaTime, ECS core, Query query)
	{
		ref PlayerNodeComponent playerNode = ref query.Results.GetComponent<PlayerNodeComponent>(entity);
		ref PlayerStateComponent playerState = ref query.Results.GetComponent<PlayerStateComponent>(entity);
		ref GameStateComponent gameState = ref core.Get<GameStateComponent>(Entity.CreateFrom(GameStateComponent.Id));

		// get the Node3D object and process position
		Player player = core.GetObject<Player>(playerNode.PlayerNodeEntity);

		// process any collided objects
		if (player.Collisions.TryPeek(out Node node))
		{
			node = player.Collisions.Dequeue();

			LoggerManager.LogDebug("Collision system processing", "", "node", node.Name.ToString());

			if (node.IsInGroup("Goal"))
			{
				gameState.Goal = true;
				playerState.Goal = true;

				// check if it's a LandingPad
				if (node is LandingPad lp)
				{
					gameState.NextLevelSceneId = lp.NextLevelId;
				}

				LoggerManager.LogDebug("Setting goal game state");
			}
			else if (node.IsInGroup("Hazard"))
			{
				gameState.Crashed = true;
				playerState.Crashed = true;

				LoggerManager.LogDebug("Setting crashed game state");
			}
		}
	}
}
