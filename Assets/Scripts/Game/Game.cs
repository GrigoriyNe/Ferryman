using System;
using System.Collections;
using UnityEngine;
using YG;

public class Game : MonoBehaviour
{
    private const int RoundSecondBoat = 5;
    private const int RoundThirdBoat = 10;
    private const int RoundRandomBoat = 15;
    private const int LowerMoney = 0;

    private const int StartFerryboat = 0;
    private const int DividerRandomRound = 3;
    private const int BombForRound = 1;
    private const int RoundAfteInterstitialAdvShow = 6;

    [SerializeField] private FabricCars _fabricCars;
    [SerializeField] private BridgeAnimator _bridge;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ObstacleLogic _obstacle;
    [SerializeField] private FerryboatFabric _shipAdder;
    [SerializeField] private MapLogic _mapLogic;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private RewardCounter _rewardCounter;
    [SerializeField] private Soungs _soungs;
    [SerializeField] private Scenes _scenes;
    [SerializeField] private Canvas _offerRestart;
    [SerializeField] private LeaderbordCounter _leaderbordCounter;
    [SerializeField] private AnimationResources _restartInfoView;

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

    public void Fail()
    {
        _scenes.ReastartLevel();
        Time.timeScale = 1;
        _offerRestart.gameObject.SetActive(false);
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
            return false;
        }
    }

    public void TryUseBomb()
    {
        if (_wallet.IsEnoughBomb())
        {
            _obstacle.TryActivateSpesialClicked();
        }
    }

    public void OfferRoundOver()
    {
        _soungs.PlayCreatCar();
        _soungs.PlayRestartSoung();
        _fabricCars.RecoverPositionNotParkCars();
        _restartInfoView.ActivateRestartButtomAnimatoin();
        _bridge.CloseOnRound();
    }

    public void RoundOver()
    {
        _wallet.AddMoney(_rewardCounter.GetRewardValue());
        _leaderbordCounter.ChangeCounter();
        _restartInfoView.DeactivateRestartButtomAnimatoin();

        if (_wallet.Money < LowerMoney)
        {
            _offerRestart.gameObject.SetActive(true);
            Time.timeScale = 0;

            return;
        }

        if (_creatigCars != null)
            StopCoroutine(_creatigCars);

        _soungs.PlayRestartSoung();

        _wallet.AddBomb(BombForRound);
        _currentRound += 1;
        LevelChange?.Invoke(_currentRound);

        if (_currentRound % RoundAfteInterstitialAdvShow == 0)
            StartCoroutine(ShowingInterstitial());

        if (_currentRound == RoundSecondBoat)
        {
            if (YG2.envir.isMobile)
            {
                _cameraMover.Zoom();
            }

            SetNextFerryboat(1);
            return;
        }
        else if (_currentRound == RoundThirdBoat)
        {
            if (YG2.envir.isMobile)
            {
                _cameraMover.Zoom();
            }

            SetNextFerryboat(2);
            return;
        }
        else if (_currentRound == RoundRandomBoat)
        {
            SetNextFerryboat(UnityEngine.Random.Range(0, _shipAdder.FerryboatsCount));
            return;
        }
        else if (_currentRound > RoundRandomBoat && _currentRound % DividerRandomRound == 0)
        {
            SetNextFerryboat(UnityEngine.Random.Range(0, _shipAdder.FerryboatsCount));
            return;
        }

        YG2.MetricaSend(_currentRound.ToString());
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

    private IEnumerator ChangeRound()
    {
        EndScene();
        yield return _wait38Millisecond;
        StartScene();
    }

    private void SetNextFerryboat(int value)
    {
        _creatigCars = null;
        EndScene();
        _obstacle.Clean();
        _mapLogic.Clean();
        StartCoroutine(ChangingFerryboat(value));
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

    private IEnumerator ChangingFerryboat(int value)
    {
        yield return _wait3Second;
        _ferryboat = _shipAdder.GetFerryboat(value);
        _fabricCars.SetPlacesNames(_ferryboat.GetPlaces());
        StartScene();
    }

    private IEnumerator OpenCargo()
    {
        yield return _wait3Second;
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
        int countSpesial = _mapLogic.GetMaxSpesialFinishPlaceCount();

        int plaseStartSpesialCar = UnityEngine.Random.Range((count / 4), (count / 2));

        _fabricCars.SetPlacesNames(_ferryboat.GetPlaces());

        for (int i = 0; i < count; i++)
        {
            _fabricCars.Create();

            yield return _wait5Millisecond;

            if (i >= plaseStartSpesialCar && i < (countSpesial + plaseStartSpesialCar))
            {
                _fabricCars.CreateSpesial();

                yield return _wait5Millisecond;
            }
        }

        _creatigCars = null;
    }

    private IEnumerator ShowingInterstitial()
    {
        yield return _wait2Second;

        YG2.InterstitialAdvShow();
    }
}
