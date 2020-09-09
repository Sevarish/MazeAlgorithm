using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float movementSpeed = 5;
    void Update()
    {
        //Basic movement with WASD
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0, 0, -movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
        }

        CheckCollisionFinish();
    }

    //Casts a sphere raycast that only checks for layer 10 (Finish).
    private void CheckCollisionFinish()
    {
        if (Physics.Raycast(this.transform.position, Vector3.forward, out RaycastHit hit1, 1, 1 << 10) ||
            Physics.Raycast(this.transform.position, Vector3.left, out RaycastHit hit2, 1, 1 << 10) ||
            Physics.Raycast(this.transform.position, Vector3.right, out RaycastHit hit3, 1, 1 << 10) ||
            Physics.Raycast(this.transform.position, Vector3.back, out RaycastHit hit4, 1, 1 << 10))
        {
            GameObject.Find("SetupManager").GetComponent<Setup>().ResetGame();
        }
    }
}
