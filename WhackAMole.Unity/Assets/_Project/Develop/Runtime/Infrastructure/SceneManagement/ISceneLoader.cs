using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;

namespace _Project.Develop.Runtime.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        UniTask<Scene> LoadAsync(
            string sceneKey,
            CancellationToken cancellationToken,
            Action<IContainerBuilder> extraRegistrations = null,
            IProgress<float> progress = null);

        UniTask UnloadAsync(Scene scene, CancellationToken cancellationToken);
    }
}
