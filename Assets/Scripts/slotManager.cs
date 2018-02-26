using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

public class slotManager : MonoBehaviour {
	// DECLARING TOTAL, BET AND CREDITS
	public static long credits = 100;
	public static int greenTotal = 0;
    public static int redTotal = 0;
    public static int blueTotal = 0;
    public int bet = 5;

	// DECLARING EACH ITEM
	public static int p1;
	public static int p2;
	public static int p3;

    public GameObject reel1;
    public GameObject reel2;
    public GameObject reel3;

    private Animator reel1Anim;
    private Animator reel2Anim;
    private Animator reel3Anim;

    private Animator myAnimator;

    private int baseAward = 5;
    private int doubleMultiplier = 3;
    private int trippleMultiplier = 5;

    public GameObject spinButton;
    public ButtonManager spinManager;
    // Use this for initialization
    void Start () {
        reel1Anim = reel1.GetComponent<Animator>();
        reel2Anim = reel2.GetComponent<Animator>(); 
        reel3Anim = reel3.GetComponent<Animator>();
        spinManager = spinButton.GetComponent<ButtonManager>();
        myAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	// GENERATES RANDOM NUMBERS
	public static class IntUtil
	{
		private static System.Random random;

		private static void Init()
		{
			if (random == null) random = new System.Random();
		}
		public static int Random(int min, int max)
		{
			Init();
			return random.Next(min, max);
		}
	}

	//played when spin is clicked
	public void button_Click()
	{
        Debug.Log("We Spun!");

        StartCoroutine("Spinning");

		//TODO update win text and credits
		//label3.Text = "Win: " + total.ToString();
		//label1.Text = "Credits: " + credits.ToString();
		
	}
    IEnumerator Spinning()
    {
        reel1Anim.SetTrigger("spin");
        yield return new WaitForSeconds(.1f);
        reel2Anim.SetTrigger("spin");
        yield return new WaitForSeconds(.1f);
        reel3Anim.SetTrigger("spin");
        yield return new WaitForSeconds(1f);

        CalculateResults();

    }
    public void CalculateResults()
    {

        //credits = credits - bet;
        //TODO update Credit text
        //label1.Text = "Credits: " + credits.ToString();

        //Randomize our three reels
        for (var i = 0; i < 10; i++)
        {
            p1 = IntUtil.Random(1, 4);
            p2 = IntUtil.Random(1, 4);
            p3 = IntUtil.Random(1, 4);
        }

        //TODO play animation for each reel depending on result
        switch (p1)
        {
            case 1:
                //Debug.Log("We Chose Red");
                reel1Anim.SetTrigger("makeRed");
                break;
            case 2:
                //Debug.Log("We Chose Greed");
                reel1Anim.SetTrigger("makeGreen");
                break;
            case 3:
                //Debug.Log("We Chose Blue");
                reel1Anim.SetTrigger("makeBlue");
                break;
            default:
                Debug.Log("Invalid reel outcome");
                break;
        }
        switch (p2)
        {
            case 1:
                //Debug.Log("We Chose Red");
                reel2Anim.SetTrigger("makeRed");
                break;
            case 2:
                //Debug.Log("We Chose Greed");
                reel2Anim.SetTrigger("makeGreen");
                break;
            case 3:
                //Debug.Log("We Chose Blue");
                reel2Anim.SetTrigger("makeBlue");
                break;
            default:
                Debug.Log("Invalid reel outcome");
                break;
        }
        switch (p3)
        {
            case 1:
                //Debug.Log("We Chose Red");
                reel3Anim.SetTrigger("makeRed");
                break;
            case 2:
                //Debug.Log("We Chose Greed");
                reel3Anim.SetTrigger("makeGreen");
                break;
            case 3:
                //Debug.Log("We Chose Blue");
                reel3Anim.SetTrigger("makeBlue");
                break;
            default:
                Debug.Log("Invalid reel outcome");
                break;
        }

        /*
		if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
		pictureBox1.Image = Image.FromFile(p1.ToString() + ".png");

		if (pictureBox2.Image != null) pictureBox2.Image.Dispose();
		pictureBox2.Image = Image.FromFile(p2.ToString() + ".png");

		if (pictureBox3.Image != null) pictureBox3.Image.Dispose();
		pictureBox3.Image = Image.FromFile(p3.ToString() + ".png");
		*/

        //resets total before calculating
        redTotal = 0;
        greenTotal = 0;
        blueTotal = 0;

        int totalReel1 = baseAward;
        int totalReel2 = baseAward;
        int totalReel3 = baseAward;
        // GET RESULTS FROM PAYTABLE
        // CHECK IF 1, 2 OR 3 OCCURANCES
        bool reel12Anim = false;
        bool reel23Anim = false;
        bool reel123Anim = false;
        if (p1 == p2 && p2 == p3)
        {
            totalReel1 = totalReel1 * trippleMultiplier;
            myAnimator.SetTrigger("123win");
            addValues(1, totalReel1);
        }
        else if (p1 == p2)
        {
            totalReel1 = totalReel1 * doubleMultiplier;
            myAnimator.SetTrigger("12win");
            addValues(1, totalReel1);
            addValues(3, totalReel3);
        }
        else if(p2 == p3)
        {
            totalReel2 = totalReel2 * doubleMultiplier;
            myAnimator.SetTrigger("23win");
            addValues(2, totalReel2);
            addValues(1, totalReel1);
        }
        else
        {
            addValues(1, totalReel1);
            addValues(2, totalReel2);
            addValues(3, totalReel3);
        }



        /*
        if (p1 == 3) blueTotal = blueTotal + 5;

        if (p1 == 2 & p2 == 2) greenTotal = greenTotal + 10;
        if (p1 == 3 & p2 == 3) blueTotal = blueTotal + 10;

        if (p1 == 1 & p2 == 1 & p3 == 1) redTotal = redTotal + 20;
        if (p1 == 2 & p2 == 2 & p3 == 2) greenTotal = greenTotal + 30;
        if (p1 == 3 & p2 == 3 & p3 == 3) blueTotal = blueTotal + 50;
        */
        redTotal = redTotal * (bet / 100);
        greenTotal = greenTotal * (bet / 100);
        blueTotal = blueTotal * (bet / 100);
        spinManager.spinResultRed = redTotal;
        spinManager.spinResultGreen = greenTotal;
        spinManager.spinResultBlue = blueTotal;
        Debug.Log("RED = " + redTotal + "  ||  " + "GREEN = " + greenTotal + "  ||  " + "BLUE = " + blueTotal);
        spinManager.SpinPressed();
        //credits = credits + total;
    }
    public void addValues(int reel, int value)
    {
        if (reel == 1)
        {
            switch (p1)
            {
                case 1:
                    //Debug.Log("We Chose Red");
                    redTotal += value;
                    break;
                case 2:
                    //Debug.Log("We Chose Greed");
                    greenTotal += value;
                    break;
                case 3:
                    //Debug.Log("We Chose Blue");
                    blueTotal += value;
                    break;
                default:
                    Debug.Log("Invalid reel outcome");
                    break;
            }
        }
        else if(reel == 2)
        {
            switch (p2)
            {
                case 1:
                    //Debug.Log("We Chose Red");
                    redTotal += value;
                    break;
                case 2:
                    //Debug.Log("We Chose Greed");
                    greenTotal += value;
                    break;
                case 3:
                    //Debug.Log("We Chose Blue");
                    blueTotal += value;
                    break;
                default:
                    Debug.Log("Invalid reel outcome");
                    break;
            }
        }
        else if (reel == 3)
        {
            switch (p3)
            {
                case 1:
                    //Debug.Log("We Chose Red");
                    redTotal += value;
                    break;
                case 2:
                    //Debug.Log("We Chose Greed");
                    greenTotal += value;
                    break;
                case 3:
                    //Debug.Log("We Chose Blue");
                    blueTotal += value;
                    break;
                default:
                    Debug.Log("Invalid reel outcome");
                    break;
            }
        }
        
    }
}


