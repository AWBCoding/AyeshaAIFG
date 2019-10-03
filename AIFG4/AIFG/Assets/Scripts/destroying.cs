using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroying : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FirstEnemy.spawned == true)
        {
            Destroy(gameObject, 3);
        }
    }
}
