using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScroreMangeScript : MonoBehaviour
{
    public bool isScoreSet = false;
    [SerializeField] Text scoreText;
    [SerializeField] Text pointText;
    public int score = 0;
    public int randomPoint = 0;
    public static int Testnum = 22;
    // Start is called before the first frame update
    [SerializeField] AudioSource getItemAudioSouce;
    [SerializeField] AudioClip getItemEffect;

    void Start()
    {

    }

    // Update is called once per frame
    async void Update()
    {
        if(isScoreSet)
        {
            isScoreSet = false;
            await FireWorkExplosion();
            score = score + randomPoint;
            scoreText.text = "SCORE :" + score.ToString();


        }
    }
    async UniTask FireWorkExplosion() // 위쪽움직임
    {

        var duration = 0.7f;
        var until = Time.time + duration;
        pointText.text = "+" + randomPoint.ToString();
        pointText.gameObject.SetActive(true);
        getItemAudioSouce.PlayOneShot(getItemEffect);
        while (Time.time < until)
        {

            await UniTask.Yield();
        }
        pointText.gameObject.SetActive(false);


    }

}
