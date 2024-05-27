/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : PlayerStateComponent
 * @created     : Monday May 27, 2024 12:28:14 CST
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

public struct PlayerStateComponent : IComponentData
{
	public static int Id { get; set; }

	// thruster state enum for thruster on/off
	public enum ThrusterState
	{
		Off = 0,
		On = 1
	}

	public ThrusterState MainThrusterState { get; set; }
	public ThrusterState LeftThrusterState { get; set; }
	public ThrusterState RightThrusterState { get; set; }

	public bool Crashed { get; set; }
	public bool Goal { get; set; }
}
