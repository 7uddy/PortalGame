/// <summary>
/// Index values related to game scenes.
/// </summary>
public enum SceneIndexes
{
    TITLE_SCREEN = 0,
    GAME
}

/// <summary>
/// Implements functionality related to SceneIndexes enumeration. 
/// </summary>
public static class SceneHelper
{
    public static int ConvertEnumToInt(this SceneIndexes sceneIndex)
    {
        return (int)sceneIndex;
    }
}