using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GameManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "GameManager";
                    instance = container.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public float GameSpeed=3;
    
    void Start ()
    {
        KeyBinder.Instance.Up += SetDirection;
        KeyBinder.Instance.Down += SetDirection;
        KeyBinder.Instance.Right += SetDirection;
        KeyBinder.Instance.Left += SetDirection;
        TileMapManager.Instance.tilePrefab = Resources.Load<Tile>("Prefabs/Tile");
        TileMapManager.Instance.offset = new Vector2(0.1f, 0.1f);
        TileMapManager.Instance.Initialize(100, 100);
        playerUnitList.Add(new GameObject().AddComponent<Unit>());
        playerUnitList.Add(new GameObject().AddComponent<Unit>());
        playerUnitList.Add(new GameObject().AddComponent<Unit>());
        playerUnitList[0].currentCoordinate = new Coordinate(3, 3);
        playerUnitList[1].currentCoordinate = new Coordinate(1, 2);
        playerUnitList[2].currentCoordinate = new Coordinate(2, 1);
        SetUnit(playerUnitList[0].currentCoordinate, playerUnitList[0]);
        SetUnit(playerUnitList[1].currentCoordinate, playerUnitList[1]);
        SetUnit(playerUnitList[2].currentCoordinate, playerUnitList[2]);
        playerUnitList[0].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tanker");
        playerUnitList[1].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Dealer");
        playerUnitList[2].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Healer");
        playerUnitList[0].GetComponent<SpriteRenderer>().sortingOrder = 10;
        playerUnitList[1].GetComponent<SpriteRenderer>().sortingOrder = 10;
        playerUnitList[2].GetComponent<SpriteRenderer>().sortingOrder = 10;
        playerUnitList[0].name = "Tanker";
        playerUnitList[1].name = "Dealer";
        playerUnitList[2].name = "Healer";
        playerUnitList[0].movePoint = 1;
        playerUnitList[1].movePoint = 1;
        playerUnitList[2].movePoint = 1;
        StartCoroutine(GameTick());


        
        //맵 생성.
        //몹 배치
        //아이템 배치
        //플레이어 캐릭터 배치
        //플레이어 파티 초기화
        //키 이벤트 등록
    }
    void SetDirection(Direction direction)
    {
        inputDirection = direction;
    }
    void SetUnit(Coordinate c,Unit u)
    {
        u.MoveToPosition(c);
    }
    Direction inputDirection;
    public List<Unit> playerUnitList = new List<Unit>();
    public Unit leader;
    public void Move()
    {
        if(leader==null)
        {
            leader = playerUnitList[0];
        }
        Unit frontUnit=leader;
        
        foreach(Unit unit in playerUnitList)
        {
            if(unit==leader)
            {
                Debug.Log(unit.name);
                unit.MoveToDirection(inputDirection, unit.movePoint);
                frontUnit = unit;
            }
            else
            {
                Debug.Log(unit.name);
                unit.MoveToPosition(frontUnit.traceList[0]);
                frontUnit = unit;
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
    
    IEnumerator GameTick()
    {
        while(true)
        {
            Turn();
            yield return new WaitForSeconds(GameSpeed);
        }
    }
    void Turn()
    {
        Move();
        //이동 경로를 트레일러처럼 만들어서 저장.
        //뒤 파티원은 그 자취를 읽으면 이동
    }
}
