using UnityEngine;

public class PIMonitor : PIDestrutable
{
    protected override void Destruct()
    {
        points = 100;
    }
}
