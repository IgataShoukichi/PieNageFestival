using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class Title_player : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;

    [SerializeField] PieDirector_Title pieD;

    public int num;
    Animator animator;
    float time;
    float time1;
    float lean;
    int rnd;
    public GameObject follow_h;
    [SerializeField] public Transform follow_HT;

    void Start()
    {
        if(num == 0 || num == 1)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        animator = GetComponent<Animator>();
        lean = 0;

    }

    void Update()
    {
        //1P
        if(num == 0 || num == 1)
        {
            animator.SetFloat("Walk_F", 1);
            animator.SetFloat("Walk_B", 0);
            if (target)
            {
                agent.destination = target.transform.position;
            }
            //2P
            if(num == 1)
            {
                animator.SetBool("BigPie", true);
            }
        }

        //3P
        if (num == 2)
        {
            time += Time.deltaTime;
            animator.SetBool("Throw", true);
            if(time >= 2)
            {
                animator.SetTrigger("Throw_tri");
                Nage();
                time = 0;
            }
        }

        //4P
        if(num == 3)
        {
            time1 += Time.deltaTime;
            if (time1 >= 1f)
            {
                if(lean == 0)
                {
                    rnd = Random.Range(1, 3);
                }
                else if(lean == 1)
                {
                    rnd = Random.Range(0, 2);
                    if(rnd == 1)
                    {
                        rnd = 2;
                    }
                }
                else if(lean == 2)
                {
                    rnd = Random.Range(0, 2);
                }
                time1 = 0;
            }

            if (rnd == 0)
            {
                animator.SetBool("Lean_R", true);
                animator.SetBool("Lean_L", false);
                lean = 0;
            }
            else if (rnd == 1)
            {
                animator.SetBool("Lean_R", false);
                animator.SetBool("Lean_L", true);
                lean = 1;
            }
            else if (rnd == 2)
            {
                animator.SetBool("Lean_R", false);
                animator.SetBool("Lean_R", false);
                lean = 2;
            }

        }

        void Nage()
        {
            
            for (int i = 0; i < pieD.pieCons1.Length; i++)
            {
                if (!pieD.pieCons1[i].gameObject.activeSelf)
                {
                    pieD.pieCons1[i].gameObject.transform.position = follow_HT.position;
                    pieD.pieCons1[i].gameObject.SetActive(true);
                    Ray ray = new Ray(follow_h.transform.position, follow_h.transform.forward);
                    Vector3 worldDir = ray.direction;
                    pieD.pieCons1[i].gameObject.GetComponent<PieController_Title>().Shoot(worldDir.normalized * 25f, transform.root.gameObject, false);
                    break;
                }
            }
            
        }

    }
}