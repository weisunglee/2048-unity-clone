using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileSpawner : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform[] cellTransforms;
    [SerializeField] private UnityEvent<int, Tile> tileSpawned;

    private const float number2Chance = 0.6f;        
    
    private int GenerateNumber()
    {
        float change = Random.Range(0f, 1f);
        if (change < number2Chance)
        {
            return 2;
        }

        return 4;
    }    

    public void Spawn()
    {           
        List<int> emptyCellIndex = new List<int>();
        for (int index = 0; index < cellTransforms.Length; ++index)
        {           
            if (cellTransforms[index].childCount == 0)
            {
                emptyCellIndex.Add(index);
            }            
        }

        if (emptyCellIndex.Count == 0)
        {
            return;
        }

        int randomEmptyCellIndex = Random.Range(0, emptyCellIndex.Count);
        int cellIndex = emptyCellIndex[randomEmptyCellIndex];

        GameObject newObject = Instantiate(tilePrefab, cellTransforms[cellIndex]);
        Tile tile = newObject.GetComponent<Tile>();

        int newValue = GenerateNumber();
        tile.Value = newValue;
        tileSpawned.Invoke(cellIndex, tile);        
    }

    /*
    public void Spawn(int index, int value)
    {                        
        GameObject newObject = Instantiate(tilePrefab, cellTransforms[index]);
        Tile tile = newObject.GetComponent<Tile>();
        
        tile.Value = value;
        tileSpawned.Invoke(index, tile);
    }
    */
}
