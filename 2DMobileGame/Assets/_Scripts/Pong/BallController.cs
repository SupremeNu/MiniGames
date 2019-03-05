using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    public GameObject AIWinScreen;
    public GameObject playerWinScreen;
    public Text BestOutOfScoreText;
    public Text playerScoreText;
    public Text AIScoreText;
    public Rigidbody rb;

    private int BestOutOfScore = 3;
    private int playerScore;
    private int AIScore;
    private int Shootway;
    private float shootAngle;
    private float speed;


    public void Start()
    {
        Shootway = Random.Range(0, 1);//random shoot left or right
        shootAngle = Random.Range(-1500, 1500);//random shoot angle
        BestOutOfScore = PlayerPrefs.GetInt("BestOutOfScore");//find out the points needed to win, set in the options menu on scene 0
        if(BestOutOfScore == 0)//if there is nothing saved then set default to 3
        {
            BestOutOfScore = 3;
        }
        speed = 1.5f;//initial speed of the ball
    }

    private void Update ()
    {
        speed = speed + 0.005f;//make the ball faster every frame
        BestOutOfScoreText.text = BestOutOfScore.ToString();//display the best out of score

        if (Shootway == 0)//shoot left
        {
            rb.velocity = new Vector3(-1 * speed, shootAngle/1000, 0);
        }
        if (Shootway == 1)//shoot right
        {
            rb.velocity = new Vector3(1 * speed, shootAngle / 1000, 0);
        }

        if (gameObject.transform.position.x >= 5)//if gone past AI add score to player
        {
            playerScore += 1;
            playerScoreText.text = playerScore.ToString();
            Start();
            gameObject.transform.position = new Vector3(0, 0, 0);
        }
        if(gameObject.transform.position.x <= -5)//if gone past player add score to AI
        {
            AIScore += 1;
            AIScoreText.text = AIScore.ToString();
            Start();
            gameObject.transform.position = new Vector3(0, 0, 0);
        }

        if(AIScore == BestOutOfScore)//if ai wins, show ai wins screen
        {
            AIWinScreen.SetActive(true);
            gameObject.transform.position = new Vector3(0, 0, 0);
        }
        if(playerScore == BestOutOfScore)//if player wins, show player wins screen
        {
            playerWinScreen.SetActive(true);
            gameObject.transform.position = new Vector3(0, 0, 0);
        }
	}

    private void OnCollisionEnter(Collision collision)//when there is a collision
    {
        if(collision.gameObject.tag == "PongStrip")//if collide with the pong player or ai
        {
            shootAngle = Random.Range(-1500, 1500);//shoot back at a random angle
            if (Shootway == 0)//change direction
            {
                Shootway = 1;
            }
            else
            {
                Shootway = 0;
            }
        }
        if (collision.gameObject.tag == "Barrier")//if collide with the top or bottom barrier
        {
            if(rb.velocity.y > 0 && shootAngle > 0)//if hit the top barrier, change angle down
            {
                shootAngle = -shootAngle;
            }
            else if(rb.velocity.y < 0 && shootAngle < 0)//if hit the bottom barrier, change angle up
            {
                shootAngle = -shootAngle;
            }
            rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z);//change the direction
        }
    }

    public void RefreshScene(int Score, Text ScoreText)//used after a point is scored to set new score and reset the scene
    {
        Score += 1;
        ScoreText.text = Score.ToString();
        Start();
        gameObject.transform.position = new Vector3(0, 0, 0);
    }
}
