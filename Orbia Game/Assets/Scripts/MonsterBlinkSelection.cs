using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBlinkSelection : MonoBehaviour
{
    List<float> breakTime = new List<float>();
    public Animator anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        breakTime.Add(5);
        breakTime.Add(10);
        breakTime.Add(15);
        breakTime.Add(20);
        breakTime.Add(25);
        breakTime.Add(30);

      
        InvokeRepeating("DontBlink", 5 , 20);
    }

   
    void DontBlink()
    {
        if (anim.GetBool("Blinking") == false)
        {
            Invoke("Blink", breakTime[Random.Range(0,breakTime.Count)]);
        }
       
           
    }

    void Blink()
    {
        anim.SetBool("Blinking", true);
        Invoke("Blink1", 0.25f);
    }
    
    void Blink1()
    {
        anim.SetBool("Blinking", false);
    }

}
