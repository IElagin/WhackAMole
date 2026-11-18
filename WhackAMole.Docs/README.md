# WhackAMole.Docs

Документация проекта: архитектура, принятые решения, соглашения.

## Smart Merge и LFS на новой машине

Привязка типов файлов к LFS и к UnityYAMLMerge лежит в `.gitattributes` в корне репозитория. Драйвер слияния регистрируется один раз на компьютер (путь — к своей версии Unity):

```bash
git config --global merge.unityyamlmerge.name "Unity SmartMerge (UnityYAMLMerge)"
git config --global merge.unityyamlmerge.driver "'/Applications/Unity/Hub/Editor/6000.6.0f1/Unity.app/Contents/Helpers/UnityYAMLMerge' merge -h -p --force --fallback none %O %B %A %A"
git config --global merge.unityyamlmerge.recursive binary
git lfs install
```

Проверка: `git config --global --get-regexp unityyamlmerge` и `git lfs ls-files`.
