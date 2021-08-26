using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PIWaterCooler : PIDestrutable
{
    [SerializeField] private ParticleSystem waterParticleSystem;
    
    protected override void Destruct()
    {
        waterParticleSystem.Play();
        // play SFX
        GetComponent<AudioSource>().Play();
    }
}
