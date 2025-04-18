using UnityEngine;

public abstract class Namer : MonoBehaviour
{
    public abstract string GetTextPlace(int vertical, int horizontal);

    internal Namer Get()
    {
        return this;
    }
}
