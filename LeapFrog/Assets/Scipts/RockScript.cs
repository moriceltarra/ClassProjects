using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [SerializeField] GameObject[] rocks;
    // Start is called before the first frame update
    void Start()
    {    
        ColocarRocas();
    }

    void ColocarRocas()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            rocks[i].transform.position = new Vector3(((i+1)*1.75f)+(-8), -3f , 1f);
        }
    }
}
