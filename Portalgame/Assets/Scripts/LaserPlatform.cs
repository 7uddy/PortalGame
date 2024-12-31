using UnityEngine;
using UnityEngine.UIElements;

public class LaserPlatform : MonoBehaviour
{
    [SerializeField] private MoveBackAndForth LaserScript;
    [SerializeField] private RotationOnTrigger PlatformScript;

    private bool IsLaserInPosition=false;
    private bool IsPlayerInPosition=false;

    private void Update()
    {
        if(LaserScript.IsOnPointA)
        {
            IsLaserInPosition=true;
        }
        else
        {
            IsLaserInPosition=false;
        }

        if (IsPlayerInPosition && IsLaserInPosition)
        {
            PlatformScript.IsPlayerOnTrigger = true;
        }
        else
        {
            PlatformScript.IsPlayerOnTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInPosition = false;
        }
    }
}
