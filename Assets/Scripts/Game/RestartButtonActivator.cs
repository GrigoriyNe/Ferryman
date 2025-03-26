using UnityEngine;
using UnityEngine.UI;

public class RestartButtonActivator : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    public void Activate()
    {
        _restartButton.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
      //  _restartButton.gameObject.SetActive(false);
    }
}