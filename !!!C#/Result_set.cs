using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class Result_set : MonoBehaviour
{
    public GameObject target;//定位置のオブジェクト
    private NavMeshAgent agent;

    Animator animator;

    [System.NonSerialized] public Result_stop RST;

    public int ani = 1;

    public bool Rotation = false;

    [SerializeField] public GameObject canvas1;
    public Result_UI RU;

    public int num;
    private float time;

    public int judge;

    static int rnd;

    public bool wait;

    public int rse_rank;

    [SerializeField] public GameObject effect;

    void Start()
    {
        
        if (num==0)
        {
            RU = canvas1.gameObject.GetComponent<Result_UI>();
            RU.RSE[num] = this;
        }
        else if (num == 1)
        {
            RU = canvas1.gameObject.GetComponent<Result_UI>();
            RU.RSE[num] = this;
        }
        else if(num == 2)
        {
            RU = canvas1.gameObject.GetComponent<Result_UI>();
            RU.RSE[num] = this;
        }
        else if (num == 3)
        {
            RU = canvas1.gameObject.GetComponent<Result_UI>();
            RU.RSE[num] = this;
        }
        

        time = 0;
        Rotation = false;
        ani = 1;
        this.animator = GetComponent<Animator>();
        animator.SetFloat("Walk_F", 1);
        animator.SetFloat("Walk_B", 0);

        //リザルト時の定位置に移動
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);

        judge = 3;

        rnd = Random.Range(1, 3); //1～2
        this.effect.gameObject.SetActive(false);

    }

    void Update()
    {
        if(ani == 0)
        {
            animator.SetFloat("Walk_F", 0);
            animator.SetFloat("Walk_B", 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if(Rotation)
        {
            RU.Switch_can = true;
            time += Time.deltaTime;
            RU.time = time;
            if(time >= 1.5f)
            {
                if(num == 0 || num == 1)
                {
                    animator.SetInteger("Rotation", 1);
                }
            }
            if (time >= 4.5f)
            {
                if (num == 0 || num == 1)
                {
                    animator.SetInteger("Rotation", 0);
                }
                if (num == 2 || num == 3)
                {
                    animator.SetInteger("Rotation", 1);
                }

            }
            if(time >= 7.5f)
            {
                if (num == 2 || num == 3)
                {
                    animator.SetInteger("Rotation", 0);
                }
            }

            if (wait)
            {
                if (num == 0)
                    animator.SetBool("Result_wait", true);

                if (num == 2)
                    animator.SetBool("Result_wait", true);

                if (num == 1)
                    animator.SetBool("Result_wait", true);

                if (num == 3)
                    animator.SetBool("Result_wait", true);

            }
            else
            {
                if (num == 0)
                    animator.SetBool("Result_wait", false);

                if (num == 2)
                    animator.SetBool("Result_wait", false);

                if (num == 1)
                    animator.SetBool("Result_wait", false);

                if (num == 3)
                    animator.SetBool("Result_wait", false);

            }

            if(rse_rank == RU.rank_price)
            {
                if(RU.rank_price == 4)
                {
                    animator.SetBool("Rank4", true);
                }
                else if(RU.rank_price == 3)
                {
                    animator.SetBool("Rank3", true);
                }
                else if(RU.rank_price == 2)
                {
                    animator.SetBool("Rank2", true);
                }
                else if(RU.rank_price == 1)
                {
                    if(num == 0)
                    {
                        animator.SetBool("Win1", true);
                        this.effect.gameObject.SetActive(true);
                    }
                    else if(num == 1)
                    {
                        animator.SetBool("Win2", true);
                        this.effect.gameObject.SetActive(true);
                    }
                    else if (num == 2)
                    {
                        animator.SetBool("Win3", true);
                        this.effect.gameObject.SetActive(true);
                    }
                    else if (num == 3)
                    {
                        animator.SetBool("Win4", true);
                        this.effect.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
