using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Manage the player's amount they turn left
    [SerializeField] private int turnsLeft;
    [SerializeField] private Text turnsLeftText;
    [SerializeField] private bool gameOver;

    // Update is called once per frame
    void Update()
    {
        if (turnsLeft <= 0)
        {
            gameOver = true;
        }
        else
        {
            turnsLeftText.GetComponent<Text>().text = turnsLeft.ToString();
        }
    }

    public int GetTurnsLeft()
    {
        return turnsLeft;
    }
}
