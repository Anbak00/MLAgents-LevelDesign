using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Flee : SteeringBehavior
{
    public float offDistance = 5f;
    public Transform targetTransform;

    private Vector2 df = new Vector3(0f, 0f, 0f);
    private Vector2 sf = new Vector3(0f, 0f, 0f);

    public override Vector3 GetSteeringForce()
    {
        if (offDistance * offDistance > (transform.position - targetTransform.position).sqrMagnitude)
        {
            df = (transform.position - targetTransform.position).normalized * maxSpeed;
            sf = df - rigidbody.velocity;
            return sf;
        }
        return new Vector3();
    }

    public override void DrawDebugLine()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + rigidbody.velocity, Color.yellow);
        Debug.DrawLine(transform.position, (Vector2)transform.position + df, Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + sf, Color.red);
        
    }
}
