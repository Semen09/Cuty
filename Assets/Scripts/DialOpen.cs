using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Old script for dialogue system
/// </summary>
public class DialOpen : MonoBehaviour {

    public bool inDialogue = false;
    [SerializeField]
    bool destroyAfterConversation = false;

    public int numberOfPhrases;
    public string[] phrases;// = new string[numberOfPhrases];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inDialogue = true;
            //freeze game until end of dialogue
            collision.gameObject.GetComponent<ShowDialogue>().CreateDialogue(phrases.Length, phrases, this.gameObject, destroyAfterConversation);
        }
    }
}
