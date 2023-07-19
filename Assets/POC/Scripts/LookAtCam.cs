using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main; 
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(true)
        {
            transform.LookAt(cam.transform);
            yield return null;
        }
    }

}
