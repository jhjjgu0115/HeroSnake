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
        playerUnitList[0].coordinate = new Coordinate(3, 3);
        playerUnitList[1].coordinate = new Coordinate(1, 2);
        playerUnitList[2].coordinate = new Coordinate(2, 1);
        SetUnit(playerUnitList[0].coordinate, playerUnitList[0]);
        SetUnit(playerUnitList[1].coordinate, playerUnitList[1]);
        SetUnit(playerUnitList[2].coordinate, playerUnitList[2]);
        playerUnitList[0].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tanker");
        playerUnitList[1].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Dealer");
        playerUnitList[2].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Healer");
        playerUnitList[0].GetComponent<SpriteRenderer>().sortingOrder = 10;
        playerUnitList[1].GetComponent<SpriteRenderer>().sortingOrder = 10;
        playerUnitList[2].GetComponent<SpriteRenderer>().sortingOrder = 10;
        playerUnitList[0].name = "Tanker";
        playerUnitList[1].name = "Dealer";
        playerUnitList[2].name = "Healer";
        playerUnitList[0].movePoint = 3;
        playerUnitList[1].movePoint = 1;
        playerUnitList[2].movePoint = 1;
        StartCoroutine(GameTick());
        leader = playerUnitList[0];
        leader.mainAttackInfoList.Add(new Unit.AttackInfo());
        leader.mainAttackInfoList[0].coordList.Add(new Coordinate(1, 0));
        leader.mainAttackInfoList[0].color = new Color32(255, 0, 0, 255);
        leader.mainAttackInfoList[0].damage = 5;
        leader.mainAttackInfoList.Add(new Unit.AttackInfo());
        leader.mainAttackInfoList[1].coordList.Add(new Coordinate(2, 1));
        leader.mainAttackInfoList[1].coordList.Add(new Coordinate(2, -1));
        leader.mainAttackInfoList[1].color = new Color32(0, 0, 255, 255);
        leader.mainAttackInfoList[1].damage = 3;
        leader.mainAttackInfoList.Add(new Unit.AttackInfo());
        leader.mainAttackInfoList[2].coordList.Add(new Coordinate(3, 2));
        leader.mainAttackInfoList[2].coordList.Add(new Coordinate(3, -2));
        leader.mainAttackInfoList[2].color = new Color32(0, 255, 0, 255);
        leader.mainAttackInfoList[2].damage = 1;

        //맵 생성.
        //몹 배치
        //아이템 배치
        //플레이어 캐릭터 배치
        //플레이어 파티 초기화
        //키 이벤트 등록
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            leader.MainAttack();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            leader.MoveToPosition(leader.coordinate + (new Coordinate(0, 1) * leader.movePoint));
            leader.direction = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            leader.MoveToPosition(leader.coordinate + (new Coordinate(-1, 0) * leader.movePoint));
            leader.direction = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            leader.MoveToPosition(leader.coordinate + (new Coordinate(0, -1) * leader.movePoint));
            leader.direction = Direction.Down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            leader.MoveToPosition(leader.coordinate + (new Coordinate(1, 0) * leader.movePoint));
            leader.direction = Direction.Right;
        }
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
