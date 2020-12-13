using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchController : MonoBehaviour
{
    public Light2D torchLight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        torchLight.intensity = Mathf.PingPong(Time.time, 1f);
    }
}
