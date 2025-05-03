using System;
using System.Collections;
using UnityEngine;
using YG;

namespace Game
{
    public class GameProcess : MonoBehaviour
    {
        private const int RoundSecondBoat = 5;
        private const int RoundThirdBoat = 10;
        private const int RoundRandomBoat = 15;
        private const int LowerMoney = 0;

        private const int SecondFerryboat = 1;
        private const int ThirdFerryboat = 2;

        private const int DividerRanomVertival = 2;
        private const int DividerRanomHorizontal = 4;

        private const int DividerRandomRound = 3;
        private const int BombForRound = 1;
        private const int RoundAfteInterstitialAdvShow = 6;

        [SerializeField] private Counters.ScoreCounter _counter;
        [SerializeField] private Counters.RewardCounter _rewardCounter;
        [SerializeField] private MapFerryboat.MapLogic _mapLogic;
        [SerializeField] private Obstacle.ObstacleLogic _obstacle;
        [SerializeField] private PlayerResouce.Wallet _wallet;
        [SerializeField] private SoungsGroup.Soungs _soungs;
        [SerializeField] private UiOnScreen.AnimationResources _restartInfoView;
        [SerializeField] private Envoriments.BridgeAnimator _bridge;
        [SerializeField] private FerryboatGroup.FerryboatFabric _shipAdder;
        [SerializeField] private CarGroup.FabricCars _fabricCars;
        [SerializeField] private CameraMove.CameraMover _cameraMover;
        [SerializeField] private ScenesChanger _scenes;
        [SerializeField] private Canvas _offerRestart;
        [SerializeField] private LeaderbordCounter _leaderbordCounter;

        private FerryboatGroup.Ferryboat _ferryboat;
        private Coroutine _creatigCars = null;
        private int _currentRound = 0;
        private int _currentFerryboat = 0;

        private WaitForSeconds _creatingCarDelay;
        private float _creatingCarDelayValue = 0.3f;

        private WaitForSeconds _waitChangeRound;
        private float _delayChangeRoundValue = 3.8f;

        private WaitForSeconds _waitCreateCars;
        private float _delayWaitCreateCarsValue = 1f;

        private WaitForSeconds _waitInterstitial;
        private float _delayWaitInterstitialValue = 2f;

        private WaitForSeconds _waitChangeFerryboat;
        private float _delayWaitChangeFerryboatValue = 3f;

        private WaitForSeconds _waitCloseCargo;
        private float _delayWaitCloseCargoValue = 4f;

        public event Action StartSceneStart;

        public event Action StartSceneDone;

        public event Action FinishSceneStart;

        public event Action FinishSceneDone;

        public event Action<int> LevelChange;

        private void OnEnable()
        {
            SetWaitings();
            LoadSaves();
        }

        public void Fail()
        {
            _wallet.SetDefaultValue();
            _currentRound = 0;

            YG2.saves.Money = _wallet.Money;
            YG2.saves.Bomb = _wallet.Bomb;
            YG2.saves.Level = _currentRound;
            YG2.saves.Ferryboat = 0;

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
                TryChangeCameraMobile();

                SetFerryboat(SecondFerryboat);

                return;
            }
            else if (_currentRound == RoundThirdBoat)
            {
                TryChangeCameraMobile();

                SetFerryboat(ThirdFerryboat);

                return;
            }
            else if (_currentRound == RoundRandomBoat)
            {
                SetFerryboat(UnityEngine.Random.Range(0, _shipAdder.FerryboatsCount));

                return;
            }
            else if (_currentRound > RoundRandomBoat && _currentRound % DividerRandomRound == 0)
            {
                SetFerryboat(UnityEngine.Random.Range(0, _shipAdder.FerryboatsCount));

                return;
            }

            YG2.MetricaSend(_currentRound.ToString());
            YG2.saves.Ferryboat = _currentFerryboat;
            YG2.saves.Money = _wallet.Money;
            YG2.saves.Bomb = _wallet.Bomb;
            YG2.saves.Level = _currentRound;
            YG2.SaveProgress();

