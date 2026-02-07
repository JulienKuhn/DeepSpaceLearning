using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class DoorController : MonoBehaviour
{
    [SerializeField] private DoorType type;
    [SerializeField] private Transform leftDoor, leftDoorClosePoint, leftDoorOpenPoint;
    [SerializeField] private Transform rightDoor, rightDoorClosePoint, rightDoorOpenPoint;
    [SerializeField] private float doorOpeningSpeed;

    private Tweener leftAnimationTweener = null;
    private Tweener rightAnimationTweener = null;

    private enum DoorType
    {
        Accessible,
        Locked,
        Blue,
        Green,
        Red,
    }

    public void OpenDoor()
    {
        StopAnimations();
        float distance = Vector3.Distance(leftDoor.position, leftDoorOpenPoint.position);
        leftAnimationTweener = leftDoor.DOMove(leftDoorOpenPoint.position, doorOpeningSpeed * distance);
        rightAnimationTweener = rightDoor.DOMove(rightDoorOpenPoint.position, doorOpeningSpeed * distance);
    }

    public void ClostDoor()
    {
        StopAnimations();
        float distance = Vector3.Distance(leftDoor.position, leftDoorClosePoint.position);
        leftAnimationTweener = leftDoor.DOMove(leftDoorClosePoint.position, doorOpeningSpeed * distance);
        rightAnimationTweener = rightDoor.DOMove(rightDoorClosePoint.position, doorOpeningSpeed * distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CanOpen())
            OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && CanOpen())
            ClostDoor();
    }

    private void StopAnimations()
    {
        if (leftAnimationTweener != null && leftAnimationTweener.IsPlaying())
            leftAnimationTweener.Kill();


        if (rightAnimationTweener != null && rightAnimationTweener.IsPlaying())
            rightAnimationTweener.Kill();
    }

    private bool CanOpen()
    {
        switch (type)
        {
            case DoorType.Accessible:
                return true;
            case DoorType.Locked:
                return false;
            case DoorType.Blue:
                return GameManager.instance.currentSave.BlueCardAquired;
            case DoorType.Green:
                return GameManager.instance.currentSave.GreenCardAquired;
            case DoorType.Red:
                return GameManager.instance.currentSave.RedCardAquired;
        }
        return false;
    }

}
