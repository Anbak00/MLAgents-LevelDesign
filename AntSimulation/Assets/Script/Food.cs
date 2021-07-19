using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int amount = 10;

    public void Bited()
    {
        amount--;
        transform.localScale = new Vector3(5f*(float)amount/30f, 5f*(float)amount/30f, 0f);        
        if (amount == 0)
        {
            amount = 10;
            Vector3 pos = new Vector3();
            pos.x = Random.Range(0f, 1f);
            pos.y = Random.Range(0f, 1f);
            transform.position = Camera.main.ViewportToWorldPoint(pos); 
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Ant")
        //    Bited();
    }
}
