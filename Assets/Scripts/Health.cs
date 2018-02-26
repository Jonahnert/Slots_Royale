using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public float dodgeChance;

    public bool immunity = false;
    public int regenAmount = 1;

    public Transform healthBarPosition;
    protected TextSpawner textSpawner;

    public Scrollbar lifebar;
    private Graphic lifeBarInstance;
    public Gradient healthBarColors;
    private float fillAmount;

    public float lifebarVisibilityCounter = 3f;

    public enum DamageElement { Physical, Fire, Ice, Water, Earth };

	// Use this for initialization
	protected virtual void Awake () {
        textSpawner = GetComponent<TextSpawner>();
        lifeBarInstance = lifebar.targetGraphic;
        currentHealth = maxHealth;    
	}

    protected virtual void Start()
    {
        lifebar.gameObject.SetActive(false);
    }


    protected virtual IEnumerator Regen()
    {
        while(true)
        {
            Heal(regenAmount + Mathf.CeilToInt(regenAmount));
            yield return new WaitForSeconds(1f);
        }
    }
    private void Levelup()
    {
        maxHealth = Mathf.CeilToInt(maxHealth * 1.5f);
        currentHealth = maxHealth;
        SetImmunity(1f);
    }

    public virtual void ReceiveDamage(int damage)
    {
        if(!immunity)
        {
            currentHealth -= damage;

            if (currentHealth <= 0.0f)
            {
                Death();
            }
            else
            {
                ShowText(damage);
                UpdateLifebar();
            }
        }
    }

    public void ShowText(int damage)
    {
        if (textSpawner != null)
            textSpawner.SpawnDamageText(damage);
    }

    public void UpdateLifebar()
    {
        if (lifebar != null)
        {
            lifebarVisibilityCounter = 3;
            lifebar.gameObject.SetActive(true);
            fillAmount = (float)currentHealth / maxHealth;
            lifebar.size = fillAmount;
            lifeBarInstance.color = healthBarColors.Evaluate(fillAmount);
        }
    }

    //This will take into account armor, damage resistance, attack type and resistance type.  Need to add calculation for elemental damage modifiers and armor.
    public virtual int CalculateRecievedDamage(int damage)
    {
        return damage;
    }

    IEnumerator SetImmunity(float timer)
    {
        Debug.Log("Becoming immune!");
        immunity = true;
        yield return new WaitForSeconds(timer);
        immunity = false;
        Debug.Log("Disabling immunity!");
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;           
        }

        if (lifebar != null)
        {
            lifebar.size = (float)currentHealth / maxHealth;
            lifeBarInstance.color = healthBarColors.Evaluate(fillAmount);
        }
    }

    public virtual void Death()
    {
        if(GetComponent<BaseManager>())
        {
            GetComponent<BaseManager>().LoadGameOverScreen();
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        if(lifebarVisibilityCounter <= 0 && lifebar.gameObject.activeSelf == true)
        {
            lifebarVisibilityCounter = 0;
            lifebar.gameObject.SetActive(false);
        }

        else
        {
            lifebarVisibilityCounter -= Time.deltaTime;
        }
    }
}
