using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] public Cell Up;
    [SerializeField] public Cell Down;
    [SerializeField] public Cell Left;
    [SerializeField] public Cell Right;

    public Tile Tile { get; set; }
}
