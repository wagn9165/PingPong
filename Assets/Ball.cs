using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 temp;
        temp.x = Random.Range(0.5f, 1.0f);
        temp.y = Random.Range(0.5f, 1.0f);
        temp.Normalize();
        direction = temp;
        radius = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        //Bounce off top and bottom
        if((transform.position.y < (GameManager.bottomLeft.y + radius)) && direction.y < 0)
        {
            direction.y = -direction.y;
        }
        if ((transform.position.y > (GameManager.topRight.y - radius)) && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        //Score
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
        {
            Debug.Log("Right player wins!!");

            GameManager.Score(false);
            ResetBall();
        }
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            Debug.Log("Left player wins!!");

            GameManager.Score(true);
            ResetBall();
        }
    }

    public void ResetBall()
    {
        transform.position = Vector2.zero;

        Vector2 temp;
        temp.x = Random.Range(-1.0f, 1.0f);
        while(temp.x < 0.5f && temp.x > -0.5f)
        {
            temp.x *= 2;
        }
        temp.y = Random.Range(-1.0f, 1.0f);
        temp.Normalize();
        direction = temp;

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter fired");
        if(other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight;

            if (isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
            }

            if(isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
            }

        }
    }

    void OnDestroy()
    {
        Debug.Log("I, the ball, have been destroyed");
    }
}
