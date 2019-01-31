using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{    

	public float velocity = 0.1f;
	public Vector3 start_position;
	[SerializeField]
	private Vector3 final_position;
	public int stamina;
	public Transform temporal_objective;
	private IEnumerator desplazar;
	private IEnumerator regresar;
	private Tesoro tesoro_robado;
	public bool tiene_tesoro = false;


	//Print Text
	public TextMesh count;

	public void Update(){
		IninitCount();
	}

	private void IninitCount(){
		if (stamina > 0)
		{
			count.text = stamina.ToString();

		}else
		{
		 count.text = "";

		}
	}

    // Start is called before the first frame update
    void Start()
    {
    	start_position = transform.position;
    	final_position = temporal_objective.position;
        desplazar = Desplazar();
        regresar = Regresar();
        StartCoroutine(desplazar);
    }

    public void ReceiveDamage(int damage){
    	if (stamina >= 1) {
    		stamina -=damage;
    		Debug.Log(stamina);
    	}
		else {
			if(tiene_tesoro){
			tesoro_robado.transform.SetParent(null);
			tesoro_robado.Regresar();
			Destroy(gameObject);

			}
			else{
			Destroy(gameObject);
			}
		}
    }

    private void OnTriggerEnter2D(Collider2D collision){
    	if (collision.CompareTag("Tesoro")){
    		//Debug.Log("Lo atrapé jejejeje");
    		StopCoroutine(desplazar);
            TomarTesoro(collision.gameObject.GetComponent<Tesoro>());
    	}
    }

    private void TomarTesoro (Tesoro tesoro) {
        if (tesoro.resistencia == 0){
			tiene_tesoro = true;
			tesoro_robado = tesoro;
            StartCoroutine(regresar);
        } else {
            Destroy(gameObject);
        }
    }
    
    private IEnumerator Desplazar(){
    	while(true)
    	{
    		transform.position = Vector3.MoveTowards(transform.position, temporal_objective.position, velocity* Time.deltaTime);
    		yield return null;
    	}
    }

    private IEnumerator Regresar() {
    	while(true)
    	{
    		transform.position = Vector3.MoveTowards(transform.position, start_position, velocity* Time.deltaTime);
    		yield return null;
    	}
    }


}
