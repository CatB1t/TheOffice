using UnityEngine;
using System.Collections;

public class PITrash : PIDestrutable
{
    [SerializeField] private GameObject propToThrowOut;
    [SerializeField] private int _maxCount = 5;
    [SerializeField] private Vector3 spawnOffset;

    protected override void Destruct()
    {
        StartCoroutine(ThrowTrashOut());
        Invoke("PlayAudio", 1f);
    }

    IEnumerator ThrowTrashOut()
    {
        GameObject[] objs = new GameObject[_maxCount];
        for (int i = 1; i < _maxCount; i++)
        {
            objs[i-1] = SpawnObjectPushUp();
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(5);

        foreach (var obj in objs)
            Destroy(obj);

        yield return null;
    }

    private GameObject SpawnObjectPushUp()
    {
        GameObject obj = Instantiate(propToThrowOut, transform.position + spawnOffset, Random.rotation, transform);
        Rigidbody rb = obj.AddComponent<Rigidbody>();
        Vector3 randomDirection = new Vector3(Random.Range(0, .5f), Random.Range(1,2f), Random.Range(0, .5f));
        rb.AddForce(randomDirection * Random.Range(2,5f), ForceMode.Impulse);
        return obj;
    }
    private void PlayAudio() => GetComponent<AudioSource>().Play();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + spawnOffset, .1f);
    }
}
