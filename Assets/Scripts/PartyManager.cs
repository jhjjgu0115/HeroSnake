using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PartyManager : MonoBehaviour
{
    static PartyManager instance;
    public static PartyManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<PartyManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "PartyManager";
                    instance = container.AddComponent<PartyManager>();
                }
            }
            return instance;
        }
    }
    public Unit leader;
    public List<Unit> memberList = new List<Unit>();//리더가 0번이면
    [SerializeField]
    List<TraceInfo> positionTrace = new List<TraceInfo>();

    public void Move(E_Direction direction)
    {
        leader.direction = direction;

        Coordinate nextPosition;
        switch(direction)
        {
            case E_Direction.Up:
                nextPosition = new Coordinate(0, 1);
                break;
            case E_Direction.Left:
                nextPosition = new Coordinate(-1, 0);
                break;
            case E_Direction.Down:
                nextPosition = new Coordinate(0, -1);
                break;
            case E_Direction.Right:
                nextPosition = new Coordinate(1, 0);
                break;
            default:
                nextPosition = new Coordinate(0, 0);
                break;
        }
        Coordinate resultPosition = leader.coordinate;
        TraceInfo traceInfo = new TraceInfo(direction,leader.coordinate);
        for(int point =1; point <=leader.movePoint;point++)
        {
            Tile t = TileMapManager.GetTile(nextPosition * point + leader.coordinate);
            if(t!=null)
            {
                //이동 가능한지 확인구간
                if(t.unit==null)
                {
                    traceInfo = new TraceInfo(direction, resultPosition);
                    resultPosition = nextPosition * point + leader.coordinate;
                    AddTrace(traceInfo);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        leader.MoveToPosition(resultPosition);

        for (int index=0; index < memberList.Count; index++)
        {
            if(positionTrace.Count-1>=index)
            {
                memberList[index].MoveToPosition(positionTrace[index].coordinate);
                memberList[index].direction = positionTrace[index].direction;
            }
            else
            {
                break;
            }
        }
        //문제 발생 가능함. 리더의 밀쳐지기 혹은 급작스런 이동시.
    }
    public void PartyAct()
    {
        leader.Act(E_ActionList.MainSkill);
        foreach(Unit member in memberList)
        {
            member.Act(E_ActionList.SubSkill);
        }
    }
    
}
public partial class PartyManager : MonoBehaviour
{
    [System.Serializable]
    struct TraceInfo
    {
        public E_Direction direction;
        public Coordinate coordinate;
        public TraceInfo(E_Direction dir, Coordinate coord)
        {
            direction = dir;
            coordinate = coord;
        }
    }
    void AddTrace(TraceInfo position)
    {
        positionTrace.Insert(0, position);
        if (positionTrace.Count > 50)
        {
            positionTrace.RemoveAt(50);
        }
    }
}
