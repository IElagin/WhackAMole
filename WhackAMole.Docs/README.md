# WhackAMole.Docs

Документация проекта: архитектура, принятые решения, соглашения.

## Игра

ДЗ первой недели курса «Unity adventure professional»: «Бей крота» в консоли. Поле 3×3, лунки — клавиши 1–9 (слева направо, сверху вниз, верхний ряд цифр или numpad). Меню: клавиши 1…N выбирают уровень. После победы пробел — в меню; после поражения пробел — уровень заново, Esc — в меню. Весь вывод — в Console.

## Сборки

| asmdef | Ссылки | Что внутри |
|---|---|---|
| `_Project.Infrastructure` | VContainer | фундамент: загрузка сцен (`ISceneLoader`), сервис конфигов, статус приложения. Об игре не знает. |
| `_Project.Runtime` | Infrastructure, Utils, VContainer, Input System | `App` (скоупы, бутстрапы, навигация, ввод), `Gameplay` (кроты, забег, уровни), `Meta` (кошелёк, прогресс) |
| `_Project.Utils` | — | общие утилиты (пока пусто) |
| `_Project.Editor` | Infrastructure | `EntryPointSceneAutoLoader`: Play из любой сцены начинается с `Bootstrap` (меню `Tools/WhackAMole/Play from Bootstrap`) |

## Скоупы

- **Root** (`RootLifetimeScope`-префаб в `VContainerSettings`, живёт всю игру): `ISceneLoader`, `GameNavigation`, `ConfigsProviderService` + `ResourcesConfigLoader`, `WalletService`, `PlayerProgressService`, `KeyboardInput`, статус приложения.
- **Bootstrap**: `AppBootstrap` — 60 FPS, загрузка конфигов, стартовое золото, переход в меню.
- **MainMenu**: `MainMenuBootstrap` (запуск сцены), `MainMenuScreen` (вывод), `LevelSelector` (открыт ли уровень, переход), `MainMenuInput`.
- **Gameplay**: номер уровня приходит аргументом сцены (`LifetimeScope.Enqueue`, `GameplayArgs`). `GameplayBootstrap` (запуск сцены), `CurrentLevel`, `GameplayConsole`, `MoleFactory` (`IObjectResolver.Instantiate` префаба из `Resources/Gameplay/Mole`), `MoleField`, `MoleSpawner`, `WhackInput`, `RunSession`, `RunFinisher`, `RunEndInput`.

Скоупы только регистрируют; сцену запускает её бутстрап (`IStartable`).

## Конфиги (`Assets/_Project/Resources/Configs`)

- `GameConfig` — стартовое золото.
- `LevelsConfig` — список уровней (до 9); `Levels/LevelN` — попадания для победы, жизни, интервал появления кротов, время снаружи, золото за попадание.
- `GameNavigationConfig` — имена сцен.

## Smart Merge и LFS на новой машине

Привязка типов файлов к LFS и к UnityYAMLMerge лежит в `.gitattributes` в корне репозитория. Драйвер слияния регистрируется один раз на компьютер (путь — к своей версии Unity):

```bash
git config --global merge.unityyamlmerge.name "Unity SmartMerge (UnityYAMLMerge)"
git config --global merge.unityyamlmerge.driver "'/Applications/Unity/Hub/Editor/6000.6.0f1/Unity.app/Contents/Helpers/UnityYAMLMerge' merge -h -p --force --fallback none %O %B %A %A"
git config --global merge.unityyamlmerge.recursive binary
git lfs install
```

Проверка: `git config --global --get-regexp unityyamlmerge` и `git lfs ls-files`.
