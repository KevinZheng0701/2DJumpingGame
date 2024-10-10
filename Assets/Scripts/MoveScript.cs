using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public bool movement = false;
    public float moveSpeed;
    private float deleteZone = -25;
    private LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        movement = !logic.isPaused;
        if (movement)
        {
            transform.position += Time.deltaTime * (Vector3.left * moveSpeed);
            if (transform.position.x < deleteZone && transform.name != "Background")
            {
                Destroy(gameObject);
            }
        }
        if (logic.gameOverTriggered)
        {
            movement = false;
            if (gameObject.name != "Background")
            {
                Destroy(gameObject);
            }
        }
    }
}
