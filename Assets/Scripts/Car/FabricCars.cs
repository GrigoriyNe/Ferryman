using UnityEditor.Search;
using UnityEngine;

public class FabricCars : MonoBehaviour
{
    [SerializeField] private Car _prefab;
    [SerializeField] private MoverLogic _moverLogic;

    private int _notCreatedCar = 0;

    public void Create()
    {
        if (_moverLogic.CountStartPlace  == 0)
        {
            _notCreatedCar += 1;
            Debug.Log(_notCreatedCar);

            return;
        }

        Car car = Instantiate(_prefab);
        car.gameObject.SetActive(true);
        car.Init(_moverLogic.GetStartCarPosition(), _moverLogic.GetFinihCarPosition());
    }
}
