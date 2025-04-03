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
    [SerializeField] private Soungs _soungs;
    [SerializeField] private Scenes _scenes;

    private Ferryboat _ferryboat;
    private Coroutine _creatigCars = null;
    private int _currentRound = 0;

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
    public event Action<int> LevelChange;

    private void Start()
    {
        SetWaitings();
        _ferryboat = _shipAdder.GetFerryboat(StartFerryboat);
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
        if (_wallet.Money < -200)
        {
            Fail();
            return;
        }

        if (_creatigCars != null)
            StopCoroutine(_creatigCars);

        _soungs.PlayGarageSoung();
        _soungs.PlayRestartSoung();

        _wallet.AddMoney(_rewardCounter.GetRewardValue());
        _wallet.AddBomb(1);

        _currentRound += 1;
        LevelChange?.Invoke(_currentRound);

        if (_currentRound == RoundSecondBoat)
        {
            SetNextFerryboat(1);
            return;
        }
        else if (_currentRound == RoundThirdBoat)
        {
            SetNextFerryboat(2);
            return;
        }
        else if (_currentRound == RoundRandomBoat)
        {
            SetNextFerryboat(UnityEngine.Random.Range(0, _shipAdder.FerryboatsCount));
            return;
        }
        else if (_currentRound > RoundRandomBoat && _currentRound % 3 == 0)
        {
            SetNextFerryboat(UnityEngine.Random.Range(0, _shipAdder.FerryboatsCount));
            return;
        }

        StartCoroutine(ChangeRound());
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

    private void Fail()
    {
        _scenes.ReastartLevel();
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
       // _cameraMover.Zoom();
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
        PlayFerryboatEngineSoung();
        StartCoroutine(OpenCargo());
    }

    private void EndScene()
    {
        FinishSceneStart?.Invoke();
        PlayFerryboatEngineSoung();
        _bridge.Close();
        StartCoroutine(CloseCargo());
    }

    private void PlayFerryboatEngineSoung()
    {
        _soungs.PlayFerryboatEngineSoung();
    }

    private IEnumerator OpenCargo()
    {
        yield return _wait3Second;
        _soungs.PlayGarageSoung();
        _obstacle.CreateObstacle();
        _obstacle.SetRandomObstacle();

        _counter.Activate();
        _soungs.PlayCreatCar();
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

        int count = _mapLogic.GetMaxFinishPlaceCount();
        _fabricCars.SetPlacesNames(_ferryboat.GetPlaces());

        for (int i = 0; i < count; i++)
        {
            yield return _wait5Millisecond;
            _fabricCars.Create();
        }

        int countSpesial = _mapLogic.GetMaxSpesialFinishPlaceCount();

        for (int i = 0; i < countSpesial; i++)
        {
            yield return _wait5Millisecond;
            _fabricCars.CreateSpesial();

        }

        _creatigCars = null;
    }
}
