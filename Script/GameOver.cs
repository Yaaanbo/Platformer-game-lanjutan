using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text txScore;
    public Text txHiScore;
    Text txSelamat;
    int highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HS", 0);
        /*if(Data.score > highScore)
        {
            //highScore = Data.score;
            PlayerPrefs.SetInt("HS", highScore);
        }*/
        if(EnemyController.enemyKilled == 3)
        {
            SceneManager.LoadScene("Congratulations");
        }
        txHiScore.text = "Highscore: " + highScore;
        //txScore.text = "Score: " + Data.score;
    }
    public void replay()
    {
        //Data.score = 0;
        EnemyController.enemyKilled = 0;
        SceneManager.LoadScene("Gameplay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
