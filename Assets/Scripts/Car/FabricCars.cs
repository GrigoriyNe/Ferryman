using UnityEngine;

public class FabricCars : MonoBehaviour
{
    [SerializeField] private Car _prefab;
    [SerializeField] private MoverLogic _moverLogic;

    public void Create()
    {
        Car car = Instantiate(_prefab);
        car.gameObject.SetActive(true);
        car.Init(_moverLogic.GetStartCarPosition(), _moverLogic.GetFinihCarPosition());
    }
}
