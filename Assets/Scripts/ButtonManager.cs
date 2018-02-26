using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerClickHandler {

    public ButtonManager redButton;
    public ButtonManager greenButton;
    public ButtonManager blueButton;
    public ButtonManager goldButton;
    public Color fillColor;
    public enum ButtonColor {Red, Green, Blue, Gold}
    public ButtonColor buttonColor;
    public PlayerControl owner;
    private string myColor;
    public int levelMax = 200;
    public int myLevel;
    private Image barImage;

    public int spinResultRed = 0;
    public int spinResultGreen = 0;
    public int spinResultBlue = 0;

    private Slider mySlider;

    public delegate void ButtonPressAction(int value1, int value2, int value3);
    public event ButtonPressAction OnPressed;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(PlayerControl.clickCooldown <= 0)
        {
            owner.Press(myColor, myLevel);
            PlayerControl.clickCooldown = 1;// + 0.2f * myLevel;
            RecalculateLevel(-myLevel, -myLevel, -myLevel);
            if(myColor == "Gold")
            {
               // OnPressed(spinResult);
            }
            else
            {
                OnPressed(0,0,0);
            }
        }
    }
    public void SpinPressed()
    {
        OnPressed(spinResultRed, spinResultGreen, spinResultBlue);
    }

    // Use this for initialization
    void Awake () {
        mySlider = GetComponent<Slider>();
        barImage = mySlider.fillRect.GetComponent<Image>();
        myLevel = 1;
        RecalculateLevel(0,0,0);
    }

    private void Start()
    {
        switch (buttonColor)
        {
            case ButtonColor.Red:
                myColor = "Red";
                greenButton.OnPressed += RecalculateLevel;
                blueButton.OnPressed += RecalculateLevel;
                goldButton.OnPressed += RecalculateLevel;
                break;
            case ButtonColor.Green:
                myColor = "Green";
                redButton.OnPressed += RecalculateLevel;
                blueButton.OnPressed += RecalculateLevel;
                goldButton.OnPressed += RecalculateLevel;
                break;
            case ButtonColor.Blue:
                myColor = "Blue";
                redButton.OnPressed += RecalculateLevel;
                greenButton.OnPressed += RecalculateLevel;
                goldButton.OnPressed += RecalculateLevel;
                break;
            case ButtonColor.Gold:
                myColor = "Gold";
                redButton.OnPressed += RecalculateLevel;
                greenButton.OnPressed += RecalculateLevel;
                blueButton.OnPressed += RecalculateLevel;
                break;
        }
    }

    void RecalculateLevel(int levelDeltaRed, int levelDeltaGreen, int levelDeltaBlue)
    {
        int levelDelta = 0;
        switch (buttonColor)
        {
            case ButtonColor.Red:
                levelDelta = levelDeltaRed;
                break;
            case ButtonColor.Green:
                levelDelta = levelDeltaGreen;
                break;
            case ButtonColor.Blue:
                levelDelta = levelDeltaBlue;
                break;
            case ButtonColor.Gold:
                levelDelta = levelDeltaRed;
                break;
            default:
                break;
        }
        myLevel += levelDelta;

        if(myLevel < 1)
        {
            myLevel = 1;
        }

        else if(myLevel > levelMax)
        {
            myLevel = levelMax;
        }
        mySlider.value = myLevel;
    }

    void SetOwner(PlayerControl owner)
    {
        this.owner = owner;
    }

    // Update is called once per frame
    void Update () {
        if(myColor == "Gold")
        {
            myLevel += 1;// Mathf.RoundToInt(1f * Time.deltaTime);
            if (myLevel > levelMax)
            {
                myLevel = levelMax;
            }
            mySlider.value = myLevel;
        }
		if(PlayerControl.clickCooldown > 0)
        {
            barImage.color = Color.grey;
        }
        else
        {
            barImage.color = fillColor;
        }
	}
}
