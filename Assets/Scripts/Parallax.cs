using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Parallax : MonoBehaviour
{
    public float speed = 0.2f;

    [SerializeField]
    private Renderer bgRenderer;
    

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
