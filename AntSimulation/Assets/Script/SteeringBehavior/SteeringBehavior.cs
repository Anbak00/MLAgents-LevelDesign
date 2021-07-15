using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SteeringBehavior : MonoBehaviour
{
    protected Rigidbody2D rigidbody;
    public float maxSpeed = 10f;
    public bool isOnDrawVectorLine = false;

    public virtual void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    public abstract Vector3 GetSteeringForce();

    public virtual void Update()
    {
        
        rigidbody.AddForce(GetSteeringForce());
        if (isOnDrawVectorLine) DrawDebugLine();
    }
    
    public virtual void DrawDebugLine() { }
    protected void DrawDebugCircle(Vector3 pos, float radius, Color color)
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
