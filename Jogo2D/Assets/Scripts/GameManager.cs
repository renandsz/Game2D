using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int vidas = 2;

    public GameObject playerPrefab, ballPrefab;
    public Transform playerSpawnPoint, ballSpawnPoint;

    private Player _player;
    private Ball _bolinha;
    public bool segurando = true;

    public int blocosRestantes;
    public TextMeshProUGUI contadorVidas;
    public GameObject msgVitoria;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SpawnarNovoPlayer();
        int normais = GameObject.FindGameObjectsWithTag("Bloco").Length;
        int reforcados = GameObject.FindGameObjectsWithTag("Reforcado").Length;
        int especiais = GameObject.FindGameObjectsWithTag("Especial").Length;
        blocosRestantes = normais + reforcados + especiais;
        AtualizarContador();
    }

    void SpawnarNovoPlayer()
    {
        GameObject playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        GameObject ballObj = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

        _player = playerObj.GetComponent<Player>();
        _bolinha = ballObj.GetComponent<Ball>();
        segurando = true;
    }

    public void SubtrairBloco()
    {
        blocosRestantes--;
        if (blocosRestantes <= 0)
        {
            Ganhou();
        }
    }

    public void PerderVida()
    {
        vidas--;
        AtualizarContador();
        Destroy(_player.gameObject);
        if (vidas <= 0)
        {
           Invoke(nameof(RecarregarCena),1);
        }
        else
        {
            Invoke(nameof(SpawnarNovoPlayer),1);
            
        }
    }

    public void RecarregarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AtualizarContador()
    {
        contadorVidas.text = $"Vidas: {vidas}";
    }

    public void Ganhou()
    {
        Destroy(_bolinha.gameObject);
        msgVitoria.SetActive(true);
    }

    void Update()
    {
        if (segurando && Input.GetKeyDown(KeyCode.Space))
        {
            segurando = false;
            float x = Random.Range(-0.2f, 0.2f);
            float y = 1;
            Vector2 dir = new Vector2(x,y) + _player.rb.velocity;
            _bolinha.LancarBolinha(dir);
        }
    }
}
