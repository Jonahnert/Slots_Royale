using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpawner : MonoBehaviour {

    public float minSize = 1f;
    public float maxSize = 3;

    public float textSpeed = 1f;
    public Transform textPos;
    public GameObject damageTextObject;

    public void SpawnText(string myText)
    {
        GameObject damageText = Instantiate(damageTextObject, textPos.position, damageTextObject.transform.rotation);
        Text instance = damageText.GetComponentInChildren<Text>();
        instance.transform.SetParent(textPos);
        instance.text = myText;
    }

    public void SpawnDamageText(float amount)
    {
        GameObject damageText = Instantiate(damageTextObject, textPos.position, damageTextObject.transform.rotation);
        Text instance = damageText.GetComponentInChildren<Text>();
        damageText.transform.SetParent(textPos);
        instance.text = amount.ToString();
    }
}
