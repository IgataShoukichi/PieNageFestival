using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MobController : MonoBehaviour
{
    public GameObject target;
    public GameObject target1;
    public GameObject target2;
    private NavMeshAgent agent;
    Animator animator;

    bool rotate;
    public float rotateMax;
    public float rotateMin;
    float y_axis;
    bool rotate_change;
    [SerializeField] PieDirector pieD;
    float time;

    public GameObject follow_h;
    [SerializeField] public Transform follow_HT;
    public float span;
    int rnd;

    [System.NonSerialized] public TimeController TC;

    public float x_axis;
    public int num;
    bool out_targat;

    public int navSpeed;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        this.animator = GetComponent<Animator>();
        y_axis = 0;
        rotate_change = false;
        rotate = false;
        animator.SetBool("Run", true);
        time = 0;
        out_targat = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ì¸èÍ
        if (TC.countdown <= 70 && TC.countdown >= 0)
        {
            agent.SetDestination(target.transform.position);

            if (rotate)
            {
                if (!rotate_change)
                {
                    y_axis++;
                    if (y_axis > rotateMax)
                    {
                        rotate_change = true;
                    }
                }
                else
                {
                    y_axis--;
                    if (y_axis < rotateMin)
                    {
                        rotate_change = false;
                    }
                }
                Transform myTransform = this.transform;
                Vector3 localAngle = myTransform.localEulerAngles;
                localAngle.y = y_axis;
                localAngle.x = x_axis;
                myTransform.localEulerAngles = localAngle;

                time += Time.deltaTime;
                animator.SetBool("Throw", true);
                if (time >= span)
                {
                    animator.SetTrigger("Throw_tri");
                    Nage();
                    time = 0;
                }
            }
        }

        //ë“Çøéûä‘
        if (TC.countdown < 0 && TC.countdown >= -3)
        {
            animator.SetBool("Run", false);
        }

        //ëﬁèÍ
        if (TC.countdown < -3)
        {
            animator.SetBool("Run", true);
            agent.SetDestination(target1.transform.position);
            this.agent.speed = navSpeed;
            if(num == 1)
            {
                agent.SetDestination(target2.transform.position);
            }

        }

        if (out_targat)
        {
            agent.SetDestination(target1.transform.position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mob1"))
        {
            rotate = true;
        }
        if (other.gameObject.CompareTag("Mob2"))
        {
            out_targat = true;
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
                rnd = Random.Range(25, 50);
                pieD.pieCons1[i].gameObject.GetComponent<PieController>().Shoot(worldDir.normalized * rnd, transform.root.gameObject,false);
                break;
            }
        }

    }

}
