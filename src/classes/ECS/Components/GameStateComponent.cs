/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : GameStateComponent
 * @created     : Sunday May 26, 2024 00:14:45 CST
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

public struct GameStateComponent : IComponentData
{
	public static int Id { get; set; }

	public bool Goal { get; set; }
	public bool Crashed { get; set; }
	public bool Finished { get; set; }
	public bool Restart { get; set; }
}
