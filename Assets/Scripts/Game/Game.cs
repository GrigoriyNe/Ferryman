using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MoverLogic _moverLogic;
    [SerializeField] private FabricCars _fabricCars;

    private void Start()
    {
        StartCoroutine(CreateCars());
    }

    private IEnumerator CreateCars()
    {
        int count = _moverLogic.CountStartPlace;

        for (int i = 0; i < count; i++)
        {
            _fabricCars.Create();
            
        }

        yield return new WaitForSeconds(1);
    }
}
