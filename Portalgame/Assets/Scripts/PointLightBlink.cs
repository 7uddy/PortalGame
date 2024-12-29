using UnityEngine;

public class PointLightBlink : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 1.0f;
    private Light pointLight;
    private float timer;

    void Start()
    {
        pointLight = GetComponent<Light>();

        if (pointLight == null)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            pointLight.enabled = !pointLight.enabled;
            timer = 0f;
        }
    }
}
