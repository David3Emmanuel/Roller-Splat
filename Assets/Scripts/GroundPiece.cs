using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
    private Material material;

    public bool isColored = false;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public void ChangeColor(Color color)
    {
        material.color = color;
        isColored = true;
        GameManager.instance.CheckComplete();
    }
}
