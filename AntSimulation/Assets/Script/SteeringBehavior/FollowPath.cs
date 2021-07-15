using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seek))]
public class FollowPath : MonoBehaviour
{
    public Transform pathSpots;
    public bool isOnDrawVectorLine = false;
    public float arriveRange = 1f;

    private Seek seek;
    private int indexCount;

    // Start is called before the first frame update
    void Start()
    {
        //seek = transform.GetComponent<Seek>();
        //seek.targetTransform = pathSpots.GetChild(0);
        //indexCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if ((transform.position - seek.targetTransform.position).sqrMagnitude <= (arriveRange * arriveRange))
        //{
        //    indexCount++;
        //    seek.targetTransform = pathSpots.GetChild(indexCount % pathSpots.childCount);
        //}
            
        //if (isOnDrawVectorLine) DrawDebugLine();
    }

    private void DrawDebugLine()
    {
        for (int i = 0; i < pathSpots.childCount; i++)
        {
            DrawDebugCircle(pathSpots.GetChild(i).position, arriveRange, Color.red);
        }
    }

    private void DrawDebugCircle(Vector3 pos, float radius, Color color)
    {
        Vector3 start = new Vector3();
        start.x = Mathf.Sin(0f);
        start.y = Mathf.Cos(0f);

        Vector3 end = new Vector3();
        for (int i = 1; i <= 20; i++)
        {
            end.x = Mathf.Sin(2f * Mathf.PI * i / 20);
            end.y = Mathf.Cos(2f * Mathf.PI * i / 20);
            Debug.DrawLine(start * radius + pos, end * radius + pos, color);
            start = end;
        }
    }
}
