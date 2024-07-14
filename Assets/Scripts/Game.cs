using UnityEngine;
public class Game : MonoSingleton<Game> {
    
    private GAME_STATUS status;
    public GAME_STATUS Status
    {
        get { return status; }
        set
        {
            status = value;
            UIManager.Instance.UpdateUI();
        }
    }

    public Player player;
    private int currentLeveleId = 1;

    void Start ()
    {
        this.Status = GAME_STATUS.Ready;
        this.player.OnDeath += Player_OnDeath;    
	}

	private void Player_OnDeath(Unit sender)
    {
        if (player.life <= 0)        
            this.Status = GAME_STATUS.GameOver;        
        else       
            player.Rebirth();
    }

    public void StartGame()
    {
        InitPlayer();
        this.Status = GAME_STATUS.InGame;
        player.Fly();
        LoadLevel();
    }

    private void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(this.currentLeveleId);
        LevelManager.Instance.level.OnLveleEnd = OnLveleEnd;
        
    }

    private void OnLveleEnd(Level1.LEVEL_RESULT result)
    {
        if(result == Level1.LEVEL_RESULT.SUCCESS)
        {
            this.currentLeveleId++;
            this.LoadLevel();
        }
    }


    public void Restart()
    {
        this.Status = GAME_STATUS.Ready;
        this.player.Init();
    }

    void InitPlayer()
    {
        this.player.Init();
    }

}
