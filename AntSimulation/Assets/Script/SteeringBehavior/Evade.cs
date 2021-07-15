using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Evade : SteeringBehavior
{
    public Transform targetTransform;
    public float offDistance = 5f;
    
    private Rigidbody targetRigidbody;

    private Vector2 df;
    private Vector2 sf;

    public override Vector3 GetSteeringForce()
    {
        Vector3 toTarget = targetTransform.position - transform.position;

        float lookAheadTime = toTarget.magnitude / (maxSpeed + targetTransform.GetComponent<Rigidbody>().velocity.magnitude);

        //flee
        if (offDistance * offDistance > (transform.position - targetTransform.position).sqrMagnitude)
        {
            df = (transform.position - targetTransform.position).normalized * maxSpeed;
            sf = df - rigidbody.velocity;
            return sf;
        }

        return Vector3.zero;
    }

    public override void DrawDebugLine()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + rigidbody.velocity, Color.yellow);
        Debug.DrawLine(transform.position, (Vector2)transform.position + df, Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + sf, Color.red);
    }
}