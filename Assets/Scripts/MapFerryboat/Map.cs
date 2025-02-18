﻿using UnityEngine;

public abstract class Map : MonoBehaviour
{
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract int GetHeight();
}