
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    Transform player,background,light1,cam;
    

    public float camPosition;
   
    private void Start()
    {
        player = GameObject.Find("Owl").GetComponent<Transform>();
        background = GameObject.Find("Background Holder").GetComponent<Transform>();
        light1 = GameObject.Find("Freeform Light 2D").GetComponent<Transform>();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
       
    }

    private void LateUpdate()
    {
 

        if ((player.position.y+camPosition) > transform.position.y) 
        {
            transform.position = new Vector3(transform.position.x, player.position.y + camPosition, transform.position.z);
            background.position = new Vector3(background.position.x, player.position.y + camPosition, transform.position.z);
            light1.position =  new Vector3(0.65f , cam.position.y+(-7.27f) , 0);
 
        }

        if ((player.position.y + camPosition) < transform.position.y )
        {
            Vector3 V = new Vector3(transform.position.x, player.position.y - camPosition, transform.position.z);
            Vector3 bg = new Vector3(background.position.x, player.position.y - camPosition, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position , V , 5*Time.deltaTime);
            background.position= Vector3.MoveTowards(background.position, bg, 5*Time.deltaTime);


        }


    }


}
