using System;
using UnityEngine;

public class Fueltank : MonoBehaviour
{
    [SerializeField] private int _maxValue = 100;
    [SerializeField] private int _minConsumption = 5;
    [SerializeField] private int _consumption = 20;
    [SerializeField] private int _valueMaxInSizedTank = 30;

    public int FuelValue { get; private set; }

    public Action<int> ChangedMax;
    public Action<int> Changed;

    public int Max => _maxValue;

    private void Start()
    {
        FuelValue = _maxValue;
        Changed(FuelValue);
    }

    public bool CheckFull()
    {
        return _maxValue - _consumption > 0;
    }

    public int GetEnoughValue()
    {
        int result = _maxValue - FuelValue;
        return result;
    }

    public void Refill()
    {
        FuelValue = _maxValue;
        Changed(FuelValue);
    }

    public void Travel()
    {
        FuelValue -= _consumption;
        Changed(FuelValue);
    }

    public void SetMoreTank()
    {
        _maxValue += _valueMaxInSizedTank;
        ChangedMax(_valueMaxInSizedTank);
    }

    public void ImproveEngine()
    {
        if(_consumption - 3 > _minConsumption)
        {
            _consumption -= 3;
        }

     //   Debug.Log(_consumption);
    }
}
