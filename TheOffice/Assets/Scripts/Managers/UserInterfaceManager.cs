using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance { get { return _instance; } }

    [Header("UI References")]
    [SerializeField] private TMP_Text interactionTextReference;
    [SerializeField] private TMP_Text destroyedCount;

    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private GameObject wonCanvas;
    [SerializeField] private GameObject lostCanvas;

    private GameObject _currentActiveCanvas;

    private static UserInterfaceManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        playerCanvas.SetActive(true);
        _currentActiveCanvas = playerCanvas;
    }

    public void ShowPlayerCanvas()
    {
        _currentActiveCanvas.SetActive(false);
        playerCanvas.SetActive(true);
        _currentActiveCanvas = playerCanvas;
    }

    public void ShowPauseCanvas()
    {
        _currentActiveCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        _currentActiveCanvas = pauseCanvas;
    }

    public void ShowWonCanvas()
    {
        _currentActiveCanvas.SetActive(false);
        wonCanvas.SetActive(true);
        _currentActiveCanvas = wonCanvas;
    }

    public void ShowLostCanvas()
    {
        _currentActiveCanvas.SetActive(false);
        lostCanvas.SetActive(true);
        _currentActiveCanvas = lostCanvas;
    }

    public void UpdateDestroyCount(int current, int max)
    {
        destroyedCount.text = current + "/" + max;
    }

    #region Interaction UI
    public void UpdateInteractionText(string text)
    {
        interactionTextReference.text = text;
    }

    // TODO this causing bug
    public void ShowInteractionText()
    {
        interactionTextReference.enabled = true;
    }

    // TODO this causing bug
    public void HideInteractionText()
    {
        interactionTextReference.enabled = false;
    }
    #endregion

}
