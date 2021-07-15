using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Hide : SteeringBehavior
{
    public GameObject hidingSpots;
    public Transform target;
    public float distFromHidingSpot = 1f;

    private Rigidbody targetRigidbody;

    private Transform [] spotTransforms;
    private Vector3 selectedSpot;
    private Vector3 debugPos;

    private Vector2 df;
    private Vector2 sf;

    public override void Start()
    {
        base.Start();
        targetRigidbody = target.GetComponent<Rigidbody>();
        spotTransforms = new Transform[hidingSpots.transform.childCount];

        for (int i = 0; i < hidingSpots.transform.childCount; i++)
        {
            spotTransforms[i] = hidingSpots.transform.GetChild(i);
        }
    }

    private Vector3 GetHidingSpot()
    {
        Transform nearestSpot = null;

        float nearestdist = float.MaxValue;
        foreach (Transform spot in spotTransforms)
        {
            float dist = Vector3.Distance(transform.position, spot.position);
            if (nearestdist > dist)
            {
                nearestdist = dist;
                nearestSpot = spot;
            }
        }

        if(nearestdist > Vector3.Distance(transform.position, target.position))
        {
            return Vector3.zero;
        }

        Debug.Log(nearestSpot);
        selectedSpot = nearestSpot.position;
        Vector3 hidingSpot = (nearestSpot.position - target.position).normalized;
        hidingSpot *= (nearestSpot.localScale.x/2f + distFromHidingSpot);
        hidingSpot += nearestSpot.position;

        return hidingSpot;
    }

    public override Vector3 GetSteeringForce()
    {
        debugPos = GetHidingSpot();

        if (debugPos == Vector3.zero)
            return GetEvadeForce(target);

        return GetArriveForce(debugPos);
    }

    private Vector3 GetEvadeForce(Transform targetTransform)
    {
        Vector3 toTarget = target.position - transform.position;

        float lookAheadTime = toTarget.magnitude / (maxSpeed + targetRigidbody.velocity.magnitude);

        df = (transform.position - target.position).normalized * maxSpeed;
        sf = df - rigidbody.velocity;
        return sf;
    }
    private Vector3 GetArriveForce(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;
        float speed = dist / 0.3f;
        speed = (speed > maxSpeed) ? maxSpeed : speed;
        df = toTarget / dist * speed;
        sf = df - rigidbody.velocity;

        return sf;
    }

    public override void DrawDebugLine()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + df, Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + sf, Color.red);
        Debug.DrawLine(transform.position, (Vector2)transform.position + rigidbody.velocity, Color.yellow);
        DrawDebugCircle(debugPos, 0.2f, Color.black);
        DrawDebugCircle(selectedSpot, 0.2f, Color.red);
    }
}
