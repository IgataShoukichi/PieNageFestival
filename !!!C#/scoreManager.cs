using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scoreManager : MonoBehaviour
{
    public int player1;
    public int player2;
    public int player3;
    public int player4;

    [System.NonSerialized] public DecalController DC;

    [SerializeField] public GameObject result_R;
    private Result_UI RU;

    public GameObject[] red_t;
    public GameObject[] blue_t;
    public GameObject[] yellow_t;
    public GameObject[] green_t;
    private void Awake()
    {
        red_t = GameObject.FindGameObjectsWithTag("Red");
        blue_t = GameObject.FindGameObjectsWithTag("Blue");
        yellow_t = GameObject.FindGameObjectsWithTag("Yellow");
        green_t = GameObject.FindGameObjectsWithTag("Green");
    }

    // Start is called before the first frame update
    void Start()
    {
        RU = result_R.gameObject.GetComponent<Result_UI>();
        RU.SCM = this;

        player1 = 0;
        player2 = 0;
        player3 = 0;
        player4 = 0;
    }


    // Update is called once per frame
    void Update()
    {
        RU.score[0] = player1;
        RU.score[1] = player2;
        RU.score[2] = player3;
        RU.score[3] = player4;
    }
}
