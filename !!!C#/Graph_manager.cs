using UnityEngine;
using UnityEngine.UI;

public class Graph_manager : MonoBehaviour
{
    [System.NonSerialized] public Result_UI RU;

    //現在
    public float current;

    //全体の高さ
    int random_max;
    int random_min;

    //ランダムの数値の範囲
    public int scope_max = 50;
    public int scope_min = 40;

    bool change = false;

    public int gm_rank;
    public int RU_num;

    bool stop;

    [SerializeField] GameObject myGraph;

    void Start()
    {
        random_max = Random.Range(scope_min, scope_max);
        random_min = Random.Range(40, scope_min);
        stop = false;
    }

    void FixedUpdate()
    {
        Graph(current, scope_max);

        //キレイ度表示
        if (gm_rank == RU.rank_price && !stop)
        {
            stop = true;
        }
        else if (gm_rank != RU.rank_price && !stop)
        {
            if (!change)
            {
                current++;
                if (current >= random_max)
                {
                    change = true;
                }
            }
            else
            {
                current--;
                if (current <= random_min)
                {
                    random_max = Random.Range(scope_min, scope_max);
                    random_min = Random.Range(40, scope_min);
                    change = false;
                }
            }
        }
    }

    public void Graph(float current, float max)
    {
        myGraph.GetComponent<Image>().fillAmount = current / max;
    }
}
