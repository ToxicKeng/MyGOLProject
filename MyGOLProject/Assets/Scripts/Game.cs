using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private static int SCREEN_WIDTH = 64;       //- 1024 pixels
    private static int SCREEN_HEIGHT = 48;      //- 769 pixels

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    // Start is called before the first frame update
    void Start()
    {
        PlaceCells ();
    }

    // Update is called once per frame
    void Update()
    {
        CountNeighbors();

        PopulationControl();
    }

    void PlaceCells()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }


    void CountNeighbors()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (var x = 0; x < SCREEN_WIDTH; x++)
            {
                    int numNeighbors = 0;

                    //- North
                    if (y + 1 < SCREEN_HEIGHT)
                    {
                        if (grid[x, y + 1].isAlive)
                        {
                            numNeighbors++;
                        }
                    }
                    //-East
                    if (x + 1 < SCREEN_WIDTH)
                    {
                        if (grid[x + 1, y].isAlive)
                        {
                            numNeighbors++;
                        }
                    }
                    //-South
                    if (y-1 >= 0)
                    {
                        if (grid[x, y-1].isAlive)
                        {
                            numNeighbors++;
                        }
                    }
                    //-West
                    if (x - 1 >= 0)
                    {
                        if (grid[x-1, y].isAlive)
                        {
                            numNeighbors++;
                        }
                    }
                    //-NothEast
                    if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT)
                    {
                        if (grid[x + 1, y + 1].isAlive)
                        {
                            numNeighbors++;
                        }
                    }
                    //-NothWest
                    if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT)
                    {
                        if (grid[x - 1, y + 1])
                        {
                            numNeighbors++;
                        }
                    }
                    //-SouthEast
                    if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
                    {
                        if (grid[x + 1, y - 1].isAlive)
                        {
                            numNeighbors++;
                        }
                    }
                    //-SouthWest
                    if (x - 1 >= 0 && y - 1 >= 0)
                    {
                        if (grid[x - 1, y - 1].isAlive)
                        {
                            numNeighbors++;
                        }
                    }
                    grid[x, y].numNeighbors = numNeighbors;

            }
        }
    }

    void PopulationControl()//-15:30 into part 3 vid
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                //-rules
                //-Any live cell with 2 of 3 lives neighbors survives
                //-Any dead cell with 3 live neigbors become a live cell
                //-All other live cells die in next Gen and all other dead cells stay dead

                if (grid[x, y].isAlive)
                {
                    //-cell is Alive
                    if (grid[x,y].numNeighbors != 2 && grid[x, y].numNeighbors != 3)
                    {
                        grid[x, y].SetAlive(false);
                    }
                    else
                    {
                        //-cell is dead
                        if (grid[x, y].numNeighbors == 3)
                        {
                            grid[x, y].SetAlive(true);
                        }
                    }
                }
            }
        }
    }

        bool RandomAliveCell()
        {
            int rand = UnityEngine.Random.Range(0, 100);

                if (rand > 75)
            {
                return true;
            }
            return false;
        }
    }

