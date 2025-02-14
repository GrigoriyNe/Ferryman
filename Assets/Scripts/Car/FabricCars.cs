using UnityEditor.Search;
using UnityEngine;

public class FabricCars : MonoBehaviour
{
    [SerializeField] private Car _prefab;
    [SerializeField] private MoverLogic _moverLogic;

    public int NotCreatedCarCount {get; private set;}

    private void Start()
    {
        NotCreatedCarCount = 0;
    }

    public void Create()
    {
        if (_moverLogic.CountStartPlace  == 0)
        {
            NotCreatedCarCount += 1;
            Debug.Log(NotCreatedCarCount);

            return;
        }

        Car car = Instantiate(_prefab);
        car.gameObject.SetActive(true);
        car.Init(_moverLogic.GetStartCarPosition(), _moverLogic.GetFinihCarPosition());
    }
}
