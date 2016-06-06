using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class backgroundScript : MonoBehaviour {

    public int numApoios;

	// Use this for initialization
	void Start () {
        apoiosScript.points = 0;
        CreateApoios();
    }
	
	// Update is called once per frame
	void Update () {
        var camera = GameObject.Find("Main Camera");
        var position = new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z);
        transform.position = position;


        GameObject[] apoiosRestantes = GameObject.FindGameObjectsWithTag("apoio");
        if(apoiosRestantes.Length == 1)
        {
            CreateApoios();
        }
	}

    void CreateApoios()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 0.5f);
        GameObject[] apoios = new GameObject[numApoios];
        for (int i = 0; i < apoios.Length; i++)
        {
            apoios[i] = Instantiate(GameObject.Find("apoiosSprite")) as GameObject;
            apoios[i].transform.position = new Vector2(0, 0);
        }
        for (int i = 0; i < apoios.Length; i++)
        {
            apoios[i].transform.position = new Vector2(Random.Range(0.45f, 5.55f), Random.Range(0.1f, 9.9f));
        }
        for (int i = 0; i < apoios.Length; i++)
        {
            for (int j = 0; j < apoios.Length; j++)
            {
                while (i != j && (apoios[i].transform.position.x <= apoios[j].transform.position.x + 0.5f && apoios[i].transform.position.x >= apoios[j].transform.position.x - 0.5f &&
                    apoios[i].transform.position.y <= apoios[j].transform.position.y + 0.2f && apoios[i].transform.position.y >= apoios[j].transform.position.y - 0.2f))
                {
                    apoios[i].transform.position = new Vector2(Random.Range(0.45f, 5.55f), Random.Range(0.1f, 9.9f));
                }
            }
        }
    }
}
