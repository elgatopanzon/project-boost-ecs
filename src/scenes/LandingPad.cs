/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : LandingPad
 * @created     : Sunday May 26, 2024 16:21:19 CST
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

public partial class LandingPad : CsgBox3D
{
	[Export]
	public string NextLevelId { get; set; }
}
