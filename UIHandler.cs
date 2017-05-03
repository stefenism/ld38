using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {
    public int money = 0;
    public Text cashRenderRef;

    private float dialPercent = 0;
    public Component dialStartRef;
    public Component dialEndRef;
    public Image dial;

    public Text timerRef;
    private float timeLeft= 420;
    public Color timerColorStart;
    public Color timerColorEnd;

    public float currentMass = 0;
    public float totalMass = 100;

    private float wait = 3;

    public ScreenFlash flash;

    // Use this for initialization
    void Start () {
	}

	// Update is called once per frame
	void Update () {
        if (timeLeft < 3)
        {
            flash.Lose();
        }
        if (timeLeft < -5)
        {
            Application.LoadLevel("Lose");
        }
        if (dialPercent >= 0.9)
        {
            flash.Win();
            if (wait <= 0)
            {
                Application.LoadLevel("Win");
            }
            wait -= Time.deltaTime;
        }
        //Cash

        cashRenderRef.text = money.ToString("n0");

        //Distruction dial
        dialPercent = currentMass / totalMass;
        dial.transform.position = Vector3.Lerp(dialStartRef.transform.position, dialEndRef.transform.position, dialPercent);

        //Timer
        timeLeft -= Time.deltaTime;
        string minutes = Mathf.Floor(timeLeft / 60).ToString("0");
        string seconds = (timeLeft % 60).ToString("00");
        if (Mathf.Floor(timeLeft / 60) > 0 || (timeLeft % 60) > 0)
        {
            timerRef.text = "Time to Impact: " + minutes + ":" + seconds;
            timerRef.color = Color.Lerp(timerColorEnd, timerColorStart, timeLeft / 300);
        }
    }
}
