﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector2 targetpos;
    public float Yincrement;
    public float speed;
    public float maxHeight;
    public float minHeight;
    public Text Scoretime;
    public Text Highscore;
    public Image[] hearts;
    public GameObject panel;
    public GameObject effect;
    public GameObject deathsound;
    private AudioSource[] Sounds;
    private float time = 0.18f;
    private float timer = 0.18f;
    public int health = 3;
    private float scoretime = 0;
    private float HighSc = 0;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Rigidbody2D rb;

    private void Start()
    {
        HighSc = PlayerPrefs.GetFloat("EndRunnerScore");
        Sounds = GetComponents<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        scoretime += Time.deltaTime;
        Scoretime.text = "Score: " + Mathf.Round(scoretime);

        //transforming position to a certain point
        transform.position = Vector2.MoveTowards(transform.position, targetpos, speed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < maxHeight && timer < 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);// bubble animation
            targetpos = new Vector2(transform.position.x, transform.position.y + Yincrement); //change direction of player going up
            Sounds[1].Play();
            timer = time;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > minHeight && timer < 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);// bubble animation
            targetpos = new Vector2(transform.position.x, transform.position.y - Yincrement);//change direction of player going down
            Sounds[1].Play();
            timer = time;
        }

        else if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && transform.position.y < maxHeight && timer < 0)
                {
                    Instantiate(effect, transform.position, Quaternion.identity);// bubble animation
                    targetpos = new Vector2(transform.position.x, transform.position.y + Yincrement); //change direction of player going up
                    Sounds[1].Play();
                    timer = time;
                }
                //swipe down
                else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && transform.position.y > minHeight && timer < 0)
                {
                    Instantiate(effect, transform.position, Quaternion.identity);// bubble animation
                    targetpos = new Vector2(transform.position.x, transform.position.y - Yincrement);//change direction of player going down
                    Sounds[1].Play();
                    timer = time;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && transform.position.y < maxHeight && timer < 0)
            {
                Instantiate(effect, transform.position, Quaternion.identity);// bubble animation
                targetpos = new Vector2(transform.position.x, transform.position.y + Yincrement); //change direction of player going up
                Sounds[1].Play();
                timer = time;
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && transform.position.y > minHeight && timer < 0)
            {
                Instantiate(effect, transform.position, Quaternion.identity);// bubble animation
                targetpos = new Vector2(transform.position.x, transform.position.y - Yincrement);//change direction of player going down
                Sounds[1].Play();
                timer = time;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (health == 2 && (other.CompareTag("Enemy"))) // check health of player is 2
        {
            hearts[2].gameObject.SetActive(false);
        }
        else if (health == 1 && (other.CompareTag("Enemy"))) // check health of player is 1
        {
            hearts[1].gameObject.SetActive(false);

        }
        else if (health == 0 && (other.CompareTag("Enemy"))) // // check health of player is 0
        {
            if (scoretime > HighSc) //display new high score if the player beats it
            {
                deathsound.GetComponent<AudioSource>().Play();
                PlayerPrefs.SetFloat("EndRunnerScore", scoretime);
                hearts[0].gameObject.SetActive(false);
                HighSc = PlayerPrefs.GetFloat("EndRunnerScore");
                Highscore.text = "Your new HighScore\n " + Mathf.Round(HighSc);
                Destroy(gameObject);
                Destroy(this);
                panel.SetActive(true);
            }
            else //display the best highscore
            {
                deathsound.GetComponent<AudioSource>().Play();
                hearts[0].gameObject.SetActive(false);
                HighSc = PlayerPrefs.GetFloat("EndRunnerScore");
                Highscore.text = "HighScore\n " + Mathf.Round(HighSc);
                Destroy(gameObject);
                Destroy(this);
                panel.SetActive(true);
            }
        }      
    }
}
