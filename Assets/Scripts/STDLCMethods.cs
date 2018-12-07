using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STDLCMethods
{
    public static bool FindInList(GameObject obj, List<GameObject> list)
    {
        foreach (GameObject selObj in list)
        {
            if (selObj == obj)
                return true;
        }
        return false;
    }
}
