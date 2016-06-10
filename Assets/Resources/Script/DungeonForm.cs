using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//１部屋のクラス
public struct Room
{
    public int m_baseX;    //X軸基準点
    public int m_baseZ;    //Z軸基準点
    public int m_sizeX;    //X軸サイズ
    public int m_sizeZ;    //Z軸サイズ
}

public struct Space
{
    public int m_baseX;    //X軸基準点
    public int m_baseZ;    //Z軸基準点
    public int m_sizeX;    //X軸サイズ
    public int m_sizeZ;    //Z軸サイズ
    public Room m_room;
}

public class DungeonForm : MonoBehaviour
{
    public int MIN_ROOM_SIZE;       //部屋の最小サイズ
    int MIN_SPACE_SIZE;             //空間の最小サイズ
    public int MAX_MAP_X_SIZE;     //マップの最大サイズ
    public int MAX_MAP_Z_SIZE;     //マップの最大サイズ
    public int MIN_ROOM_NUM;      //最少部屋数

    Space m_space = new Space();
    private List<Space> m_spaceList;
    public int[,] m_map;//(仮)
    public GameObject Wall;
    public GameObject Floor;

    public enum MapID//(仮)
    {
        MAP_NOT_OBJECT = -1,
        MAP_WALL,
        MAP_ROOM,
        MAP_STAIRS,
        MAP_FLOOR,
        MAP_BOUNDARY
    };

    //一番最初に生成してほしいのでAwakeにしました。
    void Awake()
    {
        MIN_SPACE_SIZE = MIN_ROOM_SIZE + 4;

        do
        {
        m_map = new int[MAX_MAP_X_SIZE, MAX_MAP_Z_SIZE];//(仮)
        m_spaceList = new List<Space>();
        m_space.m_sizeX = MAX_MAP_X_SIZE;
        m_space.m_sizeZ = MAX_MAP_Z_SIZE;
        Divide(m_space);
        } while (m_spaceList.Count < MIN_ROOM_NUM);
        MakeRoom();
        MakeFloor();
     
        for(int x=0;x<MAX_MAP_X_SIZE;x++)
        {
            for(int z=0;z<MAX_MAP_Z_SIZE;z++)
            {
                if (m_map[x, z] == (int)MapID.MAP_WALL)
                {
                    //生成したオブジェクトを取得
                    GameObject obj = (GameObject)Instantiate(Wall, new Vector3(x, 0.5f, z),Quaternion.identity );
                    obj.transform.SetParent(this.transform);
                }
                else if (m_map[x, z] == (int)MapID.MAP_ROOM || m_map[x, z] == (int)MapID.MAP_FLOOR || m_map[x, z] == (int)MapID.MAP_BOUNDARY)
                {
                    //生成したオブジェクトを取得
                    GameObject obj = (GameObject)Instantiate(Floor, new Vector3(x, 0, z), Quaternion.identity);
                    obj.transform.SetParent(this.transform);
                }
            }
        }
    }

    //スペース分割関数
    void Divide(Space t_space)
    {
        Space splited_space1 = new Space();
        Space splited_space2 = new Space();
        bool flg = false;
        if (Random.Range(0, 2) == 0)
        {
            if (t_space.m_sizeX > MIN_SPACE_SIZE * 2)
            {
                DivideX(t_space, ref splited_space1, ref splited_space2);
                flg = true;
            }
        }
        else
        {
            if (t_space.m_sizeZ > MIN_SPACE_SIZE * 2)
            {
                DivideZ(t_space, ref splited_space1, ref splited_space2);
                flg = true;
            }
        }
        if (flg == true)
        {
            Divide(splited_space1);
            Divide(splited_space2);
        }
        else
        {
            //部屋設定
            t_space.m_room.m_sizeX = Random.Range(MIN_ROOM_SIZE, t_space.m_sizeX - 4);
            t_space.m_room.m_sizeZ = Random.Range(MIN_ROOM_SIZE, t_space.m_sizeZ - 4);
            t_space.m_room.m_baseX = Random.Range(2, t_space.m_sizeX - t_space.m_room.m_sizeX - 1) + t_space.m_baseX;
            t_space.m_room.m_baseZ = Random.Range(2, t_space.m_sizeZ - t_space.m_room.m_sizeZ - 1) + t_space.m_baseZ;
            m_spaceList.Add(t_space);
        }
    }
    //X軸の分断通路生成関数
    void DivideX(Space t_space, ref Space splited_space1, ref Space splited_space2)
    {
        int axisX = t_space.m_baseX + Random.Range(MIN_SPACE_SIZE, t_space.m_sizeX - MIN_SPACE_SIZE);
        for (int axisZ = t_space.m_baseZ; axisZ < t_space.m_baseZ + t_space.m_sizeZ; axisZ++)
        {
            m_map[axisX, axisZ] = (int)MapID.MAP_BOUNDARY;
        }
        splited_space1.m_baseX = t_space.m_baseX;
        splited_space1.m_baseZ = t_space.m_baseZ;
        splited_space1.m_sizeX = axisX - t_space.m_baseX;
        splited_space1.m_sizeZ = t_space.m_sizeZ;

        splited_space2.m_baseX = axisX + 1;
        splited_space2.m_baseZ = t_space.m_baseZ;
        splited_space2.m_sizeX = t_space.m_sizeX + t_space.m_baseX - (axisX + 1);
        splited_space2.m_sizeZ = t_space.m_sizeZ;
    }

