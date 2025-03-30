using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Game : MonoBehaviour
{
    private const int RoundSecondBoat = 5;
    private const int RoundThirdBoat = 10;
    private const int RoundRandomBoat = 15;
    private const int StartFerryboat = 0;


    [SerializeField] private FabricCars _fabricCars;
    [SerializeField] private BridgeAnimator _bridge;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ObstacleLogic _obstacle;
    [SerializeField] private FerryboatFabric _shipAdder;
    [SerializeField] private MapLogic _mapLogic;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private RewardCounter _rewardCounter;
    [SerializeField] private OfferWindowSpesialRemoveOstacle _offerBomb;

    private Ferryboat _ferryboat;
    private Coroutine _creatigCars = null;
    private int _thisRound = 0;

    private WaitForSeconds _wait5Millisecond;
    private float _delay5Millisecond = 0.5f;
    private WaitForSeconds _wait38Millisecond;
    private float _delay38Millisecond = 3.8f;
    private WaitForSeconds _wait1Second;
    private float _delay1Second = 1f;
    private WaitForSeconds _wait2Second;
    private float _delay2Second = 2f;
    private WaitForSeconds _wait3Second;
    private float _delay3Second = 3f;
    private WaitForSeconds _wait4Second;
    private float _delay4Second = 4f;

    public event Action StartSceneStart;
    public event Action StartSceneDone;
    public event Action FinishSceneStart;
    public event Action FinishSceneDone;

    private void Start()
    {
        SetWaitings();
        _ferryboat = _shipAdder.GetFerryboat(StartFerryboat);
        StartScene();
    }

    private void SetWaitings()
    {
        _wait38Millisecond = new WaitForSeconds(_delay38Millisecond);
        _wait5Millisecond = new WaitForSeconds(_delay5Millisecond);
        _wait1Second = new WaitForSeconds(_delay1Second);
        _wait2Second = new WaitForSeconds(_delay2Second);
        _wait3Second = new WaitForSeconds(_delay3Second);
        _wait4Second = new WaitForSeconds(_delay4Second);
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

    public void TryUseBomb()
    {
        if (_wallet.IsEnoughBomb())
        {
            _wallet.RemoveBomb();
            _obstacle.ActivateSpesialClicked();
        }
        else
        {
            MakeOffer();
        }
    }

    public void RoundOver()
    {
        if (_creatigCars != null)
            StopCoroutine(_creatigCars);

        _wallet.AddMoney(_rewardCounter.GetRewardValue());
        StartCoroutine(ChangeRound());

        _thisRound += 1;

        if (_thisRound > RoundSecondBoat)
            SetNextFerryboat(1);
        if (_thisRound > RoundThirdBoat)
            SetNextFerryboat(2);
        if (_thisRound > RoundRandomBoat)
            SetNextFerryboat(UnityEngine.Random.Range(0, _shipAdder.FerryboatsCount));

    }

    private void MakeOffer()
    {
        _offerBomb.Open();
    }

    private IEnumerator ChangeRound()
    {
        EndScene();
        yield return _wait38Millisecond;
        StartScene();
    }

    public void SetNextFerryboat(int value)
    {
        _creatigCars = null;
        EndScene();
        _obstacle.Clean();
        _mapLogic.Clean();
        StartCoroutine(ChangingFerryboat(value));
        _cameraMover.Zoom();
    }

    private IEnumerator ChangingFerryboat(int value)
    {
        yield return _wait3Second;
        _ferryboat = _shipAdder.GetFerryboat(value);
        _fabricCars.SetPlacesNames(_ferryboat.GetPlaces());
        StartScene();
    }

    private void StartScene()
    {
        StartSceneStart?.Invoke();
        _bridge.Open();
        _ferryboat.Activate();
        StartCoroutine(OpenCargo());
    }

    private void EndScene()
    {
        FinishSceneStart?.Invoke();
        _bridge.Close();
        StartCoroutine(CloseCargo());
    }

    private IEnumerator OpenCargo()
    {
        yield return _wait3Second;
        _obstacle.CreateObstacle();
        _obstacle.SetRandomObstacle();

        _counter.Activate();
        _creatigCars = StartCoroutine(CreatingCars());
        _mapLogic.PrepareReward();
        StartSceneDone?.Invoke();
    }

    private IEnumerator CloseCargo()
    {
        _ferryboat.Finish();
        _counter.Deactivate();
        _fabricCars.DeactivateCars();

        yield return _wait4Second;
        FinishSceneDone?.Invoke();
    }

    private IEnumerator CreatingCars()
    {
        yield return _wait1Second;

        int count = _mapLogic.CountFinishPlace;
        _fabricCars.SetPlacesNames(_ferryboat.GetPlaces());

        for (int i = 0; i < count; i++)
        {
            yield return _wait5Millisecond;
            _fabricCars.Create();
        }

        if (_fabricCars.NotCreatedCarCount > 0)
        {
            yield return _wait2Second;
            _fabricCars.Create();
        }

        int countSpesial = _mapLogic.CountFinishSpesialPlace;

        for (int i = 0; i < countSpesial; i++)
        {
            yield return _wait5Millisecond;
            _fabricCars.CreateSpesial();
        }

        if (_fabricCars.NotCreatedSpesialCarCount > 0)
        {
            yield return _wait2Second;
            _fabricCars.CreateSpesial();
        }

        _creatigCars = null;
    }
}
