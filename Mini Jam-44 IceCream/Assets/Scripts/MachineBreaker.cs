using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MachineBreaker : MonoBehaviour
{

    public float minBreakInterval = 30f;
    public float maxBreakInterval = 60f;

    public float breakIntervalModifier;

    private float timer;

    public List<StandManager> allMachines = new List<StandManager>();


    void Start()
    {
        StandManager[] found = FindObjectsByType<StandManager>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < minBreakInterval)
        {

        }
    }
}
