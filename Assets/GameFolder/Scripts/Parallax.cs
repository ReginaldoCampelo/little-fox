using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer mesh;

    [Header("Parallax em X")]
    [Range(0.1f, 10f)]
    [Tooltip("Quanto menor o valor maior o parallax em X")]
    public float xParallax = 10f;

    [Header("Parallax em Y")]
    [Range(0f, 5f)]
    [Tooltip("Quanto maior o valor maior o parallax em Y")]
    public float yParallax = 0f;
    private float yPos;
    private Vector2 startPos;
    private float horizontal;


    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    void LateUpdate()
    {
        mesh.material.mainTextureOffset = new Vector2(Camera.main.transform.position.x / (xParallax * 10f), 0f);
        horizontal = (Camera.main.transform.position.y * (yParallax / 10f));
        yPos = startPos.y + (Camera.main.transform.position.y - horizontal);

        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
