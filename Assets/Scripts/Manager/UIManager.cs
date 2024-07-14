using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoSingleton<UIManager>
{
    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelGameOver;
    public Text uiLife;
    public Text uiLeveleName;
    public Text uiLeveleStartName;
    public Slider hpBar;
    public GameObject uiLevelStart;
    public GameObject uiLevelEnd;


    void Start ()
    {
        panelReady.SetActive(true);
        this.uiLeveleName.text = string.Format("LEVEL {0} {1}", LevelManager.Instance.level.LevelID, LevelManager.Instance.level.Name);

    }

    void Update ()
    {
        this.hpBar.value = Mathf.Lerp(this.hpBar.value, Game.Instance.player.HP, 0.1f);
        if (Game.Instance.player != null)
            this.uiLife.text = Game.Instance.player.life.ToString(); 
    }

    public void UpdateUI()
    {
        this.panelReady.SetActive(Game.Instance.Status == GAME_STATUS.Ready);
        this.panelInGame.SetActive(Game.Instance.Status == GAME_STATUS.InGame);
        this.panelGameOver.SetActive(Game.Instance.Status == GAME_STATUS.GameOver);
        this.hpBar.maxValue = Game.Instance.player.MaxHP;
        this.hpBar.value = Game.Instance.player.HP;
    }

    public void ShowLevelStart(string name)
    {
        this.uiLeveleName.text = name;
        this.uiLeveleStartName.text = name;
        uiLevelStart.SetActive(true);
        //Destroy(go, 2f);
    }
}
