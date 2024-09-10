using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveScript = 5;
    public float deadZone = -45;
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (logic.gameStarted)
        {
            transform.position = transform.position + (Vector3.left * moveScript) * Time.deltaTime * 3;

            if (transform.position.x < deadZone)
            {
                Destroy(gameObject);
            }
        }
    }
}
