/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : InputStateComponent
 * @created     : Saturday May 25, 2024 15:24:03 CST
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

public struct InputStateComponent : IComponentData
{
	public static int Id { get; set; }

	// input states
	public bool ThrustPressed { get; set; }
	public bool LeftPressed { get; set; }
	public bool RightPressed { get; set; }
}
