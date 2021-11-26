using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomize : MonoBehaviour
{
    public Cell[] cells;
    public static bool[] savedCells;

    //set to occur happen after CellGenerator's Start method in execution order
    private void Start()
    {
        GameObject[] cellGameObjects = GameObject.FindGameObjectsWithTag("Cell");
        cells = new Cell[cellGameObjects.Length];
        savedCells = new bool[cells.Length];

        for (int i = 0; i < cellGameObjects.Length; i++)
        {
            cells[i] = cellGameObjects[i].GetComponent<Cell>();
        }
    }

    public void RandomizeCells()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (Random.value < SetRandom.PercentageChance / 100)
            {
                cells[i].SetToAlive();
            }
            else
            {
                cells[i].SetToDead();
            }
        }
    }

    public void Reload()
    {
        for (int i = 0; i < savedCells.Length; i++)
        {
            if (savedCells[i])
            {
                cells[i].SetToAlive();
            }
            else
            {
                cells[i].SetToDead();
            }
        }
    }

    public void Save()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            savedCells[i] = cells[i].isAlive;
        }
    }
}
