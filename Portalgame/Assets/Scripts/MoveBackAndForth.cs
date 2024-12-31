using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MoveBackAndForth : MonoBehaviour
{
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float waitTime = 2.0f;

    private bool movingToB = false;
    public bool MovingToB=>movingToB;
    private bool isMoving = false;
    public bool IsMoving=>isMoving;
    public bool IsOnPointA=>transform.position==pointA;

    [SerializeField] private bool MOVE_ON_UPDATE=true;

    void Update()
    {
        if (!isMoving && MOVE_ON_UPDATE)
        {
            if (movingToB)
            {
                StartCoroutine(MoveToPoint(pointB, pointA));
            }
            else
            {
                StartCoroutine(MoveToPoint(pointA, pointB));
            }
        }
    }
    private IEnumerator MoveToPoint(Vector3 from, Vector3 to)
    {
        float time = 0;
        movingToB = to == pointB;
        isMoving = true;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(from, to, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        transform.position = to;
        yield return new WaitForSeconds(waitTime);
        isMoving = false;
    }

    public IEnumerator MoveToPointA()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToPoint(transform.position, pointA));
        }
        yield return null;
    }

    public IEnumerator MoveToPointB()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToPoint(transform.position, pointB));
        }
        yield return null;
    }
}
