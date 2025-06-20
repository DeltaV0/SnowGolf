using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objs;
    // Start is called before the first frame update
    void Start()
    {
        //stupid ass spawning system because github cant handle files bigger than 100 mb ffs
        for (int i = 0; i < objs.Length; i++) {
            GameObject bullet = (GameObject)Instantiate(objs[i], new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
