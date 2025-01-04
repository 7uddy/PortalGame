using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsScript : MonoBehaviour
{
    public List<GameObject> _RobotsGameObjects;

    private void Awake()
    {
        RotateRobots();
    }
    void Start()
    {
        //RotateRobots();
    }
    public void RotateRobots()
    {
        float waitTime = 0.1f;
        for (int i = 0; i < _RobotsGameObjects.Count; i++)
        {
            StartCoroutine(PlayAnimation(_RobotsGameObjects[i], "Idle", waitTime));
            waitTime += 0.2f;
        }
    }
    private IEnumerator PlayAnimation(GameObject gameObject, string animationName, float beginningDelay)
    {
        yield return new WaitForSeconds(beginningDelay); 
        Animator anim = gameObject.GetComponent<Animator>();
        anim.Play("Idle");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            RotateRobots();
        }
    }
}
