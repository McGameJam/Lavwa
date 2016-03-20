using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonClick : MonoBehaviour
{
    float x;
    float y;

    private Camera theCam;
    public bool dragging;
    public int enCollisionAvecSize = 0;

    public string enCollisionAvecStr = "";
    Rigidbody2D rigidbody;

    public List<GameObject> enCollisionAvec;

    void Start()
    {
        theCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
         rigidbody =  this.GetComponent<Rigidbody2D>();
        enCollisionAvec = new List<GameObject>();
    }





    void OnCollisionEnter2D(Collision2D coll)
    {
      //  Debug.Log("Me:" + this.name + " other: " + coll.gameObject.name + " ENTER ");
        enCollisionAvec.Add(coll.gameObject);
        enCollisionAvecSize++;
        enCollisionAvecStr += coll.gameObject.name;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
      //  Debug.Log("Me:" + this.name + " other: " + coll.gameObject.name + " EXIT ");
        enCollisionAvec.Remove(coll.gameObject);
        enCollisionAvecSize--;
        enCollisionAvecStr = enCollisionAvecStr.Remove(enCollisionAvecStr.Length - coll.gameObject.name.Length);

    }




    void LateUpdate()
    {
        // This draws a line (ray) from the camera at a distance of 3
        // If it hits something, it moves the object to that point, if not, it moves it to the end of the ray (3 units out)
        if (dragging)
        {
          
            if (Input.GetKey(KeyCode.Q)) {
                transform.Rotate(Vector3.forward, 100.0f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.back, 140.0f * Time.deltaTime);
            }
          
            Ray ray = theCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            

            if (Physics.Raycast(ray, out hit, 9.7f))
            {

                Vector3 temp = hit.point - transform.position;
                transform.position = hit.point;
            }
            else
            {
                float ztemp = transform.position.z;
                Vector3 temp = ray.GetPoint(9.7f) - transform.position;
             //   Debug.Log(" transform.position:" + transform.position+ " ray.GetPoint(10f) : " + ray.GetPoint(9.7f) +  " temp: " + temp);
                transform.position = ray.GetPoint(9.7f);// + temp/2;
                
                transform.position = new Vector3(transform.position.x, transform.position.y, ztemp);
            }
        }
    }

    float clickX, clickY;

    void OnMouseDown()
    {

        dragging = true;
        rigidbody.isKinematic = true;
        
    }

    // Adds the force when you let go based on the Mouse X/Y values.
    void OnMouseUp()
    {
        dragging = false;
        rigidbody.isKinematic = false;
        

        rigidbody.AddForce(theCam.transform.right * Input.GetAxis("Mouse X") * 20f, ForceMode2D.Impulse);
        rigidbody.AddForce(theCam.transform.up * Input.GetAxis("Mouse Y") * 20f, ForceMode2D.Impulse);
    }





    /*
    void OnCollisionEnter(Collision collision)
    {
        // if (other.gameObject.tag == "bullet")
        //     Destroy(gameObject);
        Debug.Log("collision");
        Debug.Log("Me:"+this.name +" other: "+ collision.gameObject.name);
    }


    */



    /*
    
    
    
    
    
    
    // Update is called once per frame 
    void Update()
    {
        x = Input.mousePosition.x;
        y = Input.mousePosition.y;
    }

    */









    /*  void OnMouseDrag()
      {
          this.GetComponent<Rigidbody2D>().isKinematic = true;
          transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10.0f));
      }

      void OnMouseUp() {
          Debug.Log("Drag ended!");
          this.GetComponent<Rigidbody2D>().isKinematic = false;

      }*/

}