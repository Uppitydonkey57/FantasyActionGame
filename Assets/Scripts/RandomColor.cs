using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private Color[] colors;

    [SerializeField] private MeshRenderer[] objects;

    // Start is called before the first frame update
    void Start()
    {
        Color color = colors[Random.Range(0, colors.Length)];

        foreach (MeshRenderer rend in objects) rend.material.color = color;
    }
}
