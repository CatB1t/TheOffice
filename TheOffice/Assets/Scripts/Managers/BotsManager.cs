using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotsManager : MonoBehaviour
{
    public static BotsManager Instance { get { return _instance; } }
    private static BotsManager _instance;

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

    [SerializeField] private List<BotNavigation> botsNavigatinInScene;
    [SerializeField] private List<BotNavigation> chaosedBots;
}
