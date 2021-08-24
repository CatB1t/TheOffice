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

    // 1. Keep track of spawened bots
    [SerializeField] private List<BotNavigation> botsNavigatinInScene;
    // 2. Keep track of chaos'ed bots
    [SerializeField] private List<BotNavigation> chaosedBots;
    // 3. Save a list of current Office points
    [SerializeField] private List<BotNavigation> chairPoints;
    // 4. Save a list of current activities 
    [SerializeField] private List<BotNavigation> activitiesPoints;
}
