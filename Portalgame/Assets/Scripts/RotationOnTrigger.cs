using UnityEngine;

public class RotationOnTrigger : MonoBehaviour
{
    [SerializeField]private Vector3 targetRotation;
    [SerializeField]private float rotationSpeed = 1.0f;
    
    private bool isPlayerOnTrigger = false;

    public bool IsPlayerOnTrigger
    {
        get => isPlayerOnTrigger;
        set => isPlayerOnTrigger = value;
    }

    private Quaternion initialRotation;
    private Quaternion finalRotation;
    private float rotationProgress = 0f;

    void Start()
    {
        initialRotation = transform.rotation;
        finalRotation = Quaternion.Euler(targetRotation);
    }

    void Update()
    {
          if (isPlayerOnTrigger)
        {
            rotationProgress += rotationSpeed * Time.deltaTime;
        }
        else
        {
            rotationProgress -= rotationSpeed * Time.deltaTime;
        }

        rotationProgress = Mathf.Clamp01(rotationProgress);

        transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, rotationProgress);
    }
}
