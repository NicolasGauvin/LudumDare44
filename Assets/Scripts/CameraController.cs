using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Shader inverted;

    void Start()
    {
        GetComponent<Camera>().SetReplacementShader(inverted, "RenderType");
    }
}
