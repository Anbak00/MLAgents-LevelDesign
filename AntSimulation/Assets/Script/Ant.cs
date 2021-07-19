using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Ant : Agent
{
    public PheromoneMap pheromoneMap;
    public Vector3 prePos;
    public GameObject food;
    public Camera camera;
    public float removingTimeOfPheromone = 60f;
    


    Rigidbody2D rigidbody;

    float lastTimeAtHome;
    float lastTimeFindFood;

    float isBite = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lastTimeAtHome = removingTimeOfPheromone;
        lastTimeFindFood = removingTimeOfPheromone;
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeAtHome -= Time.deltaTime;
        lastTimeFindFood -= Time.deltaTime;
        if (isBite == 0.1f)
            LeaveColonyPheromone();
        else if(isBite == 0.99f)
            LeaveFoodPheromone();
    }
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Colony")
        {
            lastTimeAtHome = removingTimeOfPheromone;
            if(isBite == 0.99f)
            {
                food.SetActive(false);
                SetReward(1.0f);
                isBite = 0.1f;
                EndEpisode();
            }
        }
        if(collision.tag == "Food" && isBite == 0.1f)
        {
            lastTimeFindFood = removingTimeOfPheromone;
            SetReward(0.5f);
            isBite = 0.99f;
            food.SetActive(true);
            collision.GetComponent<Food>().Bited();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Colony")
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
        if ((int)(transform.position.x*4f) != (int)(prePos.x*4f) ||
            (int)(transform.position.y*4f) != (int)(prePos.y*4f))
            pheromoneMap.AddBlueByPos(transform.position, lastTimeAtHome / removingTimeOfPheromone);
    }
    public void LeaveFoodPheromone()
    {
        if ((int)(transform.position.x*4f) != (int)(prePos.x*4f) ||
            (int)(transform.position.y * 4f) != (int)(prePos.y*4f))
            pheromoneMap.AddGreenByPos(transform.position, lastTimeFindFood / removingTimeOfPheromone);        
    }

    //Agent
    public override void CollectObservations(VectorSensor sensor)
    {
        try
        {
            sensor.AddObservation(pheromoneMap.GetBlueByPos(transform.position + new Vector3(0.25f, 0f)));
            sensor.AddObservation(pheromoneMap.GetBlueByPos(transform.position + new Vector3(-0.25f, 0f)));
            sensor.AddObservation(pheromoneMap.GetBlueByPos(transform.position + new Vector3(0.25f, 0.25f)));
            sensor.AddObservation(pheromoneMap.GetBlueByPos(transform.position + new Vector3(0.25f, -0.25f)));
            sensor.AddObservation(pheromoneMap.GetBlueByPos(transform.position + new Vector3(0f, 0.25f)));
            sensor.AddObservation(pheromoneMap.GetBlueByPos(transform.position + new Vector3(0f, -0.25f)));
            sensor.AddObservation(pheromoneMap.GetBlueByPos(transform.position + new Vector3(-0.25f, -0.25f)));
            sensor.AddObservation(pheromoneMap.GetBlueByPos(transform.position + new Vector3(-0.25f, 0.25f)));
            sensor.AddObservation(pheromoneMap.GetGreenByPos(transform.position + new Vector3(0.25f, 0f)));
            sensor.AddObservation(pheromoneMap.GetGreenByPos(transform.position + new Vector3(-0.25f, 0f)));
            sensor.AddObservation(pheromoneMap.GetGreenByPos(transform.position + new Vector3(0.25f, 0.25f)));
            sensor.AddObservation(pheromoneMap.GetGreenByPos(transform.position + new Vector3(0.25f, -0.25f)));
            sensor.AddObservation(pheromoneMap.GetGreenByPos(transform.position + new Vector3(0f, 0.25f)));
            sensor.AddObservation(pheromoneMap.GetGreenByPos(transform.position + new Vector3(0f, -0.25f)));
            sensor.AddObservation(pheromoneMap.GetGreenByPos(transform.position + new Vector3(-0.25f, -0.25f)));
            sensor.AddObservation(pheromoneMap.GetGreenByPos(transform.position + new Vector3(-0.25f, 0.25f)));
            sensor.AddObservation(isBite);
        }
        catch
        {

            Debug.Log(transform.position);
        }

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector2 controlSignal = Vector2.zero;
        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.y = actions.ContinuousActions[1];

        SetReward(Mathf.Clamp01(pheromoneMap.GetBlueByPos(transform.position)/2f));

        rigidbody.AddForce(controlSignal);
    }

}
