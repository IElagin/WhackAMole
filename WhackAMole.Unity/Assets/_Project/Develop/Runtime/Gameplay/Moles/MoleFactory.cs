using System;
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

        public Mole Create(Transform hole, int holeNumber)
        {
            if (_prefab == null)
                _prefab = LoadPrefab();

            Mole mole = _resolver.Instantiate(_prefab, hole);
            mole.Setup(holeNumber);

            return mole;
        }

        private static Mole LoadPrefab()
        {
            Mole prefab = Resources.Load<Mole>(MOLE_PREFAB_PATH);

            if (prefab == null)
                throw new InvalidOperationException($"Префаб крота не найден в Resources по пути '{MOLE_PREFAB_PATH}'");

            return prefab;
        }
    }
}
