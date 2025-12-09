using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] public GameObject getUpTutorial;
    [SerializeField] private GameObject movementPanel;
    [SerializeField] private TMP_Text wsText;
    [SerializeField] private TMP_Text ikText;

    public bool w,s,i,k = false;

    private bool movementTutorialDone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movementTutorialDone) return;

        if(Input.GetKeyDown(KeyCode.W))
        {
            w = true;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            s = true;
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            i = true;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            k = true;
        }

        MovementTutorial();
    }

    public void MovementTutorial()
    {
        if(w && s)
        {
            wsText.color = Color.green;
        }

        if(i && k)
        {
            ikText.color = Color.green;  
        }

        if(w && s && i && k)
        {
            StartCoroutine(TutorialComplete());
        }
    }

    public IEnumerator TutorialComplete()
    {
        yield return new WaitForSeconds(1f);
        movementPanel.SetActive(false);
        movementTutorialDone = true;
    }
}
