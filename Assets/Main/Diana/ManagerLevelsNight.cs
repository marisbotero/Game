using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLevelsNight : MonoBehaviour
{
    //Level control
    /*[SerializeField] private int currentLevelNight = 0;
    public int CurrentLevelNight {
        get {
            return currentLevelNight;
        }
        set {
            //aqui se puede poner condicional para evitar que el valor sea menor que cero 
            currentLevelNight = value;
        }
    }*/
    public static int currentLevelNight = 0;
    public static bool ganaPesadilla;
    private float tiempo = 30f;
    public TextMesh cronometroText;
    //Enemies
    public GameObject[] enemiesPrefab;
    public int typesOfEnemyPerLevel; //Sirve 'levelNight' porque se suma 1 enemigo por nivel, pero pongo esto por si quieren meter mas enemigos por nivel
    public Transform spawnPointsEnemyParent;
    public Transform[] spawnPointsEnemy;
    public Transform[] tesoroQueDeboSeguir;
    IEnumerator enemyWaves;
    //Tesoros
    public Transform[] tesoros;
    //Buffs
    public enum Buffs { perfecto, balanceado, negativo, nada };
    public static Buffs buffers = Buffs.nada;
    public static int buffAttack = 0;
    public static int buffTesoro = 0;
    public static float buffToEnemies = 0;
    public GameObject buffVelocidadAtaque;

    public static ManagerLevelsNight instanse;

    private void Awake()
    {
        instanse = this;
    }

    void Start()
    {
        int children = spawnPointsEnemyParent.childCount;
        spawnPointsEnemy = new Transform[children];
        for (int i = 0; i < children; ++i)
            spawnPointsEnemy[i] = spawnPointsEnemyParent.GetChild(i).transform;

        StartCoroutine(StartCountdown());
        enemyWaves = EnemyWaves();
        StartCoroutine(enemyWaves);
        StartCoroutine(SpawnBuffDisatancia());
        buffAttack = 0;
        buffTesoro = 0;
        buffToEnemies = 0;

        switch (buffers)
        {
            case Buffs.perfecto:
                buffAttack = 999;
                break;
            case Buffs.balanceado:
                buffTesoro = 2;
                break;
            case Buffs.negativo:
                buffToEnemies = 0;
                break;
            default:
                break;
        }
    }

    IEnumerator EnemyWaves()
    {
        print("incio waves");
        int index = 0;
        int densityOfWave = 2;
        while (true)
        {
            for (int x = 0; x < densityOfWave; x++)
            {
                index = Random.Range(0, spawnPointsEnemy.Length);
                GameObject newEnemy = Instantiate(enemiesPrefab[Random.Range(0, typesOfEnemyPerLevel)], spawnPointsEnemy[index].position, Quaternion.identity);
                Enemy2 scriptEnemy = newEnemy.GetComponent<Enemy2>();
                //scriptEnemy.temporal_objective = tesoros[0];
                if (currentLevelNight == 0)
                {
                    scriptEnemy.temporal_objective = tesoros[0];
                }
                else
                {
                    scriptEnemy.temporal_objective = tesoroQueDeboSeguir[index];
                }
                yield return new WaitForSeconds(1);
            }
            densityOfWave++;
            yield return new WaitForSeconds(2);
        }
    }

    public static void DetenerCorrutinas()
    {
        if (instanse != null)
        {
            instanse.StopAllCoroutines();
        }
    }

    public IEnumerator StartCountdown()
    {
        float currCountdownValue = tiempo;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1);
            currCountdownValue--;
            cronometroText.text = currCountdownValue.ToString();
        }
        print("nivel completado");
        SceneManager.LoadScene("WinPesadilla");
        StopAllCoroutines();
        yield return null;
    }

    public IEnumerator SpawnBuffDisatancia()
    {
        bool valor = true;
        while (valor)
        {
            yield return new WaitForSeconds(5);
            Vector3 posBuff = new Vector3(Random.Range(-6, 6), Random.Range(-3, 3), 0);
            GameObject newBuff = Instantiate(buffVelocidadAtaque, posBuff, Quaternion.identity);
            SpriteRenderer spriteDelBuff = newBuff.GetComponent<SpriteRenderer>();
            yield return new WaitForSeconds(2.5f);
            spriteDelBuff.enabled = false;
            yield return new WaitForSeconds(0.2f);
            spriteDelBuff.enabled = true;
            yield return new WaitForSeconds(0.2f);
            spriteDelBuff.enabled = false;
            yield return new WaitForSeconds(0.2f);
            spriteDelBuff.enabled = true;
            yield return new WaitForSeconds(0.2f);
            spriteDelBuff.enabled = false;
            yield return new WaitForSeconds(0.2f);
            spriteDelBuff.enabled = true;
            yield return new WaitForSeconds(0.2f);
            Destroy(newBuff);
        }
        yield return null;
    }

}
