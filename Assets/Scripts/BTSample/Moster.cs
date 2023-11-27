using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using System;
using Random = UnityEngine.Random;

public class Moster : BTTree
{
    public float moveSpeed;
    public float potrolRange;
    public float radius;
    private Vector3 waypoint;
    private Vector3 gardPos;
    private GameObject attackTarget;
    public bool isFound;
    public Vector3 currentPatrolPos;

    public override void Start()
    {
        base.Start();
        gardPos = transform.position;
        _database.Setdata("Ѳ�ߵ�", GetwayPoint());
        currentPatrolPos = (Vector3)_database.GetData("Ѳ�ߵ�");
    }
    /// <summary>
    /// ������Ϊ��
    /// </summary>
    /// <returns></returns>
    public override BTBaseNode SetUpTree()
    {
        BTSelectNode bt_Select = new BTSelectNode();
        BTSequenceNode SeqPatrol = SeqBTInitNot(FoundPlayer, MoveToPatrolPoint);//��Ҫװ�νڵ�
        BTSequenceNode Seqpursuit = SeqBTInit(FoundPlayer, Pursuit);
        BTSequenceNode SeqBack = SeqBTInitNot(FoundPlayer, MoveToPatrolPoint);//��Ҫװ�νڵ�
        BTSequenceNode SeqAtt = SeqBTInit(AttackRange, Attack);
        bt_Select.AddChild(SeqPatrol, Seqpursuit, SeqBack, SeqPatrol);
        return bt_Select;
    }

    public BTSequenceNode SeqBTInit(Func<bool> actionCondi = null, Func<E_NodeState> action = null)
    {
        BTSequenceNode seq = new BTSequenceNode();

        BTConditionNode bTCondition = new BTConditionNode(actionCondi);

        BTActionNode btActionNode = new BTActionNode(action);

        seq.AddChild(bTCondition, btActionNode);

        return seq;
    }

    public BTSequenceNode SeqBTInitNot(Func<bool> actionCondi = null, Func<E_NodeState> action = null)
    {
        BTSequenceNode seq = new BTSequenceNode();

        BTDecoratorNot bTCondition = new BTDecoratorNot(actionCondi);

        BTActionNode btActionNode = new BTActionNode(action);

        seq.AddChild(bTCondition, btActionNode);

        return seq;
    }

    public E_NodeState Pursuit()
    {
        if (FoundPlayer())
        {
            GameObject attTarget = _database.GetData("��������") as GameObject;
            if (_database.GetData("��������") != null)
            {
                MoveToTarget(attTarget.transform.position);
                return E_NodeState.Running;
            }
        }

        return E_NodeState.Failed;
    }

    bool FoundPlayer()//�ҵ�����
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                _database.Setdata("��������", attackTarget);
                isFound = true;
                return isFound;
            }
        }
        if (_database.GetData("��������") != null)
        {
            _database.ClearData("��������");
        }
        isFound = false;
        return isFound;
    }

    Vector3 GetwayPoint()//���Ѳ�ߵ�
    {
        float RandomX = Random.Range(-potrolRange, potrolRange);
        float RandomZ = Random.Range(-potrolRange, potrolRange);
        Vector3 RandomPoint = new Vector3(gardPos.x + RandomX, transform.position.y, gardPos.z + RandomZ);
        waypoint = RandomPoint;
        _database.Setdata("Ѳ�ߵ�", waypoint);
        return waypoint;
    }

    public E_NodeState MoveToPatrolPoint()
    {
        if (!FoundPlayer())
        {
            if (Vector3.Distance(currentPatrolPos, transform.position) < 2f)
            {
                _database.Setdata("Ѳ�ߵ�", GetwayPoint());
                currentPatrolPos = (Vector3)_database.GetData("Ѳ�ߵ�");
                MoveToTarget(currentPatrolPos);
                Debug.Log("�л�Ѳ�ߵ�");
                return E_NodeState.Running;
            }
            else
            {
                MoveToTarget(currentPatrolPos);
                return E_NodeState.Running;
            }
        }
        return E_NodeState.Failed;
    }

    public void MoveToTarget(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        Debug.Log("�ƶ�����");

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, potrolRange);
    }

    public E_NodeState Attack()
    {
        Debug.Log("�������");
        return E_NodeState.Success;
    }

    bool AttackRange()
    {
        if ((_database.GetData("��������") as GameObject) != null)
        {
            if (Vector3.Distance((_database.GetData("��������") as GameObject).transform.position, transform.position) < 2f)
            {
                return true;
            }
        }

        return false;
    }


}
