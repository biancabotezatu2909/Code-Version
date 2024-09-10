using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LisaScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrenght;
    public LogicScript logic;
    public bool LisaIsAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        gameObject.SetActive(false); 

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true && LisaIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrenght; // it goes (1, 0) on axis

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        LisaIsAlive = false;
    }
}
