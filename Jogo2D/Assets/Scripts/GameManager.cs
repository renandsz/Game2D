using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int vidas = 2;
    public int blocosRestantes;

    public GameObject playerPrefab;
    public GameObject ballPrefab;

    public Player playerAtual;
    public Ball bolaAtual;

    public bool segurando = true;

    public TextMeshProUGUI contadorVidas;
    public TextMeshProUGUI msgVitoria;

    public Transform playerSpawn;
    public Transform ballSpawn;
    public Vector3 offset;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnarNovoJogador()
    {
        GameObject playerObj = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        GameObject ballObj = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);

        playerAtual = playerObj.GetComponent<Player>();
        bolaAtual = ballObj.GetComponent<Ball>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnarNovoJogador();
        offset = playerAtual.transform.position - bolaAtual.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (segurando)
        {
            bolaAtual.transform.position = playerAtual.transform.position - offset;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                segurando = false;
                bolaAtual.DispararBolinha(playerAtual.inputX);
            }
        }
    }
}
