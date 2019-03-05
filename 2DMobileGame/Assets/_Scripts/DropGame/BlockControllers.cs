using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControllers : MonoBehaviour {

    public GameObject livesCheck;

    private void Update ()
    {
        if (gameObject.transform.position.y < -6)//when the block goes off the screen, used if you dont hit the object so it despawns
        {
            Object.Destroy(gameObject);
            GameObject.FindGameObjectWithTag("lives").SetActive(false);//used to tell the other script to loose a life
        }
    }
}
