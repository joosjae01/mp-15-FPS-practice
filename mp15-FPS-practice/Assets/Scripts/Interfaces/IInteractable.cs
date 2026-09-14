using UnityEngine;

public interface IInteractable
{
    public GameObject GameObject { get; }

    public void Targeting();
    public void Untargeting();
    public void Interact(IInteractor owner);
}
