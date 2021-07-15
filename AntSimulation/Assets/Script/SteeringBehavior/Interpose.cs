using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interpose : SteeringBehavior
{

    public Transform[] targetTramsfoms;
    private Rigidbody[] targetRigidbodys;

    private Arrive arrive = new Arrive();
    private Rigidbody targetRigidbody;
    private Rigidbody target2Rigidbody;

    private Vector2 df;
    private Vector2 sf;
    private float breakSpeed = 0.3f;

    public override void Start()
    {
        base.Start();


        targetRigidbodys = new Rigidbody[targetTramsfoms.Length];
        for (int i = 0; i < targetTramsfoms.Length; i++)
        {
            targetRigidbodys[i] = targetTramsfoms[i].GetComponent<Rigidbody>();
        }

    }

    public override Vector3 GetSteeringForce()
    {

        Vector3 midPoint = new Vector3();
        for (int i = 0; i < targetTramsfoms.Length; i++)
        {
            midPoint += targetTramsfoms[i].position;
        }
        midPoint /= targetTramsfoms.Length;

        float timeToReachMidPoint = (transform.position - midPoint).magnitude / maxSpeed;

        Vector3 pos = new Vector3();
        for(int i = 0; i < targetTramsfoms.Length; i++)      
            pos += targetRigidbodys[i].velocity;       
        pos *= timeToReachMidPoint;
        for (int i = 0; i < targetTramsfoms.Length; i++)
            pos += targetTramsfoms[i].position;

        midPoint = pos / targetTramsfoms.Length;

        //arrive 
        Vector3 toTarget = midPoint - transform.position;
        float dist = toTarget.magnitude;
        float speed = dist / breakSpeed;
        speed = (speed > maxSpeed) ? maxSpeed : speed;
        df = toTarget / dist * speed;
        sf = df - rigidbody.velocity;

        return sf;
    }

    public override void DrawDebugLine()
    {
        //DrawDebugCircle(midPoint, 0.1f, Color.black);
        Debug.DrawLine(transform.position, (Vector2)transform.position + df, Color.blue);
        Debug.DrawLine(transform.position, (Vector2)transform.position + sf, Color.red);
        Debug.DrawLine(transform.position, (Vector2)transform.position + rigidbody.velocity, Color.yellow);
    }
}
