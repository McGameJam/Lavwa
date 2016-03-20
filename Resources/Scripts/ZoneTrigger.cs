using UnityEngine;
using System.Collections;

public class ZoneTrigger : MonoBehaviour {

    public GoalBase goalBase;

    public GameObject balleRouge;
    public GameObject balleBleu;
    public GameObject balleCible;

	// Use this for initialization
	void Start () {
        goalBase = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GoalBase>();
        balleRouge = GameObject.FindGameObjectWithTag("TennisRouge");
        balleBleu = GameObject.FindGameObjectWithTag("TennisBleu");
        init();
    }
    public void init()
    {
        if (this.tag.Equals("ZoneBleu"))
        {
            balleCible = balleBleu;
        }
        else {
            balleCible = balleRouge;
        }
    }
            
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject jetaisOK = null;

    /*c.gameObject.GetComponent<Rigidbody2D>().IsSleeping()*/
    void OnTriggerStay2D(Collider2D c) {
       
        if (balleCible == c.gameObject)
        {
            if (c.gameObject.GetComponent<ButtonClick>().dragging == false) {
                Debug.Log(c.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
                if (c.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.01)
                {
                    Debug.Log("OnTriggerEnter2D ZONETRIGGER");
                    goalBase.zoneEnter(this, c.gameObject);
                    jetaisOK = c.gameObject;
                }
            }
        } 
    
    }

    void OnTriggerExit2D(Collider2D c) {
        if (c.gameObject == jetaisOK) {
            goalBase.zoneExit(this, c.gameObject);
        }

    } 
   

    void OnTriggerEnter2D(Collider2D c)
    {

        if (balleRouge == c.gameObject || balleBleu == c.gameObject)
        {
            if (this.tag.Equals("ZoneBleu") || this.tag.Equals("ZoneRouge"))
            {

            }
            else {
                Debug.Log("Balle Exit Screen");
                goalBase.perdreVie(1);
            }
        }

    }
}
