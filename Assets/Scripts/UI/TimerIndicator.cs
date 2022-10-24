using UnityEngine;
using TMPro;

public class TimerIndicator : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private float timer;
    private bool timerIsRunning = false;
    private string timerName;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        TimerStart(timer);
        RefreshUI();
    }

    public void TimerStart(float timerMax)
    {
        timer = timerMax;
        timerIsRunning = true;
        if (timerIsRunning)
        {
           if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
           
           if (timer < 1f)
            {
                this.gameObject.SetActive(false);
                timerIsRunning = false;
            }
        }
    }

    public string TimerName(string name)
    {
        timerName = name;
        return timerName;
    }
    private void RefreshUI()
    {
        timerText.text = timerName + "Timer : " + (int)timer;
    }
}
