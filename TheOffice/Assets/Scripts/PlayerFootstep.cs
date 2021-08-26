using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    [SerializeField] private AudioSource footstepsSource;
    [SerializeField] private AudioClip[] footstepsClips;
    private PlayerController _playerController;
    private int count = 1;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_playerController.IsMoving && !footstepsSource.isPlaying)
        {
            footstepsSource.PlayOneShot(footstepsClips[GetRandomClipIndex()]);
            footstepsSource.pitch = Random.Range(0.8f, 1f);
        }
    }

    private int GetRandomClipIndex()
    {
        count++;
        count = count % footstepsClips.Length + 1;
        return count-1;
    }
}
