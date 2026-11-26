# WhackAMole.Docs

Документация проекта: архитектура, принятые решения, соглашения.

## Игра

«Бей крота» — ДЗ первых двух недель курса Unity Adventure Professional. Откройте проект `WhackAMole.Unity` в Unity 6000.6.0f1 и нажмите Play. При включённой настройке `Tools/WhackAMole/Play from Bootstrap` игра стартует через `Bootstrap` из любой открытой сцены. Включите отображение обычных сообщений в Unity Console. Для игры с клавиатуры выберите Game View.

В меню цифры 1–5 выбирают открытый уровень. Во время забега цифры верхнего ряда или numpad ударяют по лункам. Нумерация идёт по строкам слева направо, от верхнего ряда к нижнему. На поле 2×2 верхний ряд — 1, 2, нижний — 3, 4. На поле 2×3 верхний ряд — 1, 2, 3, нижний — 4, 5, 6. На 3×3 доступны все девять клавиш.

После победы нажмите пробел для возврата в меню. После поражения пробел перезапускает уровень, Esc возвращает в меню. Попадания, жизни и золото выводятся в Console.

## Сборки

| asmdef | Основные зависимости | Назначение |
|---|---|---|
| `_Project.Infrastructure` | VContainer, UniTask | Загрузка сцен и конфигов, выполнение шагов, прогресс, статус приложения |
| `_Project.Runtime` | Infrastructure, Utils, VContainer, UniTask, Input System | Скоупы, бутстрапы, навигация, ввод, игровое поле, забег, кошелёк и прогресс |
| `_Project.Utils` | — | Общие утилиты |
| `_Project.Editor` | Infrastructure | Выбор стартовой сцены для Play |

## Порядок запуска

1. Проектный `RootLifetimeScope` живёт между сценами. В нём зарегистрированы `GameTransitionService`, загрузчик сцен, сервисы конфигов, кошелёк и прогресс игрока.
2. В `Bootstrap` точка входа `AppBootstrap` реализует `IAsyncStartable`. Загрузка конфигов и выдача стартового золота завершаются до перехода в меню.
3. При переходе `ScenesService` загружает и активирует `Empty`, выгружает предыдущую сцену, загружает цель и выгружает буфер. Для Gameplay регистрация `GameplayArgs` действует на время загрузки сцены. Повторный запрос до завершения перехода пропускается.
4. `FlowRunner` последовательно выполняет шаги из локального скоупа. После `ISceneBootstrap.InitializeAsync` переход вызывает `Run()`. Меню показывает подготовленный список уровней и включает ввод. Gameplay загружает префаб крота и создаёт по одной лунке за кадр, затем запускает забег. В Console видны этап, количество готовых лунок и процент сборки.

## Конфиги

В `Assets/_Project/Resources/Configs` находятся стартовое золото (`GameConfig`), имена сцен (`GameNavigationConfig`) и список уровней (`LevelsConfig`). В `Levels/LevelN` задаются строки и столбцы от 1 до 3, попадания для победы, жизни, интервал появления, время снаружи и золото за попадание. Уровень 1 использует поле 2×2, уровень 3 — 2×3, уровни 2, 4 и 5 — 3×3.

## Smart Merge и LFS на новой машине

Привязка типов файлов к LFS и к UnityYAMLMerge лежит в `.gitattributes` в корне репозитория. Драйвер слияния регистрируется один раз на компьютер (путь — к своей версии Unity):

```bash
git config --global merge.unityyamlmerge.name "Unity SmartMerge (UnityYAMLMerge)"
git config --global merge.unityyamlmerge.driver "'/Applications/Unity/Hub/Editor/6000.6.0f1/Unity.app/Contents/Helpers/UnityYAMLMerge' merge -h -p --force --fallback none %O %B %A %A"
git config --global merge.unityyamlmerge.recursive binary
git lfs install
```

Проверка: `git config --global --get-regexp unityyamlmerge` и `git lfs ls-files`.
