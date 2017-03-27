using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    //We need to set the width and the height of the maze and also the seed for our random generator, so we can have the same maze.
    public int mazeWidth;
    public int mazeHeight;
    public string mazeSeed;

    //Next, we need references to our sprites
    public Sprite floorSprite;
    public Sprite wallSprite;

    //And we also need a reference to our MazeSprite prefab
    public MazeSprite mazeSpritePrefab;

    //We need a reference for our random generator
    System.Random mazeRG;

    //and we need the instance of our Maze class
    Maze maze;

    //The Start function initializes our random generator, checks if we have set odd dimensions, 
    //if not, we add 1 to the width or height, then it creates a new instance of our maze and generates the maze and calls the DrawMaze function
    void Start()
    {
        mazeRG = new System.Random(mazeSeed.GetHashCode());

        if (mazeWidth % 2 == 0)
            mazeWidth++;

        if (mazeHeight % 2 == 0)
        {
            mazeHeight++;
        }

        maze = new Maze(mazeWidth, mazeHeight, mazeRG);
        maze.Generate();

        DrawMaze();
    }

    //The DrawMaze function loops through the grid and checks, if the maze cell is a path, and if it is, we create a new maze sprite
    void DrawMaze()
    {
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                Vector3 position = new Vector3(x, y);

                if (maze.Grid[x, y] == true)
                {
                    CreateMazeSprite(position, floorSprite, transform, 0, mazeRG.Next(0, 3) * 90);
                }
            }
        }
    }

    //the CreateMazeSprite function instantiates a new MazeSprite prefab at the current position, sets the sprite with the sorting order, sets the instances transform parent and rotates the sprite
    void CreateMazeSprite(Vector3 position, Sprite sprite, Transform parent, int sortingOrder, float rotation)
    {
        MazeSprite mazeSprite = Instantiate(mazeSpritePrefab, position, Quaternion.identity) as MazeSprite;
        mazeSprite.SetSprite(sprite, sortingOrder);
        mazeSprite.transform.SetParent(parent);
        mazeSprite.transform.Rotate(0, 0, rotation);
    }
}
