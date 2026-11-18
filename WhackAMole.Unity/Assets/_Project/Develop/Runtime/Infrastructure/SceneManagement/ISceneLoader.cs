namespace _Project.Develop.Runtime.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        void Load(string sceneKey);

        void Load<TArgs>(string sceneKey, TArgs args);
    }
}
