﻿using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static readonly UnityEvent OnLevelCleared = new UnityEvent();
    public static readonly UnityEvent<Transform> OnEnemyDied = new UnityEvent<Transform>();
}