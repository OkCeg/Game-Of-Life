using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //shouldBeAlive calculates the isAlive
    public bool shouldBeAlive = false;
    public bool isAlive = false;
    public List<Cell> neighbors = new List<Cell>();

    public SpriteRenderer sr;

    public int r = 255;
    public int g = 255;
    public int b = 255;

    // default 2, 3, 3 (may be changed in CellGenerator)
    public int aliveCondition1 = 2;
    public int aliveCondition2 = 3;
    public int reviveCondition = 3;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        r = 255;
        g = 255;
        b = 255;
    }

    public void OnMouseDown()
    {
        //if alive, change the color to black
        if (isAlive)
        {
            SetToDead();
        }
        //if dead, change the color to white
        else
        {
            SetToAlive();
        }
    }

    public int AliveNeighborNum()
    {
        int count = 0;

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i].isAlive)
            {
                count++;
            }
        }

        return count;
    }

    public void CalculateLife()
    {
        int aliveNeighbors = AliveNeighborNum();

        //if cell is alive
        if (isAlive)
        {
            //if number of neighbors is not 2 or 3, kill the cell
            if (!(aliveNeighbors == aliveCondition1 || aliveNeighbors == aliveCondition2))
            {
                shouldBeAlive = false;
            }
            else
            {
                shouldBeAlive = true;
            }
        }
        //if cell is not alive
        else
        {
            //if number of neighbors is 3, give life to cell
            if (aliveNeighbors == reviveCondition)
            {
                shouldBeAlive = true;
            }
            else
            {
                shouldBeAlive = false;
            }
        }
    }

    public void SetToDead()
    {
        sr.color = new Color(0, 0, 0);
        isAlive = false;
    }

    public void SetToAlive()
    {
        sr.color = new Color(r / 255f, g / 255f, b / 255f);
        isAlive = true;
    }

    public void SetColor()
    {
        if (isAlive)
        {
            sr.color = new Color(r / 255f, g / 255f, b / 255f);
        }
        else
        {
            sr.color = new Color(0, 0, 0);
        }
    }
}
