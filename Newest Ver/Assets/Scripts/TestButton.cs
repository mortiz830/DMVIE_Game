using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    [SerializeField]
    RaceBar bar;
    public void TestClick()
    {
        bar.UpdateProgress(0.1f);
    }
}
