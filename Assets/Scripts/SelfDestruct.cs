using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour {

    public bool fade = false;
    Color fadeColor;
    Renderer rend;
    Text text;

    float startTime;
    public float destructTimer = 1f;

    public IEnumerator Destruct()
    {
        yield return new WaitForSeconds(destructTimer);
        Destroy(gameObject);
    }
	// Use this for initialization
	void Start () {
        if (fade)
        {
            if(gameObject.GetComponent<Text>() != null)
            {
                text = gameObject.GetComponent<Text>();
                fadeColor = text.color;
            }

            else if (gameObject.GetComponent<Renderer>() != null)
            {
                rend = gameObject.GetComponent<Renderer>();
            }
        }
        StartCoroutine("Destruct");
        startTime = Time.time;
	}

    // Update is called once per frame
    void Update () {
        if(fade)
        {
            if(text != null)
            {
                fadeColor.a = Mathf.Lerp(1, 0, ((Time.time - startTime) / destructTimer));
                text.color = fadeColor;
            }

            else if (rend != null)
            {
                rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, Mathf.Lerp(1, 0, ((Time.time - startTime) / destructTimer)));
            }
        }
        
	}
}
