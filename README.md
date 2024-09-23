# Векторное МКЭ

## Реализация ПО

#### Программный комлекс реализует трехмерное векторное МКЭ

- Реализация иерархического базиса:
  - [x] Линейный базис (первое краевое условие)
  - [ ] Квадратичный базис
  - [ ] Кубический базис


- Проект состоит из 2 частей:
  - Клиент с графическим интерфейсом
  - Сервер, содержащий реализацию мкэ

#### Клиент общается с сервером посредство http-запросов:
 - Для отправки входных данных на сервер используется `HttpPost` запрос
   - Клиент отправляет на сервер данные о расположении расчётной области, параметрах дробления и дополнительных параметрах расчётной сессии: мю, гамма и выбраное краевое условие.
   - Сервер возвращает id расчётной сессии, значение относительной погрешности и количество итераций решателя СЛАУ
 - Для получения результата тестирования с сервера используется `HttpGet` запрос
   - Клиент отправляет на сервер id расчётной сессии.
   - Сервер возвращает JSON строку в которой содержится информация о результате данной расчётной сессии: список закодированных в Base64 графиков расчётной области, а так же списки $q$, $q\*$, $\||q - q\*||$ и $\frac{||q - q\*||}{||q\*||}$

## Запуск проекта

### Запуск из IDE
 - Сервер запускается конфигурацией `FEM.Server: http`
 - Клиент конфигурацией `Client.Avalonia`

По-умолчанию, сервер поднимается автоматически с клиентом, однако после внесения изменений в серверную часть, **проект нужно пересобрать**.

### Запуск из сборки
Для запуска проекта из сборки, необходимо запустить клиент (готовый билд находится в папке ___Redist\net8.0___). Для запуска нужно
запустить приложение `Client.Avalonia.exe`. Сервер запустится автоматически.

### Использование программы
Для получения решения на клиенте необходимо задать параметры сетки, а также коэффициенты $μ$ и $γ$, затем нажать
кнопку `Посчитать`.

Параметры сетки задаются следющим образом:

* Центр расчётной области
* Расстояние от центра до границ расчётной области

Например, центр в точке $(0,0,0)$ и расстояние до границ $(1,1,1)$ образуют расчётную область $[-1;1]x[-1;1]x[-1;1]$.

В качестве ответа сгенерируется таблица со следующими столбцами:
1. Номер ребра
2. Аналитическое решение
3. Численное решение
4. Разность решений
5. Относительная погрешность решения

Кнопка `Cкачать отчёт` генерирует pdf файл с визуализацией построенной сетки и численным решением на каждом из рёбер.

Для изменения искомой функции необходимо поменять её в файле ***/FEM.Server/Services/ProblemService/ProblemService.cs*** методы
`ResolveMatrixContributionsAsync` (описывает искомую функцию) и `ResolveVectorContributionsAsync` (описывает $rot(rot\vec{A})$).

**После изменения искомой функции, потребуется перекомпиляция проекта**