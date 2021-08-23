using UnityEngine;

public class PIMonitor : PIDestrutable
{
    protected override void Destruct()
    {
        Debug.Log("Destructed ouch!");
    }
}
