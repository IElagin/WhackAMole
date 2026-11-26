using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Gameplay.Moles
{
    public sealed class MoleFactory
    {
        private const string MOLE_PREFAB_PATH = "Gameplay/Mole";

        private readonly IObjectResolver _resolver;

        private Mole _prefab;

        public MoleFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public async UniTask LoadPrefabAsync(IProgress<float> progress, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ResourceRequest request = Resources.LoadAsync<GameObject>(MOLE_PREFAB_PATH);
            GameObject prefab = (GameObject)await request.ToUniTask(progress, cancellationToken: cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            if (prefab == null || !prefab.TryGetComponent(out Mole mole))
                throw new InvalidOperationException($"В Resources по пути '{MOLE_PREFAB_PATH}' нет префаба с компонентом Mole");

            _prefab = mole;
            progress.Report(1f);
        }

        public Mole Create(Transform hole, int holeNumber)
        {
            Mole mole = _resolver.Instantiate(_prefab, hole);
            mole.Setup(holeNumber);

            return mole;
        }
    }
}