    //Z軸の分断通路生成関数
    void DivideZ(Space t_space, ref Space splited_space1, ref Space splited_space2)
    {
        int axisZ = t_space.m_baseZ + Random.Range(MIN_SPACE_SIZE, t_space.m_sizeZ - MIN_SPACE_SIZE);
        for (int axisX = t_space.m_baseX; axisX < t_space.m_baseX + t_space.m_sizeX; axisX++)
        {
            m_map[axisX, axisZ] = (int)MapID.MAP_BOUNDARY;
        }
        splited_space1.m_baseX = t_space.m_baseX;
        splited_space1.m_baseZ = t_space.m_baseZ;
        splited_space1.m_sizeX = t_space.m_sizeX;
        splited_space1.m_sizeZ = axisZ - t_space.m_baseZ;

        splited_space2.m_baseX = t_space.m_baseX;
        splited_space2.m_baseZ = axisZ + 1;
        splited_space2.m_sizeX = t_space.m_sizeX;
        splited_space2.m_sizeZ = t_space.m_sizeZ + t_space.m_baseZ - (axisZ + 1);
    }

    //部屋生成関数s
    void MakeRoom()
    {
        for(int idx=0; idx<m_spaceList.Count;idx++)
        {
            for (int axisX = m_spaceList[idx].m_room.m_baseX; axisX < m_spaceList[idx].m_room.m_baseX + m_spaceList[idx].m_room.m_sizeX; axisX++)
            {
                for (int axisZ = m_spaceList[idx].m_room.m_baseZ; axisZ < m_spaceList[idx].m_room.m_baseZ + m_spaceList[idx].m_room.m_sizeZ; axisZ++)
                {
                        m_map[axisX, axisZ] = (int)MapID.MAP_ROOM;
                }
            }
        }

    }
    void MakeFloor()
    {
        int axisX, axisZ;
        for (int idx = 0; idx < m_spaceList.Count; idx++)
        {
            if (m_spaceList[idx].m_baseX > 0)
            {
                axisZ = m_spaceList[idx].m_room.m_baseZ + Random.Range(0, m_spaceList[idx].m_room.m_sizeZ);
                for (int lineX = m_spaceList[idx].m_room.m_baseX; lineX >= m_spaceList[idx].m_baseX - 1; lineX--)
                {
                    m_map[lineX, axisZ] = (int)MapID.MAP_FLOOR;
                }
            }
            if (m_spaceList[idx].m_baseX + m_spaceList[idx].m_sizeX<MAX_MAP_X_SIZE)
            {
                axisZ = m_spaceList[idx].m_room.m_baseZ + Random.Range(0, m_spaceList[idx].m_room.m_sizeZ);
                for (int lineX = m_spaceList[idx].m_room.m_baseX + m_spaceList[idx].m_room.m_sizeX; lineX <= m_spaceList[idx].m_baseX + m_spaceList[idx].m_sizeX; lineX++)
                {
                    m_map[lineX, axisZ] = (int)MapID.MAP_FLOOR;
                }
            }
            if (m_spaceList[idx].m_baseZ > 0)
            {
                axisX = m_spaceList[idx].m_room.m_baseX + Random.Range(0, m_spaceList[idx].m_room.m_sizeX);
                for (int lineZ = m_spaceList[idx].m_room.m_baseZ; lineZ >= m_spaceList[idx].m_baseZ-1; lineZ--)
                {
                    m_map[axisX,lineZ] = (int)MapID.MAP_FLOOR;
                }
            }
            if (m_spaceList[idx].m_baseZ + m_spaceList[idx].m_sizeZ < MAX_MAP_Z_SIZE)
            {
                axisX = m_spaceList[idx].m_room.m_baseX + Random.Range(0, m_spaceList[idx].m_room.m_sizeX);
                for (int lineZ = m_spaceList[idx].m_room.m_baseZ+ m_spaceList[idx].m_room.m_sizeZ; lineZ <= m_spaceList[idx].m_baseZ + m_spaceList[idx].m_sizeZ; lineZ++)
                {
                    m_map[axisX,lineZ] = (int)MapID.MAP_FLOOR;
                }
            }
        }

        for (axisX = 0; axisX < MAX_MAP_X_SIZE; axisX++)
        {
            if (m_map[axisX, 0] == (int)MapID.MAP_BOUNDARY)
            {
                int lineZ;
                for (lineZ = 0; m_map[axisX, lineZ] == (int)MapID.MAP_BOUNDARY; lineZ++)
                {
                    m_map[axisX, lineZ] = (int)MapID.MAP_WALL;
                }
            }
            if (m_map[axisX, MAX_MAP_Z_SIZE - 1] == (int)MapID.MAP_BOUNDARY)
            {
                for (int j = MAX_MAP_Z_SIZE - 1; m_map[axisX, j] == (int)MapID.MAP_BOUNDARY; j--)
                {
                    m_map[axisX, j] = (int)MapID.MAP_WALL;
                }
            }
        }

        for (axisZ = 0; axisZ < MAX_MAP_Z_SIZE; axisZ++)
        {
            if (m_map[0, axisZ] == (int)MapID.MAP_BOUNDARY)
            {
                for (int j = 0; m_map[j, axisZ] == (int)MapID.MAP_BOUNDARY; j++)
                {
                    m_map[j, axisZ] = (int)MapID.MAP_WALL;
                }
            }
            if (m_map[MAX_MAP_X_SIZE - 1, axisZ] == (int)MapID.MAP_BOUNDARY)
            {
                for (int j = MAX_MAP_X_SIZE - 1; m_map[j, axisZ] == (int)MapID.MAP_BOUNDARY; j--)
                {
                    m_map[j, axisZ] = (int)MapID.MAP_WALL;
                }
            }
        }
    }

    void Update()
    {
        
    }
}