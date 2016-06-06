using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class rightArmScript : MonoBehaviour {

    public Texture buttonImage;
    GameObject[] members = new GameObject[4];
    GameObject[] anchors = new GameObject[4];

    Vector2[] membersDefaultPosition = new Vector2[4];
    Vector3[] membersDefaultScale = new Vector3[4];
    Quaternion[] membersDefaultRotation = new Quaternion[4];

    Vector2[] anchorsDefaultPosition = new Vector2[4];
    Vector3[] anchorsDefaultScale = new Vector3[4];
    Quaternion[] anchorsDefaultRotation = new Quaternion[4];


    // Use this for initialization
    void Start ()
    {
        members[0] = GameObject.Find("rightArm");
        members[1] = GameObject.Find("leftArm");
        members[2] = GameObject.Find("rightLeg");
        members[3] = GameObject.Find("leftLeg");
        
        anchors[0] = GameObject.Find("anchorPointRightArm");
        anchors[1] = GameObject.Find("anchorPointLeftArm");
        anchors[2] = GameObject.Find("anchorPointRightLeg");
        anchors[3] = GameObject.Find("anchorPointLeftLeg");

        for (int i = 0; i < members.Length; i++)
        {
            membersDefaultPosition[i] = members[i].transform.position;
            membersDefaultScale[i] = members[i].transform.localScale;
            membersDefaultRotation[i] = members[i].transform.rotation;

            anchorsDefaultPosition[i] = anchors[i].transform.position;
            anchorsDefaultScale[i] = anchors[i].transform.localScale;
            anchorsDefaultRotation[i] = anchors[i].transform.rotation;
        }
    }
	
	// Update is called once per frame
	void Update () {

        //THIS IS A TEST!!

        GameObject body = GameObject.Find("body");

        Touch[] myTouchesAux = Input.touches;
        Touch[] myTouches = new Touch[members.Length];
        
        for(int i = 0; i < myTouchesAux.Length; i++)
        {
            float[] distancias = new float[anchors.Length];
            for(int j = 0; j < anchors.Length; j++)
            {
                distancias[j] = Mathf.Abs(Distance2Points(new Vector2(ConversionX(myTouchesAux[i].position.x), ConversionY(myTouchesAux[i].position.y)), anchorsDefaultPosition[j]));
            }
            for(int j = 0; j < anchors.Length; j++)
            {
                if(Mathf.Min(distancias) == distancias[j])
                {
                    myTouches[j] = myTouchesAux[i];
                }
            }
        }


        var mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition);

        Vector3[] newPositions = new Vector3[4];
        Vector3[] newScales = new Vector3[4];
        Quaternion[] newRotations = new Quaternion[4];
        Vector3[] touchPositionsConverted = new Vector3[4];
        Vector3[] membersStart = new Vector3[4];
        
        membersStart[0] = new Vector3(body.transform.position.x + 0.6f, body.transform.position.y + 0.45f, 0);
        membersStart[1] = new Vector3(body.transform.position.x - 0.6f, body.transform.position.y + 0.45f, 0);
        membersStart[2] = new Vector3(body.transform.position.x + 0.6f, body.transform.position.y - 0.45f, 0);
        membersStart[3] = new Vector3(body.transform.position.x - 0.6f, body.transform.position.y - 0.45f, 0);
        
        for(int i = 0; i < membersStart.Length; i++)
        {
            Debug.DrawLine(membersStart[i], new Vector3(ConversionX(mousePosition.x), ConversionY(mousePosition.y),0), Color.black);
        }


        for (int i = 0; i < myTouches.Length; i++)
        {
            if (myTouches[i].position == new Vector2(0, 0))
            {
                members[i].transform.position = membersDefaultPosition[i];
                members[i].transform.rotation = membersDefaultRotation[i];
                members[i].transform.localScale = membersDefaultScale[i];

                anchors[i].transform.position = anchorsDefaultPosition[i];
                anchors[i].transform.rotation = anchorsDefaultRotation[i];
                anchors[i].transform.localScale = anchorsDefaultScale[i];
            }
            else
            {
                touchPositionsConverted[i] = new Vector3(ConversionX(myTouches[i].position.x), ConversionY(myTouches[i].position.y), 0);
                Vector3 middlePoint = new Vector3((touchPositionsConverted[i].x - membersStart[i].x) / 2, (touchPositionsConverted[i].y - membersStart[i].y) / 2, 0);
                newScales[i] = new Vector3(0.2f, (1f * (Distance2Points(membersStart[i].x, touchPositionsConverted[i].x, membersStart[i].y, touchPositionsConverted[i].y)) / 1.5f), 1);
                newPositions[i] = new Vector3(membersStart[i].x + middlePoint.x, membersStart[i].y + middlePoint.y, 0);
                float angleRads = Mathf.Atan2((touchPositionsConverted[i].y - membersStart[i].y), (touchPositionsConverted[i].x - membersStart[i].x));
                float angleDegrees = 90 - (180f * angleRads) / Mathf.PI;
                newRotations[i] = Quaternion.AngleAxis(-angleDegrees, new Vector3(0, 0, 1));

                members[i].transform.position = newPositions[i];
                members[i].transform.localScale = newScales[i];
                members[i].transform.rotation = newRotations[i];
                anchors[i].transform.position = new Vector3(touchPositionsConverted[i].x, touchPositionsConverted[i].y, 0);
                anchors[i].transform.localScale = new Vector3(0.2f, 1, 1);
                anchors[i].transform.rotation = newRotations[i];
            }
        }
        /*
        for (int i = 0; i < myTouches.Length; i++)
        {
            touchPositionsConverted[i] = new Vector3(ConversionX(myTouches[i].position.x), ConversionY(myTouches[i].position.y), 0);
            Vector3 middlePoint = new Vector3((touchPositionsConverted[i].x - membersStart[i].x) / 2, (touchPositionsConverted[i].y - membersStart[i].y) / 2, 0);
            newScales[i] = new Vector3(0.2f, (1f * (Distance2Points(membersStart[i].x, touchPositionsConverted[i].x, membersStart[i].y, touchPositionsConverted[i].y)) / 1.5f), 1);
            newPositions[i] = new Vector3(membersStart[i].x + middlePoint.x, membersStart[i].y + middlePoint.y, 0);
            float angleRads = Mathf.Atan2((touchPositionsConverted[i].y - membersStart[i].y), (touchPositionsConverted[i].x - membersStart[i].x));
            float angleDegrees = 90 - (180f * angleRads) / Mathf.PI;
            newRotations[i] = Quaternion.AngleAxis(-angleDegrees, new Vector3(0, 0, 1));
        }
       
        for(int i = 0; i < myTouches.Length; i++)
        {
            members[i].transform.position = newPositions[i];
            members[i].transform.localScale = newScales[i];
            members[i].transform.rotation = newRotations[i];
            anchors[i].transform.position = new Vector3(touchPositionsConverted[i].x, touchPositionsConverted[i].y, 0);
            anchors[i].transform.localScale = new Vector3(0.2f, 1, 1);
            anchors[i].transform.rotation = newRotations[i];
        }
        */

        //Para fazer Debug, controlar braço direito com mouse
        /*
        float[] distancias = new float[anchors.Length];
        Debug.Log(new Vector2(ConversionX(mousePosition.x), ConversionY(mousePosition.y)));
        for(int i = 0; i < anchors.Length; i++)
        {
            distancias[i] = Distance2Points(anchors[i].transform.position, new Vector2(ConversionX(mousePosition.x), ConversionY(mousePosition.y)));
            Debug.Log("distancia " + i + " = " + distancias[i]);
        }
        int myThing = 0;
        for (int i = 0; i < distancias.Length; i++)
        {
            if(Mathf.Min(distancias) == distancias[i])
            {
                myThing = i;
                Debug.Log("ancora a fazer snap = " + myThing);
            }
        }
        Vector3 meioPonto = new Vector3((ConversionX(mousePosition.x) - membersStart[myThing].x) / 2, (ConversionY(mousePosition.y) - membersStart[myThing].y) / 2, 0);
        Vector3 novaPosicao = new Vector3(membersStart[myThing].x + meioPonto.x, membersStart[myThing].y + meioPonto.y);
        Vector3 novaEscala = new Vector3(0.2f, (1f * (Distance2Points(membersStart[myThing].x, ConversionX(mousePosition.x), membersStart[myThing].y, ConversionY(mousePosition.y))) / 1.5f), 1);
        float anguloRads = Mathf.Atan2((ConversionY(mousePosition.y) - membersStart[myThing].y), (ConversionX(mousePosition.x) - membersStart[myThing].x));
        float anguloGraus = 90 - (180f * anguloRads) / Mathf.PI;
        Quaternion novaRotacao = Quaternion.AngleAxis(-anguloGraus, new Vector3(0, 0, 1));

        members[myThing].transform.position = novaPosicao;
        members[myThing].transform.localScale = novaEscala;
        members[myThing].transform.rotation = novaRotacao;
        anchors[myThing].transform.rotation = novaRotacao;
        anchors[myThing].transform.position = new Vector3(ConversionX(mousePosition.x), ConversionY(mousePosition.y));
        anchors[myThing].transform.localScale = new Vector3(0.2f, 1, 1);
        */
        
    }

    float ConversionX(float pos)
    {
        return (6 * pos) / Screen.width;
    }

    float ConversionY (float pos)
    {
        return (10 * pos) / Screen.height;
    }

    float Distance2Points(float x1, float x2, float y1, float y2)
    {
        float xSquare = (x2 - x1) * (x2 - x1);
        float ySquare = (y2 - y1) * (y2 - y1);
        return (Mathf.Sqrt(xSquare + ySquare));
    }

    float Distance2Points(Vector2 v1, Vector2 v2)
    {
        float xSquare = (v2.x - v1.x) * (v2.x - v1.x);
        float ySquare = (v2.y - v1.y) * (v2.y - v1.y);
        return (Mathf.Sqrt(xSquare + ySquare));
    }

    public void Reset()
    {
        SceneManager.LoadScene("firstScene");
    }
}
