using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Seek : SteeringBehavior
{
    public Vector2 targetPos;

    private Vector2 df = new Vector3(0f, 0f, 0f);
    private Vector2 sf = new Vector3(0f, 0f, 0f);

    public override Vector3 GetSteeringForce()
    {
        df = (targetPos - (Vector2)transform.position).normalized * maxSpeed;
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