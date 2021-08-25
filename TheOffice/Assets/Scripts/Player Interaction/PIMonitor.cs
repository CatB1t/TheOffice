using System.Collections;
using UnityEngine;

public class PIMonitor : PIDestrutable
{
    [Header("References")]
    [SerializeField] private GameObject monitorOverlay;
    [SerializeField] private AudioSource audioSource;

    [Header("VFX")]
    [SerializeField] private ParticleSystem sparkleVFX;
    [SerializeField] private ParticleSystem fireVFX;

    [Header("SFX")]
    [SerializeField] private AudioClip sparkleSFX;
    [SerializeField] private AudioClip fireSFX;

    protected override void Destruct() 
    {
        monitorOverlay.SetActive(false);
        StartCoroutine(PlayVFX());
    }
    
    IEnumerator PlayVFX()
    {
        audioSource.PlayOneShot(sparkleSFX);
        sparkleVFX.Play();
        yield return new WaitForSeconds(1.5f);
        Destroy(sparkleVFX.gameObject);
        audioSource.loop = true;
        audioSource.clip = fireSFX;
        audioSource.Play();
        fireVFX.Play();
    }
    
}
