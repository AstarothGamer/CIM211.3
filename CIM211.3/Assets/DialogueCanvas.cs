using System;
using System.Collections;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{
    public GameObject box;
    public TextAnimator_TMP textAnimator;
    public TypewriterByCharacter typewriter;
    
    public float duration = 8f;

    protected GameObject camera;
    
    private void Awake()
    {
        typewriter.onTextShowed.AddListener(OnTextShowed);
        textAnimator.SetTextToSource("");
        box.SetActive(false);
        camera = FindFirstObjectByType<Camera>().gameObject;
    }

    private void Update()
    {
        if (!box.activeInHierarchy)
            return;
        
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
    }

    private void OnDestroy()
    {
        typewriter.onTextShowed.RemoveListener(OnTextShowed);
    }

    public void ShowDialogue(string text)
    {
        box.SetActive(true);
        textAnimator.SetText(text);
    }

    void OnTextShowed()
    {
        StartCoroutine(HideAfterDelay());
    }


    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(duration);
        textAnimator.SetText("");
        box.SetActive(false);
    }
    
}
