using UnityEngine;

public class PIVendingMachine : PIDestrutable
{
    [SerializeField] private ParticleSystem particleSystemRef;
    protected override void Destruct()
    {
        particleSystemRef.Play();
        GetComponent<AudioSource>().Play();
    }
}
