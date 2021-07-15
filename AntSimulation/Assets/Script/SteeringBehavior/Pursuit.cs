using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pursuit : SteeringBehavior
{
    public Transform targetTransform;

    private Rigidbody targetRigidbody;
    private Seek seek = new Seek();

    private Vector2 df;
    private Vector2 sf;

    public override void Start()
    {
        base.Start();
        targetRigidbody = targetTransform.GetComponent<Rigidbody>();
    }

    public override Vector3 GetSteeringForce()
    {
        Vector3 toTarget = targetTransform.position - transform.position;
        float lookAheadTime = toTarget.magnitude/(maxSpeed + targetRigidbody.velocity.magnitude);

        //seek
        Vector3 target = targetTransform.position + targetRigidbody.velocity * lookAheadTime;
        df = (target - transform.position).normalized * maxSpeed;
        sf = df - rigidbody.velocity;

        return sf;
           
    }

    public override void DrawDebugLine()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + df, Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + sf, Color.red);
        Debug.DrawLine(transform.position, (Vector2)transform.position + rigidbody.velocity, Color.yellow);
    }
}