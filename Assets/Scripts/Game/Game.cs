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
        yield return new WaitForSeconds(1);

        for (int i = 0; i <= _moverLogic.CountStartPlace+ 2; i++)
        {
            _fabricCars.Create();
        }
    }
}
