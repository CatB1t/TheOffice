using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PIPainting : PIDestrutable
{
    private bool _hasPlayedSound = false;
    protected override void Destruct()
    {
        Rigidbody rigRef = GetComponent<Rigidbody>();
        rigRef.isKinematic = false;
        rigRef.AddRelativeTorque(new Vector3(0, Random.Range(-5, 5), 0) * 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!_hasPlayedSound && !base.IsValid && collision.collider.CompareTag("Ground"))
        {
            GetComponent<AudioSource>().Play();
            _hasPlayedSound = true;
        }
    }

}
