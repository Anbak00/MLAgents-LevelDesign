using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosToEdge : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));        
    }
}
