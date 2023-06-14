using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public float countDownTimer = 5f;

    [Header("Thing to stop")]
    public PlayerCarController playerCarController;
    public OpponentCar opponentCar;
    public OpponentCar OpponentCarMegan;
    public OpponentCar OpponentCarDiesel;
    public OpponentCar OpponentCarZegato;
    public OpponentCar OpponentCarThunder;
    public OpponentCar OpponentCarStranger;
    public OpponentCar OpponentCarLagoon;

    public Text countDownText;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeCount());
    }

    // Update is called once per frame
    void Update()
    {
        if (countDownTimer > 1)
        {
            playerCarController.accelerationForce = 0f;
            opponentCar.movingSpeed = 0f;
            OpponentCarMegan.movingSpeed = 0f;
            OpponentCarDiesel.movingSpeed = 0f;
            OpponentCarZegato.movingSpeed = 0f;
            OpponentCarThunder.movingSpeed = 0f;
            OpponentCarStranger.movingSpeed = 0f;
            OpponentCarLagoon.movingSpeed = 0f;
        }

        if (countDownTimer == 0)
        {
            playerCarController.accelerationForce = 200f;
            opponentCar.movingSpeed = 7f;
            OpponentCarMegan.movingSpeed = 6f;
            OpponentCarDiesel.movingSpeed = 8f;
            OpponentCarZegato.movingSpeed = 7.5f;
            OpponentCarThunder.movingSpeed = 6.5f;
            OpponentCarStranger.movingSpeed = 8.7f;
            OpponentCarLagoon.movingSpeed = 7.8f;
        }
    }

    IEnumerator TimeCount()
    {
        while (countDownTimer > 0)
        {
            countDownText.text = countDownTimer.ToString();
            yield return new WaitForSeconds(1f);
            countDownTimer--;
        }
        countDownText.text = "Go";
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);

    }
}
