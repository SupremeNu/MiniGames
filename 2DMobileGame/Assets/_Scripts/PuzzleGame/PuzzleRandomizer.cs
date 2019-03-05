using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleRandomizer : MonoBehaviour {

    public GameObject endScreen;

    private GameObject[] photos;
    private GameObject[] photosFinish;
    private GameObject tempObjectInScene;
    private readonly Vector3[] PicturePositions = new Vector3[] { new Vector3(8, -8, 0), new Vector3(0, -8, 0), new Vector3(-8, -8, 0), new Vector3(8, 0, 0), new Vector3(0, 0, 0), new Vector3(-8, 0, 0), new Vector3(8, 8, 0), new Vector3(0, 8, 0), new Vector3(-8, 8, 0) };
    private int puzzleChooser;
    private int tempIntFirst;
    private readonly int puzzleChooserParts = 8;
    private readonly int puzzleCount = 6;
    private bool firstClick = false;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        puzzleChooser = Random.Range(0, puzzleCount);//chooses which picture to use
        photos = GameObject.FindGameObjectsWithTag(puzzleChooser.ToString());//find all the small pictures of the chosen photo
        photosFinish = GameObject.FindGameObjectsWithTag(puzzleChooser.ToString()).OrderBy(go => go.name).ToArray();//makes the array in order
        for (int e = 0; e <= 6; e++)
        {
            for (int i = 0; i < puzzleChooserParts; i++)
            {
                int RandomSwap = Random.Range(0, puzzleChooserParts);//new place
                GameObject TempGameObject = photos[RandomSwap];//new place has game object saved in temp variable
                photos[RandomSwap] = photos[i];//new place is overwritten with i
                photos[i] = TempGameObject;//i is swapped
            }
        }
        for (int i = 0; i <= puzzleCount; i++)
        {//finds the pictures that arent being used and deactivates them in the scene
            if (i != puzzleChooser)
            {
                GameObject unneededSprites = GameObject.FindGameObjectWithTag(i + "ParentObject".ToString());
                unneededSprites.SetActive(false);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < photos.Length; i++)//moves the pics to the correct place
        {
            photos[i].transform.position = PicturePositions[i];
        }

        bool isEqual = Enumerable.SequenceEqual(photos, photosFinish);//finds if both arrays are the same

        if(isEqual == true)//if both arrays are the same put up the end screen
        {
            endScreen.SetActive(true);
        }
        else//if not then close it
        {
            endScreen.SetActive(false);
        }
        if(endScreen.activeInHierarchy == false)//if game hasnt finished
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))//when you left click
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayCastHit;
                if (Physics.Raycast(ray, out rayCastHit))//if you hit something
                {
                    BoxCollider bc = rayCastHit.collider as BoxCollider;
                    if (firstClick == true)//if you already have clicked something you want to swap
                    {
                        for (int i = 0; i <= puzzleChooserParts; i++)
                        {
                            if (photos[i] == bc.gameObject)//checks if the item you clicked is in the array and where it is
                            {//swaps the photos about
                                tempObjectInScene = photos[i];
                                photos[i] = photos[tempIntFirst];
                                photos[tempIntFirst] = tempObjectInScene;
                            }
                        }
                        Debug.Log(tempObjectInScene);
                        firstClick = false;//resets
                    }
                    else//if you havent clicked something
                    {
                        for (int i = 0; i <= puzzleChooserParts; i++)
                        {
                            if (photos[i] == bc.gameObject)//checks if the item you clicked is in the array and where it is
                            {
                                tempObjectInScene = photos[i];//set it to the temp variable
                                tempIntFirst = i;
                            }
                        }
                        Debug.Log(tempObjectInScene);
                        firstClick = true;//show you have made a first selection
                    }
                }
            }
        }
    }
}