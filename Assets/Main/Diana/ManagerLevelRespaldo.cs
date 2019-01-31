using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevelRespaldo : MonoBehaviour
{/*
    //Level control
    [SerializeField] private int currentLevelNight = 0;
    public int CurrentLevelNight
    {
        get
        {
            return currentLevelNight;
        }
        set
        {
            //aqui se puede poner condicional para evitar que el valor sea menor que cero 
            currentLevelNight = value;
        }
    }
    //Enemies
    public GameObject[] enemiesPrefab;
    public int[] typesOfEnemyPerLevel; //Sirve 'levelNight' porque se suma 1 enemigo por nivel, pero pongo esto por si quieren meter mas enemigos por nivel
    public Transform spawnPointsEnemyParent;
    public Transform[] spawnPointsEnemy;
    public int[] wavesPerLevel;
    //Tesoros
    public List<GameObject> tesorosQueTengo = new List<GameObject>();
    public GameObject[] tesorosPrefab;
    public Transform[] currentTesorosPositions;
    public Transform[] unTesoroPosition;
    public Transform[] dosTesorosPositions;
    public Transform[] tresTesorosPositions;
    public Transform[] cuatroTesorosPositions;

    //Buffs
    public enum Buffs { perfecto, balanceado, negativo, nada };
    public static Buffs buffers = Buffs.nada;
    public static int buffAttack = 3;
    public static int buffTesoro = 2;
    public static int buffToEnemies = 50;

    void Awake()
    {
        int children = spawnPointsEnemyParent.childCount;
        spawnPointsEnemy = new Transform[children];
        for (int i = 0; i < children; ++i)
            spawnPointsEnemy[i] = spawnPointsEnemyParent.GetChild(i).transform;

        tesorosQueTengo.Add(tesorosPrefab[0]); //Coloca el primer tesoro (oso)

    }
    // Start is called before the first frame update
    void Start()
    {
        //TEMP(BORRAR) Se debe asignar cuando va a comenzar el siguiente nivel
        tesorosQueTengo.Add(tesorosPrefab[1]);
        tesorosQueTengo.Add(tesorosPrefab[2]);
        tesorosQueTengo.Add(tesorosPrefab[3]);
        StartLevelNight();//TEMP
    }
    public void StartLevelNight()
    {
        if (currentLevelNight == 0)
        {
           Debug.Log("Empieza nivel 1 (tutorial)");
       }
        else if (currentLevelNight == 1)
        {
            Debug.Log("Empieza nivel 2");
       }
        else if (currentLevelNight == 2)
        {
            Debug.Log("Empieza nivel 3");
        }
        else if (currentLevelNight == 3)
        {
            Debug.Log("Empieza nivel 4");
        }
        StopAllCoroutines();

        if (tesorosQueTengo.Count == 1)
        {
            currentTesorosPositions = unTesoroPosition;
        }
        else if (tesorosQueTengo.Count == 2)
        {
            currentTesorosPositions = dosTesorosPositions;
        }
        else if (tesorosQueTengo.Count == 3)
        {
            currentTesorosPositions = tresTesorosPositions;
        }
        else if (tesorosQueTengo.Count == 4)
        {
            currentTesorosPositions = cuatroTesorosPositions;
        }

        //Sólo se instancian los tesoros que he ganado
        for (int i = 0; i < tesorosQueTengo.Count; i++)
        {
            Instantiate(tesorosQueTengo[i], currentTesorosPositions[i].position, Quaternion.identity);
        }



        StartCoroutine(EnemyWaves());
    }

    IEnumerator EnemyWaves()
    {
        int index;
        int densityOfWave = 3;
        for (int i = 0; i < wavesPerLevel[currentLevelNight]; i++)
        {
            for (int x = 0; x < densityOfWave; x++)
            {
                print("New enemy");
                index = Random.Range(0, spawnPointsEnemy.Length);
                Instantiate(enemiesPrefab[Random.Range(0, typesOfEnemyPerLevel[currentLevelNight])], spawnPointsEnemy[index].position, Quaternion.identity);
                yield return new WaitForSeconds(1);
            }
            densityOfWave++;
            //PENDIENTE: intensidad: cuantos enemy salen por oleada? aumenta
            //PENDIENTE: el tiempo entre una oleada y otra va disminuyendo?
            //pendiente, niivel imposile
            yield return new WaitForSeconds(2);
        }
        yield return null;
    }*/
}
