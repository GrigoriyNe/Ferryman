using UnityEngine;

public class FerryboatFabric : MonoBehaviour
{
    public Ferryboat Create(Ferryboat ferryboat)
    {
        return Instantiate(ferryboat);
    }
}
