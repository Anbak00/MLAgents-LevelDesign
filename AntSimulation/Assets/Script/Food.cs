using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int amount = 5;

    public void Bited()
    {
        amount--;
        transform.localScale = new Vector3((float)amount, (float)amount, 0f);        
        if (amount == 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Ant")
        //    Bited();
    }
}
