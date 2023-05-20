using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Dialogue_mirador_Hangin2 : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    public bool isTalking;
    private int lineIndex;
    GameObject target;
    GameObject npc;


    Animator anim;
    Animator anim2;
    Animator anim3;
    public Player_invert player;

    private float typingTime = 0.01f;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private GameObject RawImage;
    [SerializeField] private GameObject librariaTalking;
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public GameObject musicToActivate;
    public bool conversationEnded = false;
    private BGMManager bgmManager;
    private SFXManager sfxManager;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [SerializeField] private GameObject npc1; // Assign in inspector
    [SerializeField] private GameObject npc2; // Assign in inspector
    [SerializeField] private GameObject npc3; // Assign in inspector

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
        StartDialogue(); // Call the StartDialogue method when the scene awakes
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Submit"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();

                dialogueText.text = dialogueLines[lineIndex];
            }

        }

    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        backgroundImage.SetActive(true);
        RawImage.SetActive(true);
        librariaTalking.SetActive(true);
        musicToActivate.SetActive(true);
        objectToDeactivate.SetActive(false);
        lineIndex = 0;

        StartCoroutine(ShowLine());
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        npc.transform.LookAt(targetPosition);
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
            objectToActivate.SetActive(true);
            anim.SetBool("isTalking", false);
            anim2.SetBool("isTalking", false);
            anim3.SetBool("isTalking", false);
            player.isplayerTalking = false;
            //Time.timeScale = 1f;
        }

        if (lineIndex >= dialogueLines.Length)
        {
            conversationEnded = true;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited");
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            StopAllCoroutines();

            if (didDialogueStart)
            {
                dialoguePanel.SetActive(false);
                backgroundImage.SetActive(false);
                RawImage.SetActive(false);
                anim.SetBool("isTalking", false);
                anim2.SetBool("isTalking", false);
                anim3.SetBool("isTalking", false);
                player.isplayerTalking = false;
            }
        }
    }
}
