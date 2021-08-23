using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance { get { return _instance; } }

    [Header("UI References")]
    [SerializeField] private TMP_Text interactionTextReference;

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
