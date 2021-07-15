using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wander : SteeringBehavior
{
    public float radius = 2f;
    public float distance = 5f;
    public float jitter = 0.5f;

    private Vector2 targetPos = new Vector3();
    private Vector2 worldTarget = new Vector3();

    private Vector2 df;
    private Vector2 sf;

    public override void Start()
    {
        base.Start();
        float r = Random.Range(0f, Mathf.PI * 2f);        
        targetPos.x = Mathf.Cos(r) * radius;
        targetPos.y = Mathf.Sin(r) * radius;

    }


    public override Vector3 GetSteeringForce()
    { 
        targetPos.x += (Random.Range(-1f, 1f) * jitter);
        targetPos.y += (Random.Range(-1f, 1f) * jitter);        

        targetPos.Normalize();
        targetPos *= radius;

        Vector2 localTarget = targetPos + new Vector2(0f, distance);

        worldTarget = Matrix4x4.Rotate(transform.rotation) * localTarget;
        worldTarget += (Vector2)transform.position;

        

        df = (worldTarget - (Vector2)transform.position).normalized * maxSpeed;
        sf = df - rigidbody.velocity;

        return sf;
    }

    public override void DrawDebugLine()
    { 
        DrawDebugCircle(worldTarget, 0.2f,Color.red);
        DrawDebugCircle(transform.position + transform.up * distance, radius,Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + df, Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + sf, Color.red);
        Debug.DrawLine(transform.position, (Vector2)transform.position + rigidbody.velocity, Color.yellow);
    }
}