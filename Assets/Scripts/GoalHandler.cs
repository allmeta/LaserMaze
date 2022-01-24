using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject burst;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      transform.eulerAngles -= Vector3.forward * Time.deltaTime * 50;
    }
    public void Kill(){
      GameObject p = Instantiate(burst, transform);
      GetComponent<SpriteRenderer>().enabled=false;
      Invoke("Destroy", 1f);
    }
    void Destroy(){
      Destroy(gameObject);
    }
}
