using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    private void FixedUpdate()
    {
        GetComponent<Text>().text = Data.score.toString("000");
    }
}
