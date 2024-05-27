/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : MovingHazard
 * @created     : Sunday May 26, 2024 17:55:12 CST
 */

namespace EGP.ProjectBoost.Scenes;

using System;

using Godot;
using GodotEGP.Objects.Extensions;
using GodotEGP.Logging;
using GodotEGP.Service;
using GodotEGP.Event.Events;
using GodotEGP.Config;
using GodotEGP.Collections;

public partial class MovingHazard : AnimatableBody3D
{
	[Export]
	public Vector3 Destination { get; set; }

	[Export]
	public float Duration { get; set; } = 1.0f;

	public override void _Ready()
	{
		Tween tween = CreateTween();
		tween.SetLoops();
		tween.SetTrans(Tween.TransitionType.Sine);
		tween.TweenProperty(this, "global_position", GlobalPosition + Destination, Duration);
		tween.TweenProperty(this, "global_position", GlobalPosition, Duration);
	}
}

