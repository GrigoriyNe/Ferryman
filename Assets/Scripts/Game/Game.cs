using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    private const int FuelCoust = 1;

    [SerializeField] private FabricCars _fabricCars;
    [SerializeField] private Ferryboat _ferryboat;
    [SerializeField] private BridgeAnimator _bridge;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private ButtonFuel _fuelerAdder;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _fuelerAdder.ButtonClicked += TryRefill;
    }

    private void OnDisable()
    {
        _fuelerAdder.ButtonClicked -= TryRefill;
    }

    private void TryRefill()
    {
        if(_ferryboat.GetEnoughValue() == 0)
                return;

        int coustRefull = _ferryboat.GetEnoughValue() * FuelCoust;

        if (_wallet.IsEnough(coustRefull))
        {
            _wallet.RemoveMoney(coustRefull);
            _ferryboat.Refill();
        }
        else
        {
            MakeOffer();
        }
    }

    private void Start()
    {
        StartScene();
    }

    public void RoundOver()
    {
        if (_ferryboat.CheckFuel())
            EndScene();
        else
            MakeOffer();
    }

    private void MakeOffer()
    {
        throw new NotImplementedException();
    }

    private void StartScene()
    {
        _bridge.Open();
        _ferryboat.Activate();
        StartCoroutine(OpenGarage());
    }

    private void EndScene()
    {
        _bridge.Close();
        StartCoroutine(CloseGarage());
    }

    private IEnumerator OpenGarage()
    {
        yield return new WaitForSeconds(3f);

        _counter.Activate();
        StartCoroutine(CreateCars());
    }

    private IEnumerator CloseGarage()
    {
        _ferryboat.Finish();
        _counter.Deactivate();
        _fabricCars.PackCars();

        yield return new WaitForSeconds(4f);
        StartScene();
    }

    private IEnumerator CreateCars()
    {
        int count = _ferryboat.CountFinishPlace;

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

        int countS = _ferryboat.CountSpesialFinishPlace;

        for (int i = 0; i < countS; i++)
        {
            yield return new WaitForSeconds(0.4f);
            _fabricCars.CreateSpesial();
        }

        if (_fabricCars.NotCreatedSpesialCarCount > 0)
        {
            yield return new WaitForSeconds(2f);
            _fabricCars.CreateSpesial();
        }
    }
}
