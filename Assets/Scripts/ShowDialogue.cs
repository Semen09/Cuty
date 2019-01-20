using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowDialogue : MonoBehaviour {
    public bool inDialogue = false;
    int numberOfPhrases;
    int currentPhrase = 0;
    string[] phrases;
    private GameObject target;
    private Text textField;
    bool destroyAfterConversation = false;
    //public GameObject spawnPrefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(inDialogue)
        {
            //if we pushing enter or whatever
            if (Input.GetButtonDown("Fire1"))
            {
                UpdateDialogue();
                currentPhrase++;
                if((numberOfPhrases - currentPhrase) <= 0)
                {
                    EndDialogue();
                    return;
                }
                textField.text = phrases[currentPhrase];
            }
        }
	}
    
    //creating dialogue first time
    public void CreateDialogue(int numberOfPhrases, string[] phrases, GameObject target, bool d)
    {
        //getting the phrases
        this.numberOfPhrases = numberOfPhrases;
        this.phrases = phrases;
        this.target = target;
        destroyAfterConversation = d;
        
        Time.timeScale = 0f;
        ActivateCloud(true);
        inDialogue = true;

        foreach (Transform trans in gameObject.transform.GetComponentInChildren<Transform>())
        {
            if (trans.Find("DialogueCloud"))
            {
                textField = trans.Find("DialogueCloud").Find("Text").GetComponent<Text>();
            }
        }
        textField.text = phrases[currentPhrase];
    }
    void UpdateDialogue()
    {

    }
    void EndDialogue()
    {
        Time.timeScale = 1f;
        ActivateCloud(false);
        inDialogue = false;
        if (destroyAfterConversation)
        {
            Destroy(target.gameObject);
        }
        else
            target.gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    void ActivateCloud(bool trig)
    {
        foreach (Transform trans in gameObject.transform.GetComponentInChildren<Transform>())
        {
            if (trans.Find("DialogueCloud"))
            {
                trans.Find("DialogueCloud").gameObject.SetActive(trig);
            }
        }
    }
}
