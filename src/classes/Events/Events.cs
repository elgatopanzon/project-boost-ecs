/**
 * @author      : ElGatoPanzon (contact@elgatopanzon.io) Copyright (c) ElGatoPanzon
 * @file        : Events
 * @created     : Sunday May 26, 2024 19:49:40 CST
 */

namespace EGP.ProjectBoost.Events;

using System;

using Godot;
using GodotEGP.Objects.Extensions;
using GodotEGP.Logging;
using GodotEGP.Service;
using GodotEGP.Event.Events;
using GodotEGP.Config;
using GodotEGP.Collections;

public class GameStateCrashed : Event {}
public class GameStateGoal : Event {}
