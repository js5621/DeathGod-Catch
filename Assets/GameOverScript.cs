using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    ScroreMangeScript scManger;
    [SerializeField] public Text scoreText;
    // Start is called before the first frame update
    public void Start()
    {
        scManger = FindAnyObjectByType<ScroreMangeScript>();
        int score = PlayerPrefs.GetInt("PlayerFinalScore");
        Debug.Log(score);
        scoreText.text = "SCORE :" + score.ToString();

    }
    public void RebootGame()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
