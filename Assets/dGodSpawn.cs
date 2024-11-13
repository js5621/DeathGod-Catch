using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dGodSpawn : MonoBehaviour
{
    [SerializeField] GameObject dGodPrefab;
    [SerializeField] Transform dGodSpawnPoint1;
    [SerializeField] Transform dGodSpawnPoint2;
    [SerializeField] Transform dGodSpawnPoint3;
    [SerializeField] Transform dGodSpawnPoint4;
    private Vector2 dGodSpawnPos;
    private float newSpawnDuration = 1f;
    GameObject tmpObject;
    public bool isDestroyed=false;
    public bool isCreated = false;
    public int positionRandom =0;
    TimerCountDown tCountDouwn;
    // Start is called before the first frame update
    private void Start()
    {

        tCountDouwn   = FindAnyObjectByType<TimerCountDown>();
        NewSpawnRequest();
    }
    private void Update()
    {

        if (isDestroyed)
            NewSpawnRequest();


    }

    void SpawnNewObject()
    {
        if (tCountDouwn.isGameOver)
            return;
        positionRandom = Random.Range(0, 4);
        switch(positionRandom)
        {
            case 0:
                dGodSpawnPos = dGodSpawnPoint1.position;
                break;
            case 1:
                dGodSpawnPos = dGodSpawnPoint2.position;
                break;
            case 2:
                dGodSpawnPos = dGodSpawnPoint4.position;
                break;
            case 3:
                dGodSpawnPos = dGodSpawnPoint3.position;
                break;

        }
        Instantiate(dGodPrefab, dGodSpawnPos, Quaternion.identity);

    }

    public void NewSpawnRequest()
    {
        if (isCreated)
            return;
        Invoke("SpawnNewObject", newSpawnDuration);
        isCreated = true;
    }

    public void RetrySpawnRequest()
    {


        Invoke("SpawnNewObject", newSpawnDuration);
        Destroy(tmpObject, 2f);
    }
}
