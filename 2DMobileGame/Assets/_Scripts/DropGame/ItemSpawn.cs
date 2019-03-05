using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawn : MonoBehaviour {

    public GameObject sphere;
    public GameObject cube;
    public GameObject rectangleH;
    public GameObject rectangleV;
    public GameObject deathScreen;
    public GameObject livesCheck;
    public Text scoreText;
    public Text highScoreText;

    private int score = 0;
    private int lives = 3;
    private int shapeChooser;
    private readonly int maxSpawnPointLength = 4;
    private float spawnTime = 3f;
    private readonly float spawnTimeChange = 0.0005f;
    private Camera cam;
    private GameObject[] shapes = new GameObject[4];

    private void Start()
    {
        cam = Camera.main;

        shapes[0] = sphere;
        shapes[1] = cube;
        shapes[2] = rectangleH;
        shapes[3] = rectangleV;

        if(spawnTime <= 0.3f)
        {
            spawnTime = 0.3f;
        }

        StartCoroutine(SpawnItems());//start to spawn the items
    }

    private void Update()
    {
        spawnTime = spawnTime - spawnTimeChange;//spawn rates gets faster
        Debug.Log(spawnTime);

        if (Input.GetMouseButtonDown(0))//when left clicked
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayCastHit;

            if (Physics.Raycast(ray, out rayCastHit))//if you hit an object with a box collider(so dropping items)
            {
                BoxCollider bc = rayCastHit.collider as BoxCollider;
                Object.Destroy(bc.gameObject);//destroy the item
                score = score + 5;//and up the score
                Debug.Log(score);
            }
        }

        if(livesCheck.activeSelf == false)//if player didnt click an object loose a life
        {
            lives = lives - 1;
            livesCheck.SetActive(true);
            Debug.Log("You have " + lives + " lives left!");
        }

        if (lives <= 0)//when you run out of lives, get the deathscreen, check highscorem display score etc.
        {
            if (score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
            }
            deathScreen.SetActive(true);
            scoreText.text = "Score: " + score;
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("highScore");
        }
    }

    private IEnumerator SpawnItems()//method to spawn items
    {
        while (lives > 0)//only spawn items if the game hasnt finished
        {
            shapeChooser = Random.Range(0, shapes.Length);//choose a andom place to spawn items
            Instantiate(shapes[shapeChooser], new Vector3(Random.Range(-4, maxSpawnPointLength), 7, 0), Quaternion.identity);//spawn the item
            yield return new WaitForSeconds(spawnTime);//wait to spawn the next one
        }
    }
}