using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    int width = 100;
    [SerializeField]
    int height = 100;
    [SerializeField]
    int splitTime = 2;
    public mSpace space;
    Tree map;

    public TileMapManager tm;
    // Use this for initialization
    void Start()
    {
        space = new mSpace(0, 0, width - 1, height - 1);
        map = new Tree(space);

        map = map.splitmSpace(space, splitTime);

        tm = TileMapManager.Instance;
        tm.Initialize(width, height);

        //Debug.Log(tm.GetTile(1, 2));
        //Tile test;
        //test = tm.GetTile(1, 2);
        //test.SetGroundInfo(EGround.Wall);
        Tree temp = map;
        drawPath(map, PathType.Wall);
        makeRoom(temp);
        drawSplitedSpace(temp);
        drawPath(map, PathType.Path);


        //drawPathBlock(map.root.mx, map.root.my);
        //Debug.Log(GetSpaceFromTree(map).root.x);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void makeRoom(Tree tree)
    {
        int range = 5;
        if (tree == null)
        {
            return;
        }

        //Debug.Log(tree.root.mx + " " + tree.root.my);
        if (tree.childl == null && tree.childr == null)
        {
            if (tree.root.w > 9 && tree.root.h > 9)//&& (tree.root.w/tree.root.h > 0.9|| tree.root.h / tree.root.w > 0.9))
            {
                int rand = Random.Range(4, range);
                //int randx = Random.Range(3, range);
                //int randy = Random.Range(3, range);
                tree.root.x += rand;
                tree.root.y += rand;
                tree.root.w -= rand;// + Random.Range(1, range);
                tree.root.h -= rand;// + Random.Range(1, range);
                //tree.root.mx = tree.root.x + (tree.root.w/2);
                //tree.root.my = tree.root.y + (tree.root.h/2);
            }
            return;
        }
        else
        {
            makeRoom(tree.childl);
            makeRoom(tree.childr);
        }
    }
    /*
    void GetSpaceFromTree(Tree tree, List<mSpace> list)
    {
        if (tree.childl == null && tree.childr == null)
        {
            list.Add(tree.root);
            return;
        }
        else
        {
            GetSpaceFromTree(tree.childl, list);
            GetSpaceFromTree(tree.childr, list);
        }
    }*/
    void drawSplitedSpace(Tree tree)
    {

        //Debug.Log(tree.root.x + " " + tree.root.y + " " + tree.root.w + " " + tree.root.h);
        if (tree.childl == null && tree.childr == null)
        {
            drawSpace(tree.root);
            //Debug.Log("null out");
            return;
        }
        else
        {
            drawSplitedSpace(tree.childl);
            drawSplitedSpace(tree.childr);
        }
    }
    void drawPath(Tree tree, PathType type)
    {
        Debug.Log(tree.root.mx + " " + tree.root.my);
        if (tree.childl == null && tree.childr == null)
        {
            return;
        }
        else
        {
            drawStraitLine(tree.childl.root, tree.childr.root, type);
            drawPath(tree.childl, type);
            drawPath(tree.childr, type);
        }
    }
    void drawStraitLine(mSpace a, mSpace b, PathType type)
    {
        //Debug.Log("A" + a.mx + " " + a.my + "B" + b.mx + " " + b.my);
        //int pathWidth = 1;
        if (a.mx == b.mx)
        {
            int start, end;
            if (a.my < b.my)
            {
                start = a.my;
                end = b.my;
            }
            else
            {
                start = b.my;
                end = a.my;
            }

            for (int i = start + 1; i < end; i++)
            {
                drawPathBlock(a.mx, i, type);
                /*Tile test;
                test = tm.GetTile(a.mx, i);
                test.SetGroundInfo(EGround.Water);*/
            }
        }
        else if (a.my == b.my)
        {
            int start, end;
            if (a.mx < b.mx)
            {
                start = a.mx;
                end = b.mx;
            }
            else
            {
                start = b.my;
                end = a.my;
            }

            for (int i = start; i < end; i++)
            {
                drawPathBlock(i, a.my, type);
                /*
                Tile test;
                test = tm.GetTile(i, a.my);
                test.SetGroundInfo(EGround.Water);
                */
            }
        }
        else
        {
            Debug.Log("WEIRD POINT A" + a.mx + "," + a.my + " B:" + b.mx + "," + b.my);
        }

    }
    enum PathType
    {
        Wall,
        Path
    }
    void drawPathBlock(int x, int y, PathType type)
    {
        Tile test;
        if (type == PathType.Wall)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i != 0 && j != 0)
                    {
                        test = tm.GetTile(new Coordinate(x + i, y + j));
                        test.SetGroundInfo(EGround.Wall);
                    }
                }
            }
        }
        else
        {
            test = tm.GetTile(new Coordinate(x, y));
            test.SetGroundInfo(EGround.Ground);
        }

    }
    void drawSpace(mSpace target)
    {
        Tile test;
        //Debug.Log(target.x + " " + target.y + " " + target.w + " " + target.h);

        for (int i = 0; i < target.h + 1; i++)
        {
            for (int j = 0; j < target.w + 1; j++)
            {
                if (i == 0 || i == target.h || j == 0 || j == target.w)
                {
                    test = tm.GetTile(new Coordinate(target.x + j, target.y + i));
                    test.SetGroundInfo(EGround.Wall);
                }
                else
                {
                    test = tm.GetTile(new Coordinate(target.x + j, target.y + i));
                    test.SetGroundInfo(EGround.Ground);
                }
            }
        }
    }
}
public class mSpace
{//공간을 나타내는 클래스 x,y좌표와 너비와 높이를 가짐
    public int x, y, w, h;
    public int mx, my;
    public mSpace(int x, int y, int w, int h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        this.mx = x + w / 2;
        this.my = y + h / 2;
    }
}
public class splitedmSpace
{
    public mSpace first;
    public mSpace second;

    int random(int min, int max)
    {
        return UnityEngine.Random.Range(min, max + 1);
    }
    int randomRatio(int max, float ratio)
    {
        return UnityEngine.Random.Range((int)((float)max * ratio), (int)((float)max * (1 - ratio)));
    }
    public splitedmSpace(mSpace space)
    {
        float rate = (float)0.3;
        //if (space.w / space.h > 1.6 && )
        //if (random(0, 1) == 0)
        if (space.w / space.h > 0.95)
        {//세로
            this.first = new mSpace(space.x, space.y, randomRatio(space.w, rate), space.h);
            this.second = new mSpace(first.x + first.w, space.y, space.w - first.w, space.h);
        }
        else
        {//가로
            this.first = new mSpace(space.x, space.y, space.w, randomRatio(space.h, rate));
            this.second = new mSpace(space.x, first.y + first.h, space.w, space.h - first.h);
        }
    }
}
public class Tree
{
    public mSpace root;
    public Tree childl;
    public Tree childr;
    public Tree(mSpace init)
    {
        root = init;
    }
    public Tree splitmSpace(mSpace space, int iter)
    {
        Tree rt = new Tree(space);
        if (iter != 0)
        {
            splitedmSpace asdf = new splitedmSpace(space);
            rt.childl = splitmSpace(asdf.first, iter - 1);
            rt.childr = splitmSpace(asdf.second, iter - 1);
        }
        return rt;
    }
}
