using System.Collections;
using System.Collections.Generic;

public static class Shuffling 
{
    public static T[] Fisher<T>(T[] thegroup, int index)
    {
        System.Random prng = new System.Random(index);

        for(int x = 0; x < thegroup.Length -1; x++)
        {
            int rI = prng.Next(x, thegroup.Length);
            T tempItem = thegroup[rI];
            thegroup[rI] = thegroup[x];
            thegroup[x] = tempItem;

        }

        return thegroup;
    }
    
}
