using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    
    public int KillsHS;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
