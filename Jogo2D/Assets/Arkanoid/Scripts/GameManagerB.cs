using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class GameManagerB : MonoBehaviour
{
    public static GameManagerB instance;
    public int vidas = 2;
    public int tijolosRestantes;

    public GameObject playerPrefab;
    public GameObject ballPrefab;

    public PlayerB playerAtual;
    public BallB bolaAtual;

    public bool segurando = true;

    public TextMeshProUGUI contadorVidas;
    public TextMeshProUGUI msgFinal;

    public Transform playerSpawn;
    public Transform ballSpawn;
    private Vector3 offset;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SpawnarNovoJogador();
        tijolosRestantes = GameObject.FindGameObjectsWithTag("Tijolo").Length;
        AtualizarContador();

    }

    public void SpawnarNovoJogador()
    {
        segurando = true;
        GameObject playerObj = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        GameObject ballObj = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);

        playerAtual = playerObj.GetComponent<PlayerB>();
        bolaAtual = ballObj.GetComponent<BallB>();
        offset = playerAtual.transform.position - bolaAtual.transform.position;
    }

    public void SubtrairVida()
    {
        vidas--;
        AtualizarContador();
        Destroy(playerAtual.gameObject);
        Destroy(bolaAtual.gameObject);

        if (vidas <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(SpawnarNovoJogador),1);
        }
        
    }

    public void AtualizarContador()
    {
        contadorVidas.text = $"Vidas: {vidas}";
    }

    public void SubtrairTijolo()
    {
        tijolosRestantes--;
        if (tijolosRestantes <= 0)
        {
            Vitoria();
            Invoke(nameof(ReiniciarCena),1);
        }
    }

    public void GameOver()
    {
        msgFinal.text = "GameOver";
        Invoke(nameof(ReiniciarCena),1);
    }
    public void Vitoria()
    {
        Destroy(bolaAtual.gameObject);
        msgFinal.text = "Parabéns";
    }

    public void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
