using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RobotCtrl : MonoBehaviour
{
    RaycastHit hit;
    Position destination;
    //public GameObject image_Type;
    public GameObject SkillPos;
    public GameObject SkillEffect;
    public Vector3 offset = new Vector3(0, 8.0f, 0);
    private GameObject skillEffect;
    public float drawRange = 10.0f;
    public float moveSpeed;
    public enum State
    {
        WAIT, TRACE, MOVE, OPEN, CLOSED, SKILL
    }

    public enum Type
    {
        RED, GREEN, YELLOW, ALL
    }
    public State state;
    public Type type;
    private Transform robotTr;
    private Transform playerTr;
    Vector3 skillDestination;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isAlive = true;
    public GameObject button;

    private readonly int hashWalk = Animator.StringToHash("Walk_Anim");
    private readonly int hashSkill = Animator.StringToHash("Skill_Anim");
    private readonly int hashOpen = Animator.StringToHash("Open_Anim");


    void Start()
    {
        robotTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = true;
        state = State.WAIT;
        /*switch (this.tag)
        {
            case "ROBOT_RED":
                type = Type.RED;
                break;
            case "ROBOT_GREEN":
                type = Type.GREEN;
                break;
            case "ROBOT_YELLOW":
                type = Type.YELLOW;
                break;
        }*/


        StartCoroutine(RobotAction());
    }

    public void Command(System.String cmd, Vector3 pos)
    {
        if (cmd != "Skill" && state == State.SKILL && button != null)
        {
            button.GetComponent<ChargeButtonCtrl>()?.StopCharge();
        }
        switch (cmd)
        {
            case "Wait":
                state = State.WAIT;
                break;
            case "Follow":
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                robotTr.LookAt(pos);
                state = State.TRACE;
                agent.isStopped = false;
                break;
            case "Move":
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                robotTr.LookAt(pos);
                state = State.MOVE;
                agent.destination = pos;
                break;
            case "Skill":
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                SkillPos.transform.LookAt(pos);
                skillDestination = pos;
                robotTr.LookAt(pos);
                state = State.SKILL;
                break;
            case "Explode":
                break;
        }
    }

    void UseSkill()
    {
        switch (type)
        {
            case Type.RED:
                ShowSkill(); break;
            case Type.GREEN:
                ShowSkill(); break;
            case Type.YELLOW:
                ShowSkill(); break;
            case Type.ALL:
                break;
        }
        if (Physics.Raycast(robotTr.position, -robotTr.up, out hit, 10.0f) && hit.transform.CompareTag("BUTTON"))
        {
            button = hit.transform.gameObject;
            button.GetComponent<ChargeButtonCtrl>()?.OnCharge(type);
        }

    }

    private void OnParticleSystemStopped()
    {

    }
    IEnumerator RobotAction()
    {
        while (isAlive)
        {
            //image_Type.transform.position = (robotTr.position + offset);
            yield return new WaitForSeconds(0.3f);

            switch (state)
            {
                case State.WAIT:
                    agent.isStopped = true;
                    anim.SetBool(hashWalk, false);
                    agent.speed = 0.0f;
                    break;
                case State.TRACE:
                    float TraceDistance = Vector3.Distance(robotTr.position, playerTr.position);
                    if (TraceDistance > 10.0f)
                    {
                        agent.SetDestination(playerTr.position);
                        anim.SetBool(hashWalk, true);
                        agent.speed = (float)playerTr.GetComponent<PlayerCtrl>()?.moveSpeed * 2;
                        agent.isStopped = false;
                    }
                    else
                    {
                        anim.SetBool(hashWalk, false);
                        agent.isStopped = true;
                        agent.velocity = Vector3.zero;
                    }
                    break;
                case State.SKILL:
                    anim.SetBool(hashOpen, true);
                    anim.SetBool(hashWalk, false);
                    yield return new WaitForSeconds(2.0f);
                    UseSkill();
                    yield return new WaitForSeconds(3.0f);

                    Destroy(skillEffect, .5f);
                    anim.SetBool(hashOpen, false);

                    state = State.WAIT;
                    break;
                case State.MOVE:
                    float MoveDistance = Vector3.Distance(agent.destination, robotTr.position);
                    if (MoveDistance > 20.0f)
                    {
                        anim.SetBool(hashWalk, true);
                        agent.speed = moveSpeed * 3;
                        agent.isStopped = false;
                    }
                    else if (MoveDistance > 10.0f)
                    {
                        anim.SetBool(hashWalk, true);
                        agent.speed = moveSpeed * 2;
                        agent.isStopped = false;
                    }
                    else if(MoveDistance > 2.0f)
                    {
                        anim.SetBool(hashWalk, true);
                        agent.speed = moveSpeed;
                        agent.isStopped = false;
                    }
                    else
                    {
                        anim.SetBool(hashWalk, false);
                        agent.isStopped = true;
                        agent.velocity = Vector3.zero;
                        agent.velocity = Vector3.zero;
                    }
                    break;

            }
            Debug.DrawRay(SkillPos.transform.position, (robotTr.forward - robotTr.up) * drawRange, Color.green);

        }
    }

    void ShowSkill()
    {
        skillEffect = Instantiate(SkillEffect, SkillPos.transform.position, robotTr.rotation * Quaternion.Euler(new Vector3(-30,0,0)));
    }




}
