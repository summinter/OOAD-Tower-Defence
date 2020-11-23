using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class MapGenerator:MonoBehaviour {
    public int width;
    public int height;

    // public string seed;
    // public bool useRandomSeed;
    public int roadLength;
    public GameObject Cube;
    public GameObject RoadCube;

    // [Range(0,100)]
    // public int randomFillPercent;

    int[,] map;

    public class Walker {
		public int x;
		public int y;
	}
    Vector2 RandomDirection(){
    int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);
    switch (choice){
        case 0:
            return Vector2.down;
        case 1:
            return Vector2.left;
        case 2:
            return Vector2.up;
        default:
            return Vector2.right;
    }
}
    void Start() {
        GenerateMap();
    }


    void GenerateMap(){
        map = new int[width, height];
        for (int i = 0; i< width; i++){
            for(int j = 0; j < height; j++){
                map[i,j] =1;
            }
        }
        while(true){
            bool isSuccess = WalkingThroughMap();
            if(isSuccess){
                break;
            }
        }
        GenerateCube();

    }
    bool WalkingThroughMap(){
        map[0,0] = 0;
        int[,] new_map = map;
        Walker walker = new Walker();
        int count = 0;
        walker.x = 0;
        walker.y = 0;
        while(true){
            //判断四个方向能不能走,确定方向
            int choice = JudgeDir(walker, new_map);
            //不能走则重新生成
            if (choice == 0){
                return false;
            }else if (choice == 1){
                new_map[walker.x - 1, walker.y] = 0;
                new_map[walker.x - 2, walker.y] = 0;
                walker.x -= 2;
            }else if (choice ==2){
                new_map[walker.x + 1, walker.y] = 0;
                new_map[walker.x + 2, walker.y] = 0;     
                walker.x += 2;           
            }else if (choice == 3){
                new_map[walker.x, walker.y - 1] = 0;
                new_map[walker.x, walker.y - 2] = 0;
                walker.y -= 2;
            }else{
                new_map[walker.x, walker.y + 1] = 0;
                new_map[walker.x, walker.y + 2] = 0;   
                walker.y += 2;            
            }
            
            count++;
            if (count >= roadLength){
                map = new_map;
                return true;
            }
            
            //前进

            
        }
    }

    int JudgeDir(Walker walker, int[,] new_map){
        List<int> dirList = new List<int>();
        //左
        if (walker.x > 1 && map[walker.x - 1, walker.y] != 0 && map[walker.x - 2, walker.y] != 0){
            dirList.Add(1);
        }

        //右
        if (walker.x < width - 2 && map[walker.x + 1, walker.y] != 0 && map[walker.x + 2, walker.y] != 0){
            dirList.Add(2);
        }
        //上
        if (walker.y > 1 && map[walker.x, walker.y - 1] != 0 && map[walker.x, walker.y - 2] != 0){
            dirList.Add(3);
        }
        //下
        if (walker.y < height - 2 && map[walker.x, walker.y + 1] != 0 && map[walker.x, walker.y + 2] != 0){
            dirList.Add(4);
        }

        int choice;
        if (dirList.Count!=0){
            int num = UnityEngine.Random.Range(0,dirList.Count);
            choice = dirList[num];
        }else{
            choice = 0;
        }
        return choice;

    }


    void GenerateCube(){
            if (map != null){
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    float pos_y = (map[x,y] == 1) ? .5f:.05f;
                    Vector3 pos = new Vector3(5 * x,pos_y, 5 * y);
                    if (map[x,y] == 1){
                        GameObject.Instantiate(Cube,pos,Quaternion.identity); 

                    }else{
                        GameObject.Instantiate(RoadCube,pos,Quaternion.identity); 
                    }
                }
            }
        }
    }
    



}