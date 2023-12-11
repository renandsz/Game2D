using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }
    public void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
