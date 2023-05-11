using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
