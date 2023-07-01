using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private string _shooterScene;

    [SerializeField]
    private string _mainScene;

    [SerializeField]
    private GameObject _uiPanel;

    [SerializeField]
    private GameObject[] _winloseGOs;

    [SerializeField]
    private EnemiesSpawner _spawner;

    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private SceneLoader _sceneloader;

    [SerializeField]
    private UnityEvent _endGame;

    private int _destroyedEnemies;

    private void Awake()
    {
        Time.timeScale = 1;
        _uiPanel.SetActive(false);
    }

    private void Start()
    {
        _source.Play();
    }

    public void DestroyEnemy() 
    {
        _destroyedEnemies++;
        if (_destroyedEnemies >= _spawner.EnemiesCount)
        {
            EndGame(true);
        }
    }
    
    public void EndGame(bool win)
    {
        Time.timeScale = 0;
        //_uiPanel.SetActive(true);
        _endGame.Invoke();
        _winloseGOs[Convert.ToInt32(win)].SetActive(true);
    }

    public void RestartGame()
    {
        _sceneloader.LoadScene(_shooterScene);
    }

    public void QuitGame()
    {
        _sceneloader.LoadScene(_mainScene);
    }
}
