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


        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Tanker")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Dealer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Healer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Dealer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Healer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Tanker")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Dealer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Healer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Tanker")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Dealer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Healer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Tanker")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Dealer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Healer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Tanker")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Dealer")));
        playerUnitList.Add(Instantiate(Resources.Load<Unit>("Prefabs/Characters/Healer")));
        int count = 0;
        foreach (Unit unit in playerUnitList)
        {
            unit.coordinate=new Coordinate(count,count);
            SetUnit(unit.coordinate, unit);
            PartyManager.Instance.memberList.Add(unit);
            count++;
        }
        StartCoroutine(GameTick());
        leader = playerUnitList[0];
        PartyManager.Instance.leader = leader;
        PartyManager.Instance.memberList.Remove(leader);




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
            PartyManager.Instance.PartyAct();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PartyManager.Instance.Move(E_Direction.Up);
            //leader.MoveToPosition(leader.coordinate + (new Coordinate(0, 1) * leader.movePoint));
            //leader.direction = E_Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PartyManager.Instance.Move(E_Direction.Left);
            //leader.MoveToPosition(leader.coordinate + (new Coordinate(-1, 0) * leader.movePoint));
            //leader.direction = E_Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PartyManager.Instance.Move(E_Direction.Down);
            //leader.MoveToPosition(leader.coordinate + (new Coordinate(0, -1) * leader.movePoint));
            //leader.direction = E_Direction.Down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PartyManager.Instance.Move(E_Direction.Right);
            //leader.MoveToPosition(leader.coordinate + (new Coordinate(1, 0) * leader.movePoint));
            //leader.direction = E_Direction.Right;
        }
    }


    void SetDirection(E_Direction direction)
    {
        inputDirection = direction;
    }

    void SetUnit(Coordinate c,Unit u)
    {
        u.MoveToPosition(c);
    }
    E_Direction inputDirection;
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
