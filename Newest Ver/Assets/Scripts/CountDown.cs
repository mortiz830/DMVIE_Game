using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : MonoBehaviour
{
    public int countDownTime;
    public GameObject countDownText;
    public AudioSource countDown;

    public void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countDownTime > 0)
        {
            countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }

        countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = "GO!!";
        yield return new WaitForSeconds(1f);
        countDownText.SetActive(false);
    }
}

