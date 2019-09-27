using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingLevel : MonoBehaviour
{
    List<locationscd> thefullloc;
    Queue<locationscd> mixedlocations;
    public string fulllevel = "Complete Level";
    public Transform prefgrid;
    public Transform hurdlepref;
    public Vector2 gridscale;
    [Range(0, 5)]
    public float borders;
    public int index = 15; 



    public void LevelCreation()
    {
        if (transform.Find(fulllevel))
        {
            DestroyImmediate(transform.Find(fulllevel).gameObject);
        }

        thefullloc = new List<locationscd>();
        for (int xaxis = 0; xaxis < gridscale.x; xaxis++)
        {
            for (int yaxis = 0; yaxis < gridscale.y; yaxis++)
            {
                thefullloc.Add(new locationscd(xaxis, yaxis));
            }

        }


        mixedlocations = new Queue<locationscd>(Shuffling.Fisher(thefullloc.ToArray(), index));
        Transform thecompleted = new GameObject(fulllevel).transform;
        thecompleted.parent = transform;

        for (int xaxis = 0; xaxis<gridscale.x; xaxis++)
        {
            for (int yaxis = 0; yaxis < gridscale.y; yaxis++)
            {
                Vector3 SectionLocation = locationtopos(xaxis, yaxis);
                Transform thegridsection = Instantiate(prefgrid, SectionLocation, Quaternion.Euler(Vector3.right * 90)) as Transform;
                thegridsection.parent = thecompleted;
                thegridsection.localScale = Vector3.one * (1 - borders);

            }
        }

        int numberofhurdle = 15;
        for(int check = 0; check<numberofhurdle; check++)
        {
            locationscd rndlocation = atrandomloc();
            Vector3 hurdleloc = locationtopos(rndlocation.x, rndlocation.y);
            Transform newhurdle = Instantiate(hurdlepref, hurdleloc + Vector3.up * 0.50f, Quaternion.identity) as Transform;
            newhurdle.parent = thecompleted;
        }


    }

    public void Start()
    {
        LevelCreation();
    }

    Vector3 locationtopos(int x, int y)
    {
        return new Vector3(-gridscale.x / 2 + 0.50f + x, 0, -gridscale.y + 0.50f + y);
    }

    public struct locationscd
    {
        public int x;
        public int y;

        public locationscd(int varx, int vary)
        {
            x = varx;
            y = vary;
        }
    }


    public locationscd atrandomloc()
    {
        locationscd randloc = mixedlocations.Dequeue();
        mixedlocations.Enqueue(randloc);
        return randloc;
    }

}
