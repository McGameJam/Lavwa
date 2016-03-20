using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalBase : MonoBehaviour
{
    float x;
    float y;

    public int enCollisionAvecSize = 0;

    public string enCollisionAvecStr = "";

    public GameObject tier1_1;
    public GameObject tier1_2;

    public PatteMvt patteDroite;
    public PatteMvt chatFace;
    public PatteMvt patteGauche;


    public TextMesh tempsSeconde;
    public TextMesh messageJoueurCentre;
    private Camera theCam;
    private bool dragging;

    private GameObject coeur1;
    private GameObject coeur2;
    private GameObject coeur3;

    


    Rigidbody2D rigidbody;

    public int lifeLeft;

    public List<GameObject> enCollisionAvec;

    public float timeRunning = 200;

    void initGame() {
        lifeLeft = 3;
        PlayerPrefs.SetInt("lifeLeft",lifeLeft);

    }

    void displayLife() {

    }


    void initComponents() {
        tempsSeconde = GameObject.FindGameObjectWithTag("ClockSec").GetComponent<TextMesh>();
        messageJoueurCentre = GameObject.FindGameObjectWithTag("messageJoueurCentre").GetComponent<TextMesh>();
        patteDroite = GameObject.FindGameObjectWithTag("PatteDroite").GetComponent<PatteMvt>();
        patteGauche = GameObject.FindGameObjectWithTag("PatteGauche").GetComponent<PatteMvt>();
        chatFace = GameObject.FindGameObjectWithTag("ChatFace").GetComponent<PatteMvt>();
        coeur1 = GameObject.FindGameObjectWithTag("Coeur1");
        coeur2 = GameObject.FindGameObjectWithTag("Coeur2");
        coeur3 = GameObject.FindGameObjectWithTag("Coeur3");
    }

    void initLevel() {
        Debug.Log("goHome");
        patteDroite.goHome();
        patteGauche.goHome();
        chatFace.goHome();
        if (lifeLeft == 3) {
            coeur1.active = true;
            coeur2.active = true;
            coeur3.active = true;
        }
        if (lifeLeft == 2)
        {
            coeur1.active = false;
            coeur2.active = true;
            coeur3.active = true;
        }
        if (lifeLeft == 1)
        {
            coeur1.active = false;
            coeur2.active = false;
            coeur3.active = true;
        }

    }

    IEnumerator interference() {
        Debug.Log("interference1");
        initLevel();
               yield return new WaitForSeconds(10);
         //   StartCoroutine(patteDroiteMove());
           // StartCoroutine(patteGaucheMove());
            StartCoroutine(chatFaceMove());
        Debug.Log("interference5");

        yield return new WaitForSeconds(5);
        StartCoroutine(patteDroiteMove());
    //   StartCoroutine(patteGaucheMove());
   //     StartCoroutine(chatFaceMove());
        Debug.Log("interference10");


        yield return new WaitForSeconds(10);
   //     StartCoroutine(patteDroiteMove());
      StartCoroutine(patteGaucheMove());
    //    StartCoroutine(chatFaceMove());

        Debug.Log("interference15");

        yield return new WaitForSeconds(15);
        StartCoroutine(patteDroiteMove());
   //     StartCoroutine(patteGaucheMove());
  //      StartCoroutine(chatFaceMove());


        yield return new WaitForSeconds(20);
  //      StartCoroutine(patteDroiteMove());
   //     StartCoroutine(patteGaucheMove());
        StartCoroutine(chatFaceMove());

        yield return new WaitForSeconds(25);
        StartCoroutine(patteDroiteMove());
        StartCoroutine(patteGaucheMove());
        StartCoroutine(chatFaceMove());

        yield return new WaitForSeconds(30);
        StartCoroutine(patteDroiteMove());
        StartCoroutine(patteGaucheMove());
        StartCoroutine(chatFaceMove());

        yield return new WaitForSeconds(35);
        StartCoroutine(patteDroiteMove());
        StartCoroutine(patteGaucheMove());
        StartCoroutine(chatFaceMove());


        yield return new WaitForSeconds(40);
        StartCoroutine(patteDroiteMove());
        StartCoroutine(patteGaucheMove());
        StartCoroutine(chatFaceMove());



        yield return new WaitForSeconds(45);
        StartCoroutine(patteDroiteMove());
        StartCoroutine(patteGaucheMove());
        StartCoroutine(chatFaceMove());
    }

    IEnumerator patteDroiteMove()
    {
        yield return new WaitForSeconds(randomInt);
        patteDroite.goCible();
    }


    IEnumerator patteGaucheMove()
    {
        yield return new WaitForSeconds(randomInt);
        patteGauche.goCible();
    }

    IEnumerator chatFaceMove()
    {
        yield return new WaitForSeconds(randomInt);
        chatFace.goCible();
    }






    void Start()
    {

        lifeLeft = PlayerPrefs.GetInt("lifeLeft");
        if (lifeLeft <= 0) {
            initGame();

        }
        initComponents();

        theCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        enCollisionAvec = new List<GameObject>();
        Debug.Log("initlevel");
        initLevel();
        Debug.Log("interference");
        StartCoroutine(interference());
    }

    public void checkWin() {
        if (redWin != null && blueWin != null) {
            isPaused = true;
            StartCoroutine(winGame());
        }
    }

    IEnumerator winGame()
    {
        messageJoueurCentre.text = "Congratulation, you won ! ";
       // audioWin.play();
        yield return new WaitForSeconds(10);
        Application.LoadLevel(0); // 0 = main menu
    }



    public GameObject redWin = null;
    public GameObject blueWin = null;


    public void zoneEnter(ZoneTrigger zoneColorOk, GameObject other) {
        if (zoneColorOk.tag.Equals("ZoneRouge"))
        {
            redWin = other;
        }
        else {
            blueWin = other;
        }

        Debug.Log("zoneEnter " + zoneColorOk.name + " other : " + other.name);
            
    }

    public void zoneExit(ZoneTrigger zoneColorOk, GameObject other)
    {

        Debug.Log("zoneEnter " + zoneColorOk.name + " other : " + other.name);
        if (zoneColorOk.tag.Equals("ZoneRouge"))
        {
            redWin = null;
        }
        else {
            blueWin = null;
        }

    }

    bool isPaused = false;

    public void perdreVie(int status)
    {
        if (!isPaused) {
        if (status == 0)
        {
            isPaused = true;
            lifeLeft--;
            PlayerPrefs.SetInt("lifeLeft", lifeLeft);
            StartCoroutine(coLostLife());
        }
        else {
            isPaused = true;
            lifeLeft--;
            PlayerPrefs.SetInt("lifeLeft", lifeLeft);
            StartCoroutine(coLostLifeExit());
        }
        }
    }

    IEnumerator coLostLife()
    {
        messageJoueurCentre.text = "Oh no.... you lost a life, time ran out !";

        yield return new WaitForSeconds(5);
        Application.LoadLevel(Application.loadedLevel);
    }
    
    IEnumerator coLostLifeExit()
    {
        messageJoueurCentre.text = "Oh no.... you lost a life, you lost a ball !";

        yield return new WaitForSeconds(5);
        Application.LoadLevel(Application.loadedLevel);
    }


    public void checkForWin() {
        if (timeRunning <= 0) {
            perdreVie(0);

        }
        checkLoseGame(); checkWin();
    }

    public void checkLoseGame() {
       // Debug.Log(" checkLoseGame ");
        if (lifeLeft <= 0) {
            Debug.Log(" GAMEOVER ");
            StartCoroutine(coLostGame());

        }
    }
    IEnumerator coLostGame()
    {
            messageJoueurCentre.text = "Oh no.... you lost the game";
            yield return new WaitForSeconds(3);
            Application.LoadLevel(0); // 0 = main menu
    }

    int randomInt = 9;

        // Update is called once per frame 
        void Update()
    {
        if (isPaused == false) {

            timeRunning -= Time.deltaTime;
            tempsSeconde.text = ((int)timeRunning).ToString();//""+10; // ((int)timeRunning).ToString() ;
            checkForWin();
            randomInt = (int)Random.RandomRange(2, 8);
        }

        if (Input.GetKey(KeyCode.I))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    









}