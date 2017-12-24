using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PartyManager : MonoBehaviour
{
    public Unit leader;
    public List<Unit> memberList = new List<Unit>();//리더가 0번이면
    List<TraceInfo> positionTrace = new List<TraceInfo>();

    public void Move(Direction direction)
    {
        /*
         * 리더 이동
         * 흔적 추가
         * 아군 따라가기
         */
        leader.direction = direction;

        Coordinate nextPosition;
        switch(direction)
        {
            case Direction.Up:
                nextPosition = new Coordinate(0, 1);
                break;
            case Direction.Left:
                nextPosition = new Coordinate(-1, 0);
                break;
            case Direction.Down:
                nextPosition = new Coordinate(0, -1);
                break;
            case Direction.Right:
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
                if(t.unit!=null)
                {
                    traceInfo = new TraceInfo(direction, leader.coordinate);
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

        for(int index=0; index < memberList.Count; index++)
        {
            memberList[index].MoveToPosition(positionTrace[index].coordinate);
            memberList[index].direction = positionTrace[index].direction;
        }
        //문제 발생 가능함. 리더의 밀쳐지기 혹은 급작스런 이동시.
    }
    public void Attack()
    {
        leader.MainAttack();
        foreach(Unit member in memberList)
        {
            member.SubAttack();
        }
    }
    
}
public partial class PartyManager : MonoBehaviour
{
    struct TraceInfo
    {
        public Direction direction;
        public Coordinate coordinate;
        public TraceInfo(Direction dir, Coordinate coord)
        {
            direction = dir;
            coordinate = coord;
        }
    }
    void AddTrace(TraceInfo position)
    {
        positionTrace.Insert(0, position);
        if (positionTrace.Count > 10)
        {
            positionTrace.RemoveAt(10);
        }
    }
}
