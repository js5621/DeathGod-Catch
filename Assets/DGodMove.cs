using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting;

public class DGodMove : MonoBehaviour
{
    [SerializeField] private float dGodSpeed = 10f;
    [SerializeField] private Transform dGodPosition;
    Rigidbody2D dGodRb;

    private Animator dGodanim;

    private bool onDGodMove = false;
    private bool onDGodMoveUpDown = false;
    private bool needToDestroy = false;// 생사판별
    private bool onRandomCreate = false;
    private bool onMoveControl = false;
    private bool udStopControl = false;
    private bool lrStopControl = false;
    // 난수변수
    private int moveRandom = 0;
    private int destroyRandom = 0;
    private CancellationTokenSource dGodCancelTokenSource;
    //외부 객체
    dGodSpawn dgSpawn;
    TimerCountDown timerCount;
    // Start is called before the first frame update
    async void Start()
    {
        dGodRb = GetComponent<Rigidbody2D>();
        dGodanim = GetComponent<Animator>();
        dGodCancelTokenSource = new CancellationTokenSource();
        timerCount = FindAnyObjectByType<TimerCountDown>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (needToDestroy||timerCount.isGameOver)
        {
            dgSpawn = FindAnyObjectByType<dGodSpawn>();
            dgSpawn.isDestroyed = true;
            //dGodCancelTokenSource.Cancel();
            gameObject.SetActive(false);
            if(gameObject !=null)
                Destroy(gameObject);
            dgSpawn.isCreated = false;
            return;
        }
        RandomCreate();//움직임 난수(설정), 생사난수(설정)
        await dGodMoveSequence(dGodCancelTokenSource.Token);




        // 객체 파괴판정
    }
    void RandomCreate()//난수설정
    {
        if (onRandomCreate)
            return;
        try
        {
            onRandomCreate = true;
            moveRandom = Random.Range(0, 2);
            destroyRandom = Random.Range(0, 4);
        }
        finally { onRandomCreate = false; }
    }


    async UniTask dGodMoveSequence(CancellationToken token) // 위아래 이동 시퀀스
    {
        if (onDGodMove || needToDestroy||timerCount.isGameOver)
        {
            return;
        }
        try
        {
            onDGodMove = true;
            dGodanim.SetBool("Moving", true);

            if (moveRandom == 0)
            {
                if (gameObject == null)
                    return;
                await DGodLeftMove();
                await UniTask.Delay(100);
                await DGodRightMove();
                await UniTask.Delay(100);
                await DGodRightMove();
                await UniTask.Delay(100);
                await DGodLeftMove();
                await UniTask.Delay(100);


            }
            else
            {
                if (gameObject == null)
                    return;
                await DGodUpMove();
                await UniTask.Delay(100);
                await DGodDownMove();
                await UniTask.Delay(100);
                await DGodDownMove();
                await UniTask.Delay(100);
                await DGodUpMove();
                await UniTask.Delay(100);



            }
            if (!timerCount.isGameOver)
                dGodanim.SetBool("Moving", false);



        }
        finally
        {

            if (destroyRandom == 3)
            {
                needToDestroy = true;

            }

            onDGodMove = false;
        }
    }
    async UniTask DGodLeftMove() //왼쪽 움직임
    {

        try
        {
            var duration = 1.0f;
            var until = Time.time + duration;
            while (Time.time < until)
            {
                if (timerCount.isGameOver)
                {
                    break;
                }
                transform.position = new Vector2(dGodPosition.position.x - Time.deltaTime * dGodSpeed, dGodPosition.position.y);
                await UniTask.Yield();
            }
        }
        finally
        {

        }
    }
    async UniTask DGodRightMove()//오른쪽움직임
    {
        if (timerCount.isGameOver)
        {
            return;
        }
        var duration = 1.0f;
        var until = Time.time + duration;
        while (Time.time < until)
        {
            if (timerCount.isGameOver)
            {
                break;
            }
            transform.position = new Vector2(dGodPosition.position.x + Time.deltaTime * dGodSpeed, dGodPosition.position.y);
            await UniTask.Yield();
        }

    }

    async UniTask DGodUpMove() // 위쪽움직임
    {
        if (timerCount.isGameOver)
        {
            return;
        }

        var duration = 1.0f;
        var until = Time.time + duration;
        while (Time.time < until)
        {
            if (timerCount.isGameOver)
            {
                break;
            }
            transform.position = new Vector2(dGodPosition.position.x, dGodPosition.position.y + Time.deltaTime * dGodSpeed);
            await UniTask.Yield();
        }



    }

    async UniTask DGodDownMove() // 아래쪽움직임
    {
        if (timerCount.isGameOver)
        {
            return;
        }
        var duration = 1.0f;
        var until = Time.time + duration;
        while (Time.time < until)
        {
            if (timerCount.isGameOver)
            {
                break;
            }
            transform.position = new Vector2(dGodPosition.position.x, dGodPosition.position.y - Time.deltaTime * dGodSpeed);
            await UniTask.Yield();
        }

    }


}
