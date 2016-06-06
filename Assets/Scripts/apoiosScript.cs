using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class apoiosScript : MonoBehaviour {
    //private Sprite apoioActivado;
    public static Text pointsText;
    public static int points;

    // Use this for initialization
    void Start ()
    {
        pointsText = GameObject.Find("PointsText").GetComponent<Text>();
        pointsText.fontSize = Screen.width / 20;
        //SpriteRenderer rendererActivo = GameObject.Find("apoiosActivadosSprite").GetComponent("SpriteRenderer") as SpriteRenderer;
        //apoioActivado = rendererActivo.sprite;
    }
	
	// Update is called once per frame
	void Update () {
        pointsText.text = "Points: " + points;
	}
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "anchorPointRightArm" || col.gameObject.name == "anchorPointLeftArm" || col.gameObject.name == "anchorPointRightLeg" || col.gameObject.name == "anchorPointLeftLeg")
        {
            //SpriteRenderer renderer = this.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
            //renderer.sprite = apoioActivado;
            points++;
            Destroy(gameObject);
        }
    }
}
