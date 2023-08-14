using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellGenerator : MonoBehaviour
{
    //set in inspector
    public Transform topLeftCorner;

    public GameObject cell;
    private float cellWidth;
    private float cellHeight;

    public Cell[,] cells;

    // default 100 by 100
    public int rows = 100;
    public int columns = 100;

    private float widthScale;
    private float heightScale;

    public bool colorOn = false;
    public bool musicOn = false;

    // default: 2, 3, 3
    // straight saucer: -1, -1, 2
    // squares #1: 3, 4, 1
    // squares #2: 3, 5, 0
    public int aliveCondition1 = 2;
    public int aliveCondition2 = 3;
    public int reviveCondition = 3;

    public InputField inputField1;
    public InputField inputField2;
    public InputField inputField3;

    private int r = 255;
    private int g = 0;
    private int b = 0;
    private string centeredColor = "r";
    private const int colorSpeed = 9;
    private const int lighten = 100;
    private int currentColorFrameCount = 0;
    private const int colorFrameRate = 6;

    //good colorSpeed lighten ratio
    //cS: 9, l: 100
    //cS: 15, l: 0

    //set in inspector
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentMusicFrameCount = 0;
    private const int musicFrameRate = 4;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        cells = new Cell[rows, columns];

        cell = Resources.Load("CellPrefab") as GameObject;
        cellWidth = cell.transform.localScale.x;
        cellHeight = cell.transform.localScale.y;

        widthScale = 100f / rows;
        heightScale = 100f / columns;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newCell = Instantiate(cell, new Vector3(topLeftCorner.position.x + j * cellWidth * widthScale,
                    topLeftCorner.position.y - i * cellHeight * heightScale, 0), Quaternion.identity);
                newCell.transform.localScale = new Vector2(0.08f * widthScale, 0.08f * heightScale);

                newCell.name = i + "," + j;

                cells[i, j] = newCell.GetComponent<Cell>();
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                AddNeighbors(i, j, cells[i, j]);
            }
        }
    }

    public void AddNeighbors(int row, int col, Cell cell)
    {
        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                if (i >= 0 && i < rows && j >= 0 && j < columns)
                {
                    if (cells[i, j] != cell)
                    {
                        cells[row, col].neighbors.Add(cells[i, j]);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cells[i, j].CalculateLife();
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (cells[i, j].shouldBeAlive)
                {
                    cells[i, j].SetToAlive();
                }
                else
                {
                    cells[i, j].SetToDead();
                }
            }
        }

        if (musicOn && currentMusicFrameCount % musicFrameRate == 0)
        {
            PlaySound();
        }

        currentMusicFrameCount++;
    }

    private void Update()
    {
        if (colorOn && currentColorFrameCount % colorFrameRate == 0)
        {
            SetColor();
        }

        currentColorFrameCount++;
    }

    public void ToggleColor()
    {
        if (colorOn)
        {
            CancelColor();
            colorOn = false;
        }
        else
        {
            SetColor();
            colorOn = true;
        }
    }

    public void SetColor()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = i; j < columns; j++)
            {
                Cell cell1 = cells[i, j];
                Cell cell2 = cells[j, i];

                cell1.r = r;
                cell1.g = g;
                cell1.b = b;

                cell2.r = r;
                cell2.g = g;
                cell2.b = b;

                cell1.SetColor();
                cell2.SetColor();
            }

            UpdateColor();
        }
    }

    public void CancelColor()
    {
        r = 255;
        g = 0;
        b = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Cell cell = cells[i, j];

                cell.r = 255;
                cell.g = 255;
                cell.b = 255;

                cell.SetColor();
            }
        }
    }

    public void UpdateColor()
    {
        switch (centeredColor)
        {
            case "r":
                if (b > lighten)
                {
                    b -= colorSpeed;
                }
                else
                {
                    g += colorSpeed;
                    b = lighten;
                }
                if (g >= 255)
                {
                    centeredColor = "g";
                    g = 255;
                }
                break;
            case "g":
                if (r > lighten)
                {
                    r -= colorSpeed;
                }
                else
                {
                    b += colorSpeed;
                    r = lighten;
                }
                if (b >= 255)
                {
                    centeredColor = "b";
                    b = 255;
                }
                break;
            case "b":
                if (g > lighten)
                {
                    g -= colorSpeed;
                }
                else
                {
                    r += colorSpeed;
                    g = lighten;
                }
                if (r >= 255)
                {
                    centeredColor = "r";
                    r = 255;
                }
                break;
            default:
                Debug.Log("how?");
                break;
        }
    }

    public int AliveNum()
    {
        int count = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (cells[i, j].isAlive)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public void PlaySound()
    {
        int num = AliveNum();

        //include 0 case so +1
        int testCount = audioClips.Length + 1; 

        if (num % testCount != 0)
        {
            audioSource.clip = audioClips[num % testCount - 1];
            audioSource.Play();
        }
    }

    public void ToggleSound()
    {
        if (musicOn)
        {
            musicOn = false;
        }
        else
        {
            musicOn = true;
        }
    }

    public void SetAlive1()
    {
        if (int.TryParse(inputField1.text, out int value))
        {
            aliveCondition1 = value;
        }
        else
        {
            aliveCondition1 = 2;
        }

        UpdateCellConditions();
    }

    public void SetAlive2()
    {
        if (int.TryParse(inputField2.text, out int value))
        {
            aliveCondition2 = value;
        }
        else
        {
            aliveCondition2 = 3;
        }

        UpdateCellConditions();
    }

    public void SetRevive()
    {
        if (int.TryParse(inputField3.text, out int value))
        {
            reviveCondition = value;
        }
        else
        {
            reviveCondition = 3;
        }

        UpdateCellConditions();
    }

    public void UpdateCellConditions()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cells[i, j].aliveCondition1 = aliveCondition1;
                cells[i, j].aliveCondition2 = aliveCondition2;
                cells[i, j].reviveCondition = reviveCondition;
            }
        }
    }
}
