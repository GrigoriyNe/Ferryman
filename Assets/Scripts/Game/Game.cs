using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private FabricCars _fabricCars;
    [SerializeField] private BridgeAnimator _bridge;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ObstacleLogic _obstacle;
    [SerializeField] private ShipAdder _shipAdder;
    [SerializeField] private MapLogic _mapLogic;

    private Ferryboat _ferryboat;
    private Coroutine _creatigCars = null;

    private void Start()
    {
        SetStartFerryboat();
        StartScene();
    }

    public bool TryPay(int coust)
    {
        if (_wallet.IsEnoughMoney(coust))
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

    public void StepsOver()
    {
        RoundOver();
    }

    public void RoundOver()
    {
        if (_ferryboat.IsFuelEnough())
        {
            StartCoroutine(ChangeRound());
        }
        else
        {
            MakeOffer();
        }
    }

    public void CreateNewCar()
    {
        if (_creatigCars == null)
            StartCoroutine(CreatingCars());
    }

    public Fueltank GetTank()
    {
        return _ferryboat.GetFueltank();
    }

    private void MakeOffer()
    {
        throw new NotImplementedException();
    }

    private IEnumerator ChangeRound()
    {
        EndScene();
        yield return new WaitForSeconds(3.8f);
        StartScene();
    }

    public void SetNextFerryboat()
    {
        if (_shipAdder.IsAmout())
        {
            _creatigCars = null;
            EndScene();
            _obstacle.Clean();
            _mapLogic.Clean();
            _ferryboat = _shipAdder.GetNextFerryboat();
            StartScene();
        }
    }

    private void SetStartFerryboat()
    {
        _ferryboat = _shipAdder.GetFerryboat();
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
        _creatigCars = StartCoroutine(CreatingCars());
    }

    private IEnumerator CloseCargo()
    {
        _ferryboat.Finish();
        _counter.Deactivate();
        _fabricCars.DeactivateCars();

        yield return new WaitForSeconds(4f);
    }

    private IEnumerator CreatingCars()
    {
        yield return new WaitForSeconds(1f);

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

        int countSpesial = _mapLogic.CountFinishSpesialPlace;

        for (int i = 0; i < countSpesial; i++)
        {
            yield return new WaitForSeconds(0.4f);
            _fabricCars.CreateSpesial();
        }

        if (_fabricCars.NotCreatedSpesialCarCount > 0)
        {
            yield return new WaitForSeconds(2f);
            _fabricCars.CreateSpesial();
        }

        _creatigCars = null;
    }
}
