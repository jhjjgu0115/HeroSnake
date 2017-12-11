using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [SerializeField]
    int width = 100;
    [SerializeField]
    int height = 100;
    [SerializeField]
    int splitTime = 2;
    public mSpace space = new mSpace(0, 0, 100, 100);
    Tree map;

    TileMapManager tm;
    // Use this for initialization
    void Start ()
    {
        map = new Tree(space);
        map = map.splitmSpace(space, 2);
        tm = TileMapManager.Instance;
        tm.Initialize(width, height);

        tm.GetTile(1, 2).SetGroundInfo(EGround.Magma);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
public class mSpace {//공간을 나타내는 클래스 x,y좌표와 너비와 높이를 가짐
    public int x, y, w, h;
    public mSpace(int x,int y,int w,int h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
    
}
public class splitedmSpace
{
    public mSpace first;
    public mSpace second;
    int random(int min, int max)
    {
        return Random.Range(min, max + 1);
    }
    int randomRatio(int max, float ratio)
    {
        return Random.Range((int)((float)max*ratio),(int)((float)max*(1-ratio)));
    }
    public splitedmSpace(mSpace space)
    {
        if (random(0,1) == 0)
        {//세로
            first = new mSpace(space.x,space.y, randomRatio(space.w, (float)0.4),space.h);
            second = new mSpace(space.x+first.w, space.y, space.w-first.w, space.h);
        } else
        {//가로
            first = new mSpace(space.x, space.y, space.w, randomRatio(space.h, (float)0.4));
            second = new mSpace(space.x , space.y+first.h, space.w , space.h-first.h);
        }
    }
}
public class Tree {
    mSpace root;
    Tree childl;
    Tree childr;
    public Tree(mSpace init)
    {
        root = init;
    }
    public Tree splitmSpace(mSpace space,int iter)
    {
        Tree rt = new Tree(space);
        if(iter != 0)
        {
            splitedmSpace asdf = new splitedmSpace(space);
            rt.childl = splitmSpace(asdf.first,iter - 1);
            rt.childr = splitmSpace(asdf.second, iter - 1);
        }
        return rt;
    }
}
