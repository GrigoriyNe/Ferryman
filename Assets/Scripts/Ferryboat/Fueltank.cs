using System;
using UnityEngine;

public class Fueltank : MonoBehaviour
{
    [SerializeField] private int _maxValue = 100;
    [SerializeField] private int _fuelСonsumption = 20;
    [SerializeField] private int _fuelAddForSaleSizedTank = 30;

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
        return _maxValue - _fuelСonsumption > 0;
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
        FuelValue -= _fuelСonsumption;
        Changed(FuelValue);
    }

    public void SetMoreTank()
    {
        _maxValue += _fuelAddForSaleSizedTank;
        ChangedMax(_fuelAddForSaleSizedTank);
    }
}
