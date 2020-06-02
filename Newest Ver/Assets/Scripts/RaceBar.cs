using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceBar : MonoBehaviour
{
    [SerializeField]
    public Text text;
    [SerializeField]
    public Image barFill;
    [SerializeField]
    public Image barOutline;
    [SerializeField]
    public Image circleFill;
    [SerializeField]
    public Image circleOutline;
    [SerializeField]
    public Color color;
    [SerializeField]
    public Color background;
    public int level = 0;
    public float currentAmount = 0;
    public Coroutine routine;
    public void OnEnable()
    {
        InitColor();
        level = 0;
        currentAmount = 0;
        barFill.fillAmount = currentAmount;
        UpdateLevel(level);
    }

    public void InitColor()
    {
        circleFill.color = color;
        circleOutline.color = color;
        barFill.color = color;
        barOutline.color = color;
        text.color = color;
    }

    public void UpdateProgress(float amount, float duration = 0.1f)
    {
        if (routine != null)
        StopCoroutine(routine);
        float target = currentAmount + amount;
        routine = StartCoroutine(FillRoutine(target, duration));
    }

    public IEnumerator FillRoutine(float target, float duration)
    {
        float time = 0;
        float tempAmount = currentAmount;
        float diff = target - tempAmount;
        currentAmount = target;

        while (time < duration)
        {
            time += Time.deltaTime;
            float percent = time / duration;
            barFill.fillAmount = tempAmount + diff * percent;
            yield return null;
        }

        if (currentAmount >= 1)
            LevelUp();
    }

    public void LevelUp()
    {
        UpdateLevel(level + 1);
        UpdateProgress(-1f, 1.0f);
    }

    public void UpdateLevel(int level)
    {
        this.level = level;
        text.text = (this.level+1).ToString();
    }

}
