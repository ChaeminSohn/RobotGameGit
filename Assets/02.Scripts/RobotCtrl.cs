using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RobotCtrl : MonoBehaviour
{
    RaycastHit hit;
    Position destination;
    public GameObject image_Type;
    public Transform SkillPos;
    public GameObject SkillEffect;
    public Vector3 offset = new Vector3(0, 8.0f, 0);
    private GameObject flame;
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
                state = State.TRACE;
                break;
            case "Move":
                state = State.MOVE;
                agent.destination = pos;
                break;
            case "Skill":
                state = State.SKILL;

                break;
            case "Explode":
                break;
        }
    }

    void UseSkill()
    {
        Debug.Log("skill");
        switch (type)
        {
            case Type.RED:
                Skill_Fire();
                break;
            case Type.GREEN:
                Skill_Wind();
                break;
            case Type.YELLOW:
                Skill_Electric();
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
            image_Type.transform.position = (robotTr.position + offset);
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
                    agent.isStopped = true;
                    anim.SetBool(hashOpen, true);
                    yield return new WaitForSeconds(1.0f);
                    UseSkill();
                    yield return new WaitForSeconds(3.0f);

                    Destroy(flame, .5f);
                    anim.SetBool(hashOpen, false);

                    state = State.WAIT;
                    break;
                case State.MOVE:
                    float MoveDistance = Vector3.Distance(agent.destination, robotTr.position);
                    if (MoveDistance > 1.0f)
                    {
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

            }

        }
    }

    void Skill_Fire()
    {
        flame = Instantiate(SkillEffect, SkillPos.position, SkillPos.rotation);
    }

    void Skill_Wind()
    {

    }
    void Skill_Electric()
    {

    }

}
