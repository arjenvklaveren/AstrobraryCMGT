using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBodyManager : MonoBehaviour
{
    public static SpaceBodyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    void Update()
    {
        
    }
}
