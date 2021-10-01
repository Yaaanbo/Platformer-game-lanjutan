using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class autoReplay : MonoBehaviour
{
    float timer = 0;
    public Text info;
    // Start is called before the first frame update
    void Start()
    {
        if(EnemyController.enemyKilled == 3)
        {
            info.text = "Congratulations \n You Win";
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5)
        {
            //Data.score = 0;
            EnemyController.enemyKilled = 0;
            SceneManager.LoadScene("Gameplay");
        }
    }
}
