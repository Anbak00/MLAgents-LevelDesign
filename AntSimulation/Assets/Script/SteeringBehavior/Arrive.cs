using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Arrive : SteeringBehavior
{
    public float breakSpeed = 0.3f;
    public Transform targetTransform;
    public float arriveRnage = 1f;
    public bool isArrive = false;

    private Vector2 df = new Vector3(0f, 0f, 0f);
    private Vector2 sf = new Vector3(0f, 0f, 0f);

    public override Vector3 GetSteeringForce()
    {
        Vector3 toTarget = targetTransform.position - transform.position;
        float dist = toTarget.magnitude;
        float speed = dist / breakSpeed;
        speed = (speed > maxSpeed) ? maxSpeed : speed;
        df = toTarget / dist * speed;
        sf = df - rigidbody.velocity;

        if ((transform.position - targetTransform.position).sqrMagnitude <= arriveRnage * arriveRnage)
            isArrive = true;
        else
            isArrive = false;

        return sf;
        //try
        //{
        //
        //}
        //catch
        //{
        //    enabled = false;
        //    isArrive = false;
        //    return new Vector3();
        //}

    }

    public override void DrawDebugLine()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + df, Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + sf, Color.red);
        Debug.DrawLine(transform.position, (Vector2)transform.position + rigidbody.velocity, Color.yellow);
    }
}