            StartCoroutine(ChangeRound());
        }

        private void LoadSaves()
        {
            _wallet.SetLoadValues(YG2.saves.Money, YG2.saves.Bomb);
            _ferryboat = _shipAdder.GetFerryboat(YG2.saves.Ferryboat);

            _currentRound = YG2.saves.Level;
            LevelChange?.Invoke(_currentRound);

            if (_currentRound == 0)
                _wallet.SetDefaultValue();

            if (_currentRound > RoundSecondBoat && YG2.envir.isMobile)
                _cameraMover.Zoom();

            if (_currentRound > RoundThirdBoat && YG2.envir.isMobile)
                _cameraMover.Zoom();

            StartScene();
        }

        private void SetWaitings()
        {
            _waitChangeRound = new WaitForSeconds(_delayChangeRoundValue);
            _creatingCarDelay = new WaitForSeconds(_creatingCarDelayValue);
            _waitCreateCars = new WaitForSeconds(_delayWaitCreateCarsValue);
            _waitInterstitial = new WaitForSeconds(_delayWaitInterstitialValue);
            _waitChangeFerryboat = new WaitForSeconds(_delayWaitChangeFerryboatValue);
            _waitCloseCargo = new WaitForSeconds(_delayWaitCloseCargoValue);
        }

        private void TryChangeCameraMobile()
        {
            if (YG2.envir.isMobile)
            {
                _cameraMover.Zoom();
            }
        }

        private void SetFerryboat(int value)
        {
            _currentFerryboat = value;

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
            StartCoroutine(OpeningCargo());
        }

        private void EndScene()
        {
            FinishSceneStart?.Invoke();
            PlayFerryboatEngineSoung();
            _bridge.Close();
            StartCoroutine(ClosingCargo());
        }

        private void PlayFerryboatEngineSoung()
        {
            _soungs.PlayFerryboatEngineSoung();
        }

        private IEnumerator ChangeRound()
        {
            EndScene();
            yield return _waitChangeRound;
            StartScene();
        }

        private IEnumerator ChangingFerryboat(int value)
        {
            yield return _waitChangeFerryboat;
            _ferryboat = _shipAdder.GetFerryboat(value);
            _fabricCars.SetPlacesNames(_ferryboat.GetPlaces());
            StartScene();
        }

        private IEnumerator OpeningCargo()
        {
            yield return _waitChangeFerryboat;
            _obstacle.CreateObstacle();
            _obstacle.SetRandomObstacle();

            _counter.Activate();
            _soungs.PlayCreatCar();
            _creatigCars = StartCoroutine(CreatingCars());
            _mapLogic.PrepareReward();
            StartSceneDone?.Invoke();
        }

        private IEnumerator ClosingCargo()
        {
            _ferryboat.Finish();
            _counter.Deactivate();
            _fabricCars.DeactivateCars();

            yield return _waitCloseCargo;
            FinishSceneDone?.Invoke();
        }

        private IEnumerator CreatingCars()
        {
            yield return _waitCreateCars;

            int count = _mapLogic.GetMaxFinishPlaceCount();
            int countSpesial = _mapLogic.GetMaxSpesialFinishPlaceCount();

            int plaseStartSpesialCar = UnityEngine.Random.Range(
                count / DividerRanomHorizontal, count / DividerRanomVertival);

            _fabricCars.SetPlacesNames(_ferryboat.GetPlaces());

            for (int i = 0; i < count; i++)
            {
                _fabricCars.Create();

                yield return _creatingCarDelay;

                if (i >= plaseStartSpesialCar && i < (countSpesial + plaseStartSpesialCar))
                {
                    _fabricCars.CreateSpesial();

                    yield return _creatingCarDelay;
                }
            }

            _creatigCars = null;
        }

        private IEnumerator ShowingInterstitial()
        {
            yield return _waitInterstitial;

            YG2.InterstitialAdvShow();
        }
    }
}