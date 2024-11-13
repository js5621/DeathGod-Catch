using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TouchEvent : MonoBehaviour
{
    GameObject tmpobject;
    [SerializeField] Object fireEffect;
    [SerializeField] Object fireEffect2;
    [SerializeField] Transform ExplosionSpawn;
    [SerializeField] Vector2 circlePoint;
    [SerializeField] AudioSource expolsionAudioSource;
    [SerializeField] AudioClip explosionClip;

    private int fireRandom;
    ScroreMangeScript smScript;
    // Start is called before the first frame update
    private void Start()
    {
        smScript = FindAnyObjectByType<ScroreMangeScript>();
    }

    private async void OnMouseDown()
    {

        await FireWorkExplosion();
    }


    async UniTask FireWorkExplosion() // 위쪽움직임
    {
        Object tmpobject = null;

        fireRandom = Random.Range(0,2);
        expolsionAudioSource.PlayOneShot(explosionClip);

        switch (fireRandom)
        {
            case 0:
                {
                    tmpobject = Instantiate(fireEffect, circlePoint, Quaternion.identity);
                    smScript.randomPoint = 100;
                    break;
                }
            case 1:
                {
                    tmpobject = Instantiate(fireEffect2, circlePoint, Quaternion.identity);
                    smScript.randomPoint = 200;
                    break;

                }
        }
        smScript.isScoreSet = true;
        this.tmpobject = (GameObject)tmpobject;

        var duration = 1.4f;
        var until = Time.time + duration;

        while (Time.time < until)
        {

            await UniTask.Yield();
        }

        Destroy(tmpobject);

    }
}
