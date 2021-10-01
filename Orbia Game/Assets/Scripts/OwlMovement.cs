using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OwlMovement : MonoBehaviour
{
    [SerializeField]private GameObject target;
    public float speed;
    public bool move = false;

    public GameObject currentCircle;                        //when shield used go back to previous circle

    GameObject level;
  
    public bool createMoreCircle=false;
    bool start;
    int i;

    private void Awake()
    {
        level = GameObject.Find("Level Generation");
      
        speed = level.GetComponent<LevelGenerator>().owlOriginalSpeed;
        i = 1;
    }

    private void Update()
    {

        
        start = level.GetComponent<LevelGenerator>().levelStarted;

        if (GetComponent<CollisionDetector>().isCollided != true )
        {
            if (start == true)
            {
                target = GameObject.Find("circle0");

                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed/1.5f * Time.deltaTime);
                if (transform.position == target.transform.position)
                {
                    level.GetComponent<LevelGenerator>().levelStarted = false;

                }
            }

            else
            {
                target = GameObject.Find("circle" + i);

                if (target==null)
                {
                    print("Next target is empty");
                }

                if (EventSystem.current.IsPointerOverGameObject())                                   // don't move owl when clicked on UI objects
                    return;

                if (Input.GetMouseButtonDown(0))
                {
                    move = true;
                }

                if (move == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                }
                if (transform.position == target.transform.position)
                {
                    i++;
                    move = false;

                }
                GameObject isLastCircle = GameObject.Find("circle5");
                if(transform.position == isLastCircle.transform.position)
                {
                    i = 1;
                   
                    GameObject.Find("Level Generation").GetComponent<LevelGenerator>().destroyPreviusCircles = true;
                }

            }

            currentCircle = GameObject.Find("circle" + (i - 1));
           


        }
    }
}


