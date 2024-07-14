using System.Collections.Generic;


public class LevelManager : MonoSingleton<LevelManager> {
    //public int currentLeveleId = 1;
    public List<Level1> level1s ;
    public Level1 level;


    public void LoadLevel(int levelID)
    {
        this.level = Instantiate<Level1>(level1s[levelID - 1]);
    }
}
