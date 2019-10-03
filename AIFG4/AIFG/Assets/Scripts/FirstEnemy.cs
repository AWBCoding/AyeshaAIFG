using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : MonoBehaviour
{
    public GameObject enemy;
    GameObject enempref;
    GeneratingLevel GL;
    public static bool spawned = false;
    float YPOS = 0.668f;
 

    // Start is called before the first frame update
    void Start()
    {
        GL = FindObjectOfType<GeneratingLevel>();
        enemy = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Transform RANDO = GL.openatrandom();
            
            
            enempref = Instantiate(enemy, RANDO.position + Vector3.up, Quaternion.identity);
            
            Debug.Log("Spawned");
            spawned = true;

            
            
            
        }

    
        
        
        
    }

   
}
