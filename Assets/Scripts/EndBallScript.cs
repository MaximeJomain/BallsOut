using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBallScript : MonoBehaviour
{
    public Material blueMaterial, redMaterial;

    private MeshRenderer meshRenderer;

    
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material = PlayerPrefs.GetInt("PlayerColor", 0) == 0 ? blueMaterial : redMaterial;
    }
}
