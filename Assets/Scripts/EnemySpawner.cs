using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private Wave [] waves;
    //the least is wave producing
    public static int waveInt=4;//1 little, 2 middle, 3 high, 4 last
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public Transform START;
    public float waveRate=1f;
    public static int CountEnemyAlive=0;
    private Coroutine coroutine;
    void Start() {
        switch(waveInt){
            case 1:
                Wave[] waves1=new Wave[3];
                waves1[0]=new Wave(enemy1,1,0);
                waves1[1]=new Wave(enemy1,5,(float)0.3);
                waves1[2]=new Wave(enemy2,5,(float)0.5);
                waves=waves1;
                break;
            case 2:
                Wave[] waves2=new Wave[3];
                waves2[0]=new Wave(enemy1,10,(float)0.3);
                waves2[1]=new Wave(enemy2,10,(float)0.3);
                waves2[2]=new Wave(enemy3,6,(float)0.5);
                waves=waves2;
                break;
            case 3:
                Wave[] waves3=new Wave[4];
                waves3[0]=new Wave(enemy2,10,(float)0.3);
                waves3[1]=new Wave(enemy3,10,(float)0.3);
                waves3[2]=new Wave(enemy4,6,(float)0.5);
                waves3[3]=new Wave(enemy4,10,(float)0.3);
                waves=waves3;
                break;
            case 4:
                Wave[] waves4=new Wave[5];
                waves4[0]=new Wave(enemy1,10,(float)0.3);
                waves4[1]=new Wave(enemy2,10,(float)0.3);
                waves4[2]=new Wave(enemy3,10,(float)0.5);
                waves4[3]=new Wave(enemy4,10,(float)0.3);
                waves4[4]=new Wave(enemy5,1,0);
                waves=waves4;
                break;
        }
        coroutine=StartCoroutine(SpawnEnemy());
    }
    public void stop(){
        StopCoroutine(coroutine);
    }
    IEnumerator SpawnEnemy(){
        foreach(Wave wave in waves)
        {
            for(int i=0;i<wave.count;i++){
                GameObject.Instantiate(wave.enemyPrefab,START.position,Quaternion.identity); 
                CountEnemyAlive++;
                if(i!=wave.count-1)
                yield return new WaitForSeconds(wave.rate);
            }
            while(CountEnemyAlive>0){
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while(CountEnemyAlive>0){
            yield return 0;
        }
        GameManager.Instance.Win();
    }
    
}
