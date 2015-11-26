using UnityEngine;
using System.Collections;

public class LevelingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    [SerializeField]
    float multiplierPerLevel;

    const int maxLevel = 10;
    int currentLevel = 1;
    float currentXp = 0;
    [SerializeField]
    float xpToNext = 100;

    void levelUp()
    {
        if (currentLevel < maxLevel)
        {
            currentXp = 0;
            currentLevel++;
            xpToNext *= multiplierPerLevel;
        }
    }
    public void AddXp(float Amount)
    {
        currentXp += Amount;
    }
    void FixedUpdate () {
        if (currentXp >= xpToNext)
        {
            levelUp();
        }
	}
}
