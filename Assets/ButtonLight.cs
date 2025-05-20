using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLight : MonoBehaviour
{
    public button Button;

    public GameObject onobj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Button.Pressed){
            onobj.SetActive(true);
        } else {
            onobj.SetActive(false);
        }
    }
}
