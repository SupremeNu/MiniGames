using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMazeGame : MonoBehaviour {

    public GameObject endScreen;

    private readonly float speed = 2f;

    // Update is called once per frame
    private void Update ()
    {
        if (Input.mousePosition.x > Screen.width / 1.25)
        {
            transform.position += (new Vector3(1, 0, 0) * speed * Time.deltaTime);
            //go right
        }
        if (Input.mousePosition.x < Screen.width / 6)
        {
            transform.position += (new Vector3(-1, 0, 0) * speed * Time.deltaTime);
            //go left
        }
        if (Input.mousePosition.y > Screen.height / 1.25)
        {
            transform.position += (new Vector3(0, 1, 0) * speed * Time.deltaTime);
            //go up
        }
        if (Input.mousePosition.y < Screen.height / 6)
        {
            transform.position += (new Vector3(0, -1, 0) * speed * Time.deltaTime);
            //go down
        }
        if (gameObject.transform.position.y <= -12)//when at the end
        {
            endScreen.SetActive(true);//pop up the UI
            gameObject.transform.position = new Vector3(17, -12.5f, 0);//freeze the players position
        }
	}
}