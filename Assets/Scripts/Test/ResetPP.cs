using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPP : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
