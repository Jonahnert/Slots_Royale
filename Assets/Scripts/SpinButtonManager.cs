using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpinButtonManager : MonoBehaviour, IPointerClickHandler
{

    public ButtonManager redButton;
    public ButtonManager greenButton;
    public ButtonManager blueButton;
    public Color fillColor;
    public enum ButtonColor { Red, Green, Blue, Gold }
    public ButtonColor buttonColor;
    public PlayerControl owner;
    private string myColor;
    public int levelMax = 10;
    public int myLevel;
    private Image barImage;

    private Slider mySlider;

    public delegate void ButtonPressAction(int value);
    public event ButtonPressAction OnPressed;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerControl.clickCooldown <= 0)
        {
            owner.Press(myColor, myLevel);
            PlayerControl.clickCooldown = 1;
            RecalculateLevel(-myLevel);
            OnPressed(1);
        }
    }

    // Use this for initialization
    void Awake()
    {
        mySlider = GetComponent<Slider>();
        barImage = mySlider.fillRect.GetComponent<Image>();
        myLevel = 1;
        RecalculateLevel(0);
    }

    private void Start()
    {
        /*
        switch (buttonColor)
        {
            case ButtonColor.Red:
                myColor = "Red";
                greenButton.OnPressed += RecalculateLevel;
                blueButton.OnPressed += RecalculateLevel;
                break;
            case ButtonColor.Green:
                myColor = "Green";
                redButton.OnPressed += RecalculateLevel;
                blueButton.OnPressed += RecalculateLevel;
                break;
            case ButtonColor.Gold:
                myColor = "Gold";
                redButton.OnPressed += RecalculateLevel;
                greenButton.OnPressed += RecalculateLevel;
                blueButton.OnPressed += RecalculateLevel;
                break;
        }
        */
    }

    void RecalculateLevel(int levelDelta)
    {
        myLevel += levelDelta;

        if (myLevel < 1)
        {
            myLevel = 1;
        }

        else if (myLevel > levelMax)
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
    void Update()
    {
        myLevel += 1;// Mathf.RoundToInt(1f * Time.deltaTime);
        if (myLevel > levelMax)
        {
            myLevel = levelMax;
        }
        mySlider.value = myLevel;

        if (PlayerControl.clickCooldown > 0)
        {
           barImage.color = Color.grey;
        }
        else
        {
            barImage.color = fillColor;
        }
    }
}


