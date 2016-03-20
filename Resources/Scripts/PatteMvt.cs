using UnityEngine;
using System.Collections;

public class PatteMvt : MonoBehaviour {

    public Transform positionInitiale;
    public Transform cible;
    public Transform seDeplaceVers;


    public AudioSource miaou;


    public bool enMouvement = false;

    public float speed = 1.0f;

    void Update()
    {
        
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, seDeplaceVers.position, step);
    }

    // Use this for initialization
    void Start () {
        //positionInitiale = this.transform;
        enMouvement = false;
        miaou = GameObject.FindGameObjectWithTag("miaou1").GetComponent<AudioSource>();


    }

    public void goCible() {
        speed = 0.5f;
        seDeplaceVers = cible;
    }

    public void goHome()
    {
        speed = 5.0f;
        seDeplaceVers = positionInitiale;
    }

    void OnMouseUp()
    {
        goHome();
        enMouvement = true;
        miaou.Play();
    }



}
