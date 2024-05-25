/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : PlayerNodeComponent
 * @created     : Saturday May 25, 2024 15:35:03 CST
 */

namespace EGP.ProjectBoost.ECS.Components;

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

public struct PlayerNodeComponent : IComponentData
{
	public static int Id { get; set; }

	public Entity PlayerNodeEntity { get; set; }
}
