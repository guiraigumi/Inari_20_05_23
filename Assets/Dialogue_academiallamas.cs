using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Dialogue_academiallamas : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    public bool isTalking;
    private int lineIndex;
    GameObject target;
    //GameObject npc;
    private Quaternion originalYRotation;
    public GameObject objectToActivate;
    public bool conversationEnded = false;
    private BGMManager bgmManager;



    Animator anim;
    Animator anim2;
    Animator anim3;
    public Player_invert player;

    private float typingTime = 0.01f;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private GameObject RawImage;
    [SerializeField] private GameObject librariaTalking;
    private SFXManager sfxManager;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField] private GameObject npc1; // Assign in inspector
    [SerializeField] private GameObject npc2; // Assign in inspector
    [SerializeField] private GameObject npc3; // Assign in inspector

     [SerializeField] private GameObject particle1;
     [SerializeField] private GameObject particle2;
     [SerializeField] private GameObject particle3;
     [SerializeField] private GameObject particle4;
     [SerializeField] private GameObject particle5;
     [SerializeField] private GameObject particle6;

    void Awake()
    {
        anim = npc1.GetComponent<Animator>();
        anim2 = npc2.GetComponent<Animator>();
        anim3 = npc3.GetComponent<Animator>();
        player = GameObject.FindObjectOfType<Player_invert>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {

            if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();

                dialogueText.text = dialogueLines[lineIndex];
            }
        }


        if (lineIndex >= dialogueLines.Length && !conversationEnded)
        {
            conversationEnded = true;
            bgmManager.StopBGM();
            objectToActivate.SetActive(true);
            sfxManager.FallingSound();
            particle1.SetActive(false);
            particle2.SetActive(false);
            particle3.SetActive(false);
            particle4.SetActive(false);
            particle5.SetActive(false);
            particle6.SetActive(false);
        }

    }

    /*void AfterCinematic()
    {
        Debug.Log("AFTER WAITING");
        if (conversationEnded)
        {
            SceneManager.LoadScene("Scene_16_1");
        }
    }*/

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        backgroundImage.SetActive(true);
        RawImage.SetActive(true);
        librariaTalking.SetActive(true);
        lineIndex = 0;

        StartCoroutine(ShowLine());
        target = GameObject.Find("Front");
        //npc = GameObject.Find("Bibliotecaria");
        //originalYRotation = npc.transform.rotation;
        Debug.Log("Rotation NPC: " + originalYRotation);
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        //npc.transform.LookAt(targetPosition);
        player.isplayerTalking = true;
    }


    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            SetIdleAnimations();
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            backgroundImage.SetActive(false);
            RawImage.SetActive(false);
            librariaTalking.SetActive(false);
            anim.SetBool("isTalking", false);
            anim2.SetBool("isTalking", false);
            anim3.SetBool("isTalking", false);
            //npc = GameObject.Find("Bibliotecaria");

            //npc.transform.SetPositionAndRotation(new Vector3(npc.transform.position.x, npc.transform.position.y, npc.transform.position.z), originalYRotation);
            player.isplayerTalking = false;
            //Time.timeScale = 1f;
        }

        if (lineIndex >= dialogueLines.Length)
        {
            Debug.Log("Conversation ended!");
            objectToActivate.SetActive(true);
        }
    }


    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        // switch the animation for the three NPCs
        if (lineIndex % 3 == 0)
        {
            anim.SetBool("isTalking", true);
            anim2.SetBool("isTalking", false);
            anim3.SetBool("isTalking", false);
        }
        else if (lineIndex % 3 == 1)
        {
            anim.SetBool("isTalking", false);
            anim2.SetBool("isTalking", true);
            anim3.SetBool("isTalking", false);
        }
        else
        {
            anim.SetBool("isTalking", false);
            anim2.SetBool("isTalking", false);
            anim3.SetBool("isTalking", true);
        }

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            sfxManager.TypingSound();

            // switch the animation for the three NPCs
            if (lineIndex % 3 == 0)
            {
                anim.SetBool("isTalking", true);
                anim2.SetBool("isTalking", false);
                anim3.SetBool("isTalking", false);
            }
            else if (lineIndex % 3 == 1)
            {
                anim.SetBool("isTalking", false);
                anim2.SetBool("isTalking", true);
                anim3.SetBool("isTalking", false);
            }
            else
            {
                anim.SetBool("isTalking", false);
                anim2.SetBool("isTalking", false);
                anim3.SetBool("isTalking", true);
            }

            yield return new WaitForSecondsRealtime(typingTime);
            yield return null;
        }

        SetIdleAnimations();
    }

    void SetIdleAnimations()
    {
        anim.SetBool("isTalking", false);
        anim2.SetBool("isTalking", false);
        anim3.SetBool("isTalking", false);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Ruhe") ||
            collision.gameObject.CompareTag("Hangin") ||
            collision.gameObject.CompareTag("Kalju"))
        {
            isPlayerInRange = true;
            player.isplayerTalking = true;
            StartDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited");
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            StopAllCoroutines();

            if (didDialogueStart)
            {
                dialoguePanel.SetActive(false);
                backgroundImage.SetActive(false);
                RawImage.SetActive(false);
                anim.SetBool("isTalking", false);
                anim2.SetBool("isTalking", false);
                anim3.SetBool("isTalking", false);
                //npc = GameObject.Find("Bibliotecaria");

                //npc.transform.SetPositionAndRotation(new Vector3(npc.transform.position.x, npc.transform.position.y, npc.transform.position.z), originalYRotation);
                player.isplayerTalking = false;
            }
        }
    }
}