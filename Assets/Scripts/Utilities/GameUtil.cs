using UnityEngine;
using UnityEditor;

public class GameUtil : Singleton<GameUtil>
{
    //判断位置是否在屏幕中（需要将世界坐标转为屏幕坐标）
    public bool InScreen(Vector3 position)
    {
        
        return Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(position));
    }
}