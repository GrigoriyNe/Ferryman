using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private FabricCars _fabricCars;
    [SerializeField] private Ferryboat _ferryboat;
    [SerializeField] private MapLogic _mapLogic;
    [SerializeField] private BridgeAnimator _bridge;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ObstacleLogic _obstacle;

    private void Start()
    {
        StartScene();
    }

    public bool TryPay(int coust)
    {
        if (_wallet.IsEnough(coust))
        {
            _wallet.RemoveMoney(coust);
            return true;
        }
        else
        {
            MakeOffer();
            return false;
        }
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
        
        StartCoroutine(OpenCargo());
    }

    private void EndScene()
    {
        _bridge.Close();
        StartCoroutine(CloseCargo());
    }

    private IEnumerator OpenCargo()
    {
        yield return new WaitForSeconds(3f);
        _obstacle.CreateObstacle();

        _counter.Activate();
        StartCoroutine(CreateCars());
    }

    private IEnumerator CloseCargo()
    {
        _ferryboat.Finish();
        _counter.Deactivate();
        _fabricCars.PackCars();

        yield return new WaitForSeconds(4f);
        StartScene();
    }

    private IEnumerator CreateCars()
    {
        yield return new WaitForSeconds(0.3f);
        

        int count = _mapLogic.CountFinishPlace;
        _fabricCars.SetPlacesNames(_ferryboat.GetPlaces());

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

        int countS = _mapLogic.CountFinishSpesialPlace;

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
