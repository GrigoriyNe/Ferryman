using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private FabricCars _fabricCars;
    [SerializeField] private NumberingPlaceText _numberingText;
    [SerializeField] private Ferryboat _ferryboat;
    [SerializeField] private GameObject _ferryboatBackground;
    [SerializeField] private BridgeAnimator _bridge;
    [SerializeField] private WindowBlind _blind;
    [SerializeField] private ScoreCounter _counter;

    private void Start()
    {
        StartScene();
    }

    public void RoundOver()
    {
        _blind.Close();
        EndScene();
    }

    private void StartScene()
    {
        _bridge.Open();
        _ferryboat.gameObject.SetActive(true);
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
        _ferryboat.gameObject.SetActive(false);
        _blind.Open();
        
        yield return new WaitForSeconds(1f);
        _map.Activate();
        _ferryboatBackground.SetActive(true);
        _numberingText.Activate();
        _counter.Activate();
        StartCoroutine(CreateCars());
    }

    private IEnumerator CloseGarage()
    {
        _map.Deactivate();
        yield return new WaitForSeconds(1f);

        _blind.gameObject.SetActive(false);
        _ferryboat.gameObject.SetActive(true);
        _ferryboat.Finish();
        _numberingText.Deactivate();
        _ferryboatBackground.SetActive(false);
        _counter.Deactivate();
        _fabricCars.PackCars();

        yield return new WaitForSeconds(4f);
        StartScene();
    }
    private IEnumerator CreateCars()
    {
        int count = _map.CountFinishPlace;

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
    }
}
