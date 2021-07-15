using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OffsetPursuit : SteeringBehavior 
{
    public float radius;
    public Vector3 rangePos;
    public Transform leaderTransform;
    private Rigidbody leaderRigidbody;

    private Vector3 offset = new Vector3();
    private Vector3 worldOffset = new Vector3();

    private Vector2 df;
    private Vector2 sf;
    private float breakSpeed = 0.3f;

    public override void Start()
    {
        base.Start();
        if (leaderTransform) leaderRigidbody = leaderTransform.GetComponent<Rigidbody>();

        offset = Random.insideUnitCircle * radius;
        offset.z = transform.position.z;

        offset += rangePos;
    }

    public override Vector3 GetSteeringForce()
    {
        worldOffset = Matrix4x4.Rotate(leaderTransform.rotation) * offset;
        worldOffset += leaderTransform.position;

        float lookAheadTime = 
            offset.magnitude / (maxSpeed + leaderRigidbody.velocity.magnitude);

        Vector3 target = worldOffset + leaderRigidbody.velocity;

        //arrive
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;
        float speed = dist / breakSpeed;
        speed = (speed > maxSpeed) ? maxSpeed : speed;
        df = toTarget / dist * speed;
        sf = df - rigidbody.velocity;

        return sf;
    }

    public override void DrawDebugLine()
    {
        Vector3 worldRangePos = Matrix4x4.Rotate(leaderTransform.rotation) * rangePos;
        worldRangePos += leaderTransform.position;

        DrawDebugCircle(worldRangePos, radius, Color.green);
        DrawDebugCircle(worldOffset, 0.1f, Color.black);
        Debug.DrawLine(transform.position, (Vector2)transform.position + df, Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + sf, Color.red);
        Debug.DrawLine(transform.position, (Vector2)transform.position + rigidbody.velocity, Color.yellow);
    }
}