using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingLevel : MonoBehaviour
{

    List<locationscd> thefullloc;
    Queue<locationscd> mixedlocations;
    Queue<locationscd> mixedavailablelocations;
    locationscd playerspawnpoint;
    public string fulllevel = "Complete Level";
    public Transform prefgrid;
    public Transform[] hurdles;
    Transform hurdlepref;
    public Vector2 gridscale;
    [Range(0, 5)]
    public float borders;
    public int index = 15;
    int randomh;
    int stop = 0;
    Transform[,] sectoronlevel;

    [Range(0, 1)]
    public float hurdlepercent;

    public void LevelCreation()
    {
        sectoronlevel = new Transform[(int)gridscale.x, (int)gridscale.y]; 
      

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

        playerspawnpoint = new locationscd((int)gridscale.x /2, (int)gridscale.y /2);



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
                sectoronlevel[xaxis, yaxis] = thegridsection;
            }
        }

        bool[,] hurdlelocations = new bool[(int)gridscale.x, (int)gridscale.y];
        int numberofhurdle = (int)(gridscale.x * gridscale.y * hurdlepercent);
        int howmany = 0;
        List<locationscd> everyavailable = new List<locationscd> (thefullloc);
        for(int check = 0; check<numberofhurdle; check++)
        {
            randomh = Random.Range(0, 2);
            for (int i = 0; i < 3; i++)
            {
                if (randomh == i)
                {
                    hurdlepref = hurdles[i];
                }
            }
            locationscd rndlocation = atrandomloc();
            hurdlelocations[rndlocation.x, rndlocation.y] = true;
            howmany++;

            if (rndlocation != playerspawnpoint)
            {
                Vector3 hurdleloc = locationtopos(rndlocation.x, rndlocation.y);
                Transform newhurdle = Instantiate(hurdlepref, hurdleloc + Vector3.up * 0.50f, Quaternion.identity) as Transform;
                newhurdle.parent = thecompleted;

                everyavailable.Remove(rndlocation);
            }

            else
            {
                hurdlelocations[rndlocation.x, rndlocation.y] = false;
                howmany--;


            }


        }

        mixedavailablelocations = new Queue<locationscd>(Shuffling.Fisher(everyavailable.ToArray(), index));


    }

    public void Start()
    {
        LevelCreation();
        hurdlepercent = Random.Range(0.1f, 0.26f);
        index = Random.Range(50, 57);
    }

    public void Update()
    {
        if (stop == 0)
        {
            LevelCreation();
            Debug.Log("randomised");
            stop = 1;
        }
       
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


        public static bool operator ==(locationscd parta, locationscd part2)
        {
            return parta.x == part2.x && parta.y == part2.y;
        }

        public static bool operator !=(locationscd parta, locationscd part2)
        {
            return !(parta == part2);
        }


    }

         

    public locationscd atrandomloc()
    {
        locationscd randloc = mixedlocations.Dequeue();
        mixedlocations.Enqueue(randloc);
        return randloc;
    }

    public Transform openatrandom()
    {
        locationscd randloc = mixedavailablelocations.Dequeue();
        mixedavailablelocations.Enqueue(randloc);
        return sectoronlevel[randloc.x, randloc.y];

    }

}
