public enum SceneIndexes
{
    TITLE_SCREEN = 0,
    GAME
}
public class SceneHelper
{
    public static int ConvertEnumToInt(SceneIndexes sceneIndex)
    {
        return (int)sceneIndex;
    }
}