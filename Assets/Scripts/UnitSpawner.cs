using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class UnitSpawner : MonoBehaviour
{
    SpawnNode spawnObject;
    public GameObject[] greenUnits;
    public GameObject[] blueUnits;
    public GameObject[] redUnits;

    public int currentMaxLevel = 50;

    public GameObject Reels;


    private void OnEnable()
    {
        
        PlayerControl.OnPress += SpawnUnit;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawnObject = GameObject.Find("Player1_UnitGenerator").GetComponent<SpawnNode>();
    }

    private void OnDisable()
    {
        PlayerControl.OnPress -= SpawnUnit;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Should only run on the server
    void SpawnUnit(string color, int level)
    {
        GameObject myUnit;
        Unit unit;
        float levelNormalized = (float)level/(float)currentMaxLevel;
        //level = (int)(levelNormalized * redUnits.Length);
        switch (color)
        {
            case "Red":
                level = (int)(levelNormalized * redUnits.Length);
                Debug.Log("Red level: " + level);
                if (level > redUnits.Length)
                {
                    level = redUnits.Length;
                }
                if(level < 1)
                {
                    level = 1;
                }
                myUnit = Instantiate(redUnits[level - 1], spawnObject.transform.position, Quaternion.identity) as GameObject;
                myUnit.name = redUnits[level - 1].name;
                unit = myUnit.GetComponent<Unit>();
                unit.teamIndex = Unit.TeamIndex.Team_1;
                unit.targetGoal = spawnObject.targetPosition;
                break;
            case "Green":
                level = (int)(levelNormalized * greenUnits.Length);
                Debug.Log("Green level: " + level);
                if (level > greenUnits.Length)
                {
                    level = greenUnits.Length;
                }
                if (level < 1)
                {
                    level = 1;
                }
                myUnit = Instantiate(greenUnits[level - 1], spawnObject.transform.position, Quaternion.identity) as GameObject;
                myUnit.name = greenUnits[level - 1].name;
                unit = myUnit.GetComponent<Unit>();
                unit.teamIndex = Unit.TeamIndex.Team_1;
                unit.targetGoal = spawnObject.targetPosition;
                break;
            case "Blue":
                level = (int)(levelNormalized * blueUnits.Length);
                Debug.Log("Blue level: " + level);
                if (level > blueUnits.Length)
                {
                    level = blueUnits.Length;
                }
                if (level < 1)
                {
                    level = 1;
                }
                myUnit = Instantiate(blueUnits[level - 1], spawnObject.transform.position, Quaternion.identity) as GameObject;
                myUnit.name = blueUnits[level - 1].name;
                unit = myUnit.GetComponent<Unit>();
                unit.teamIndex = Unit.TeamIndex.Team_1;
                unit.targetGoal = spawnObject.targetPosition;
                break;
            case "Gold":
                Reels.GetComponent<slotManager>().bet = level;
                Reels.GetComponent<slotManager>().button_Click();
                break;
            default:
                Debug.Log("Invalid unit color!");
                break;
        }
    }
}