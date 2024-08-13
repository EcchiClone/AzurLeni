// 전역적인 헬퍼 클래스를 만들어 자주 사용하는 매니저와 데이터를 단축해서 접근
public static class Game
{
    public static DataManager Data => Managers.Instance.Data;
    public static NetworkManager Network => Managers.Instance.Network;
    public static ObjectPoolManager Pool => Managers.Instance.ObjectPool;
    public static UserData UserData => Data.UserData;
    public static GameData GameData => Data.GameData;
}