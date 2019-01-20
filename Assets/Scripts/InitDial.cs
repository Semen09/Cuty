using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InitDial : MonoBehaviour {
    [SerializeField]
    TextAsset dialogue;
    [SerializeField]
    Sprite[] faces;
    bool inDial = false;
    [SerializeField]
    GameObject dialogueUI;
    [SerializeField]
    Text dialogueTextField;
    [SerializeField]
    Image faceImage;
    [SerializeField]
    bool destroyAfter;
    [SerializeField]
    bool loadLevel;
    [SerializeField]
    string levelToLoad;
    int numOfCurPhrase;
    string[] phrases;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(inDial == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                numOfCurPhrase++;
                if ((phrases.Length - numOfCurPhrase) <= 0)
                {
                    CloseDialog();
                    return;
                }
                string[] currentPhrase = phrases[numOfCurPhrase].Split('|');
                faceImage.overrideSprite = faces[int.Parse(currentPhrase[0])];
                dialogueTextField.text = currentPhrase[1];
            }
        }
	}
    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == ("Player"))
        {
            ShowDialog();
        }
    }
    void ShowDialog()
    {
        numOfCurPhrase = 0;
        Time.timeScale = 0;
        dialogueUI.gameObject.SetActive(true);
        ReadFile();
        inDial = true;
        string[] currentPhrase = phrases[numOfCurPhrase].Split('|');
        faceImage.overrideSprite = faces[int.Parse(currentPhrase[0])];
        dialogueTextField.text = currentPhrase[1];
    }

    void CloseDialog()
    {
        inDial = false;
        dialogueUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        if (loadLevel)
        {
            this.gameObject.AddComponent<LoadScene>().LoadSceneGM(levelToLoad);
        }
        if (destroyAfter)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if(this.gameObject.GetComponent<BoxCollider2D>())
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if(this.gameObject.GetComponent<CircleCollider2D>())
                this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    void ReadFile()
    {
        phrases = dialogue.text.Split('\n');
    }

    public bool getStateDial()
    {
        return inDial;
    }
}
