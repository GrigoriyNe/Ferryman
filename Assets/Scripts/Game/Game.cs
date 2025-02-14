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
        int count = _moverLogic.CountFinishPlace;

        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.4f);
            _fabricCars.Create();
            
        }

        if (_fabricCars.NotCreatedCarCount > 0)
        {
            yield return new WaitForSeconds(2f);
            _fabricCars.Create();
        }
    }
}
