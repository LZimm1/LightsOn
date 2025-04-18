using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject completeButton;
    [SerializeField]
    private GameObject darkness;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Character")){
            Destroy(darkness);
            Destroy(gameObject);
        }
    }
}
