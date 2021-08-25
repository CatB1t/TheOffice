using UnityEngine;

[RequireComponent(typeof(SeekPlayer))]
public class Boss : BotBrain
{
    private SeekPlayer _seekPlayer;

    protected override void Start()
    {
        _seekPlayer = GetComponent<SeekPlayer>();
        base.Start();
    }

    protected override void OnLeaveBase()
    {
        base.OnLeaveBase();
        _seekPlayer.IsSeeking = true;
    }

    protected override void OnLeaveNotBase()
    {
        base.OnLeaveNotBase();
        _seekPlayer.IsSeeking = false;
    }

}
