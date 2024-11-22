using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    private void Awake() => gameController = this;


    private MinutesSeconds FinalBossSpawnTime;
    private bool IsEndless = false;

    public void SetFinalBossSpawnTime(int min, int sec) => FinalBossSpawnTime = new MinutesSeconds(min, sec);
    public void SetEndless(bool isEndless) => IsEndless = isEndless;

    public MinutesSeconds GetFinalBossSpawnTime() => FinalBossSpawnTime;

}
