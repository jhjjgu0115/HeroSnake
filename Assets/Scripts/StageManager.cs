using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<StageManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "StageManager";
                    instance = container.AddComponent<StageManager>();
                }
            }
            return instance;
        }
    }
    
    void Start ()
    {
		//맵 생성.
        //몹 배치
        //아이템 배치
        //플레이어 캐릭터 배치
        //플레이어 파티 초기화
        //키 이벤트 등록
	}
    public Direction currentPartyDirection;
    public List<Unit> playerParty = new List<Unit>();
    public Unit leader;
    public void MoveNext()
    {
        if(leader)
        {
            if (leader.MoveToDirection(currentPartyDirection,leader.movePoint))
            {

            }

        }
    }

    //아이템 배치
    //캐릭터 배치
    
    //캐릭터 사망
    //플레이어 캐릭터 사망
    //패배
    //승리

    //리더 변경
    //
	
	void Update ()
    {
		
	}
}
