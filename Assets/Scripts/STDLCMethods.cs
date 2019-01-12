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
    public static bool FindInList(AudioClip obj, List<AudioClip> list)
    {
        foreach (AudioClip selObj in list)
        {
            if (selObj == obj)
                return true;
        }
        return false;
    }
}
