using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Setup : MonoBehaviour
{
    [SerializeField] private GameObject Grid;
    [SerializeField] private Camera cam;
    [SerializeField] Slider sliderX, sliderY;
    [SerializeField] Text sizeXText, sizeYText;
    float sizeX, sizeY;
    private SpawnWallField SpawnWallRef;
    private int smallestGridValue = 10, biggestGridValue = 25;
    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        //References SpawnWallRef to the SpawnWallField script that is on this Game Object.
        SpawnWallRef = this.GetComponent<SpawnWallField>();

        //If objects in the wall list exist, Destroy them all and clear the list. Also Destroys the Player object and Finish object.
        if (SpawnWallRef.GetList().Count > 0)
        {
            Destroy(GameObject.Find("Player(Clone)").gameObject, 0.01f);
            Destroy(GameObject.Find("SetupManager").GetComponent<MazeCreator>().finish);
            GameObject.Find("SetupManager").GetComponent<MazeCreator>().EmptyList();

            List<GameObject> walls = SpawnWallRef.GetList();
            for (int i = 0; i < walls.Count; i++) 
            {
                Destroy(walls[i]);
            }
            SpawnWallRef.EmptyList();
        }

        //This was sizeX and sizeY will be between 10 and 25, depending on the slider value.
        sizeX = Mathf.Round(sliderX.value * 15f) + 10;
        sizeY = Mathf.Round(sliderY.value * 15f) + 10;
        //Set the scale of the grid.
        Grid.transform.localScale = new Vector3(sizeX, 1, sizeY);

        //Changes the camera size based on the maze size. + 5 for the UI.
        if (Grid.transform.localScale.x > Grid.transform.localScale.z)
        {
            cam.orthographicSize = Grid.transform.localScale.x / 2 + 5;
        }
        else
        {
            cam.orthographicSize = Grid.transform.localScale.z / 2 + 5;
        }

        //This will spawn the inital "field" of walls.
        SpawnWallRef.SpawnField(Grid.transform.localScale.x, Grid.transform.localScale.z);

        //Generates a start position for the maze, retrievable with GetStartPosition().
        SpawnWallRef.GenerateStartPosition(Grid.transform.localScale.x, Grid.transform.localScale.z);

        //Starts the setup for the MazeCreator script that is on this Game Object.
        this.GetComponent<MazeCreator>().Setup(SpawnWallRef.GetList(), SpawnWallRef.GetStartPosition());
    }

    private void Update()
    {
        sizeXText.text = "Current maze X: " + sizeX + "\nNext maze X: " + Mathf.Round(sliderX.value * 15f + 10);
        sizeYText.text = "Current maze X: " + sizeY + "\nNext maze X: " + Mathf.Round(sliderY.value * 15f + 10);
    }
}
