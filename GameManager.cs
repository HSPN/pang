using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager>
{
    public Sprite[] pangImages;
    [SerializeField] Text _score;
    public int score
    {
        get
        {
            return int.Parse(_score.text);
        }
        set
        {
            _score.text = "" + value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Tile.UpdateRoutine();
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
