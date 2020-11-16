using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    public GameObject endUI;
    public Text endMessage;
    public static GameManager Instance;
    public int life=10;
    private EnemySpawner enemySpawner;
    void Awake () {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner> ();
    }
    public void Win () {
        endUI.SetActive (true);
        endMessage.text = "胜 利";
    }
    public void Failed () {
        life--;
        if(life==0){ 
            enemySpawner.stop ();
            endUI.SetActive (true);
            endMessage.text = "失 败";
            }
    }
    public void OnButtonRetry () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }
    public void onButtonMenu () {
        SceneManager.LoadScene (0);
    }
}