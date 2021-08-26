using UnityEngine;

public class PIDoor : PlayerInteractable
{
    private Animator _animator;
    private AudioSource _audioSource;
    private bool isOpen = false;
    override protected void Start()
    {
        base.Start();
        _displayMessage = "Open";
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    override public void Interact(PlayerController controller)
    {
        _animator.SetTrigger("ToggleDoor");
        _audioSource.Play();
        ToggleMessage();
    }

    private void ToggleMessage()
    {
        if(isOpen)
            _displayMessage = "Open";
        else
            _displayMessage = "Close";
        isOpen = !isOpen;
    }
}
