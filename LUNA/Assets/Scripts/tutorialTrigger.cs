using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class tutorialTrigger : MonoBehaviour
{
    BoxCollider2D bcollider;
    [SerializeField] Image A;
    [SerializeField] Image D;
    [SerializeField] Image W;
    [SerializeField] Image S;
    [SerializeField] Image leftArrow;
    [SerializeField] Image rightArrow;
    [SerializeField] Image upArrow;
    [SerializeField] Image downArrow;
    [SerializeField] Image leftClick;
    [SerializeField] Image spaceBar;
    [SerializeField] Image Z;
    [SerializeField] Image X;
    [SerializeField] TextMeshProUGUI drillStartsText;
    [SerializeField] TextMeshProUGUI surviveText;
    AudioSource audioSource;

    private void Start()
    {
        bcollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && transform.position.x < -20)
        {
            A.gameObject.SetActive(false);
            D.gameObject.SetActive(false);
            leftArrow.gameObject.SetActive(false);
            rightArrow.gameObject.SetActive(false);

            W.gameObject.SetActive(true);
            S.gameObject.SetActive(true);
            upArrow.gameObject.SetActive(true);
            downArrow.gameObject.SetActive(true);

        }

        else if (collision.tag == "Player" && transform.position.x < 0)
        {
            W.gameObject.SetActive(false);
            S.gameObject.SetActive(false);
            upArrow.gameObject.SetActive(false);
            downArrow.gameObject.SetActive(false);

            spaceBar.gameObject.SetActive(true);
            X.gameObject.SetActive(true);

        }
        else if (collision.tag == "Player" && transform.position.x > 19)
        {
            A.gameObject.SetActive(false);
            D.gameObject.SetActive(false);
            leftArrow.gameObject.SetActive(false);
            rightArrow.gameObject.SetActive(false);
            S.gameObject.SetActive(false);
            downArrow.gameObject.SetActive(false);
            spaceBar.gameObject.SetActive(false);
            leftClick.gameObject.SetActive(false);
            Z.gameObject.SetActive(false);
            X.gameObject.SetActive(false);

            audioSource.Stop();

            upArrow.gameObject.SetActive(true);
            W.gameObject.SetActive(true);
            StartCoroutine(TutorialTexts());
        }
    
        else if (collision.tag == "Player" && transform.position.x > 0)
        {
            A.gameObject.SetActive(false);
            D.gameObject.SetActive(false);
            leftArrow.gameObject.SetActive(false);
            rightArrow.gameObject.SetActive(false);
            W.gameObject.SetActive(false);
            S.gameObject.SetActive(false);
            upArrow.gameObject.SetActive(false);
            downArrow.gameObject.SetActive(false);
            spaceBar.gameObject.SetActive(false);
            X.gameObject.SetActive(false);

            Z.gameObject.SetActive(true);
            leftClick.gameObject.SetActive(true);

        }
    }
    IEnumerator TutorialTexts()
    {
        yield return new WaitForSeconds(8);
        W.gameObject.SetActive(false);
        upArrow.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        drillStartsText.gameObject.SetActive(true);

       
        yield return new WaitForSeconds(6);
        drillStartsText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        surviveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);
        surviveText.gameObject.SetActive(false);



    }
}
