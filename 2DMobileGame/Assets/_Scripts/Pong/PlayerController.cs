using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private void Update()
    {
        if (Input.GetMouseButton(0))//get y pos when you left click and move player to it
        {
            Vector3 MouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float target = MouseTarget.y;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(-4.5f, target, 0), 0.05f);
        }
    }
}
