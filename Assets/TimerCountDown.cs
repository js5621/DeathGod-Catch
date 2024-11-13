using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerCountDown : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] float remainingTime;
    [SerializeField] AudioSource countAudioSource;
    [SerializeField] AudioClip countAudioClip;
    // Startalled before the first frame update
    public bool isGameOver= false;
    int finalScore = 0;
    ScroreMangeScript scManager;
    bool setCountDown = false;
    int tempSeconds = 0;
    // Update is called once per frame
    public void Start()
    {
        scManager = FindAnyObjectByType<ScroreMangeScript>();

    }
    async void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;


        }

        else if (remainingTime < 0)
        {
            remainingTime = 0;
            isGameOver = true;
            finalScore = scManager.score;
            PlayerPrefs.SetInt("PlayerFinalScore", finalScore);
            await UniTask.Delay(200);


            SceneManager.LoadScene(2);
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        if (seconds <= 10 && minutes < 1)// 시간 임박효과 표시
        {
            if (seconds < tempSeconds)
                setCountDown = false;
            if (!setCountDown)
            {

                setCountDown = true;


                countAudioSource.PlayOneShot(countAudioClip);

            }
            tempSeconds = seconds;
            timerText.color = Color.red;

        }

        timerText.text ="TIME : "+string.Format("{0:00}:{1:00}",minutes,seconds);

    }
    void beep()//난수설정
    {
        if (setCountDown)
            return;
        try
        {
            setCountDown = true;
            countAudioSource.PlayOneShot(countAudioClip);

        }
        finally { setCountDown = false; }
    }
}
