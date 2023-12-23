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

    public Player _player;
    public Ball _bolinha;
    public bool segurando = true;
    private Vector3 offset;

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
        offset = _player.transform.position - _bolinha.transform.position;
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
        if (segurando)
        {
            _bolinha.transform.position = _player.transform.position - offset;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                segurando = false;
                _bolinha.LancarBolinha(_player.rb.velocity);
            }
        }
    }
}
