using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public PheromoneMap pheromoneMap;

    public Vector3 prePos;

    float lastTimeAtHome = 30f;
    float lastTimeFindFood = 30f;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeAtHome -= Time.deltaTime;
        LeaveColonyPheromone();
        //stateMachine.Excute(this);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Colony")
        {
        }
        if(collision.tag == "Food")
        {
            collision.GetComponent<Food>().Bited();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Colony")
        {            
        }
        if (collision.tag == "Food")
        {
        }
    }

    private void LateUpdate()
    {
        prePos = transform.position;
    }

    public void LeaveColonyPheromone()
    {
        if ((int)(transform.position.x * 4f) != (int)(prePos.x * 4f) ||
            (int)(transform.position.y * 4f) != (int)(prePos.y * 4f))
            pheromoneMap.SetBlueByPos(transform.position, lastTimeAtHome / 30f);
    }
    public void LeaveFoodPheromone()
    {
        if ((int)(transform.position.x * 4f) != (int)(prePos.x * 4f) ||
            (int)(transform.position.y * 4f) != (int)(prePos.y * 4f))
            pheromoneMap.SetGreenByPos(transform.position, lastTimeAtHome / 30f);
    }    
}
