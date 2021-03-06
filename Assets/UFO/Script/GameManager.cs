﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject panel;
    public Text tekst;
    public GameObject load;
    public GameObject play;

    public void scena(int scena)
    {
        tekst.text = "";
        panel.SetActive(false);
        SceneManager.LoadScene(scena);
    }

    // check if player wants to load game and if the save file exist then load it 
    public void loadscena()
    {
        if (PlayerPrefs.GetInt("ufolv") != 0)
        {
            int level = PlayerPrefs.GetInt("ufolv");
            SceneManager.LoadScene(level);
        }
        else
        {
            panel.SetActive(true);
            tekst.text = "NO SAVE GAME FILE START A NEW GAME";
            play.SetActive(false);
        }

    }
    public void closewindow()
    {
        panel.SetActive(false);
        tekst.text = "";
    }

    //function for checking if player wants to start a new game
    public void NewGame()
    {
        if (PlayerPrefs.GetInt("ufolv") <= 0)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            panel.SetActive(true);
            tekst.text = "DO YOU WISH TO START A NEW GAME";
            play.SetActive(true);
        }
    }
}
