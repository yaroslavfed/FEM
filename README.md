# Векторное МКЭ

## Реализация ПО

**Программный комлекс реализует трехмерное векторное МКЭ**

- Реализован линейный базис с первым краевым условием

- Проект состоит из 2 частей:
  - Клиент с графическим интерфейсом
  - Сервер, содержащий реализацию мкэ

## Запуск проекта

### Запуск из IDE
 - Сервер запускается конфигурацией `FEM.Server: http`
 - Клиент конфигурацией `Client.Avalonia`

По-умолчанию, сервер поднимается автоматически с клиентом, однако, после внесения изменений в серверную часть, **проект нужно будет пересобрать**.

### Запуск из сборки
Для запуска проекта из сборки, необходимо запустить клиент (готовый билд находится в папке ___Redist\net8.0___). Для запуска нужно
запустить приложение `Client.Avalonia.exe`. Сервер запустится автоматически.

### Использование программы
Для получения решения на клиенте необходимо задать параметры сетки, а также коэффициенты $μ$ и $γ$, затем нажать
кнопку `Посчитать`.

#### Параметры сетки задаются следющим образом:

* Центр расчётной области
* Расстояние от центра до границ расчётной области

Например, центр в точке $(0,0,0)$ и расстояние до границ $(1,1,1)$ образуют расчётную область $[-1;1]x[-1;1]x[-1;1]$.

#### В качестве ответа сгенерируется таблица со следующими столбцами:
1. Номер ребра
2. Аналитическое решение
3. Численное решение
4. Разность решений
5. Относительная погрешность решения

Кнопка `Cкачать отчёт` генерирует pdf файл с визуализацией построенной сетки и численным решением на каждом из рёбер.

#### Для изменения искомой функции необходимо поменять её в файле ***/FEM.Server/Services/ProblemService/ProblemService.cs***

- В методе `ResolveMatrixContributionsAsync` описывается искомая функция, для её изменения следует изменить следующий код:

```csharp
    List<double> contributionsFromVectorA =
    [
        35.0 * Math.Pow(coordinate.X, 3) + Math.Pow(coordinate.Y, 3) + Math.Pow(coordinate.Z, 3),
        Math.Pow(coordinate.X, 3) + 25.0 * Math.Pow(coordinate.Y, 3) + Math.Pow(coordinate.Z, 3),
        Math.Pow(coordinate.X, 3) + Math.Pow(coordinate.Y, 3) + 45.0 * Math.Pow(coordinate.Z, 3)
    ];
```

Например, данная функция представляет собой подобное:

> <img src="https://latex.codecogs.com/svg.image?\vec{A}=\begin{pmatrix}35x^3&plus;y^3&plus;z^3\\x^3&plus;25y^3&plus;z^3\\x^3&plus;y^3&plus;45z^3\end{pmatrix}" title="\vec{A}=\begin{pmatrix}35x^3+y^3+z^3\\x^3+25y^3+z^3\\x^3+y^3+45z^3\end{pmatrix}"  alt=""/>

- В методе `ResolveVectorContributionsAsync` описывает $rot(rot\vec{A})$, для её изменения следует изменить следующий код:

```csharp
    List<double> contributionsFromVectorF =
    [
        8.0 * (coordinate.Y + coordinate.Z),
        8.0 * (coordinate.X + coordinate.Z),
        8.0 * (coordinate.X + coordinate.Y)
    ];
```

Например, данная функция представляет собой подобное:

> <img src="https://latex.codecogs.com/svg.image?\vec{F}=8*\begin{pmatrix}y&plus;z\\x&plus;z\\x&plus;y\end{pmatrix}" title="\vec{F}=8*\begin{pmatrix}y+z\\x+z\\x+y\end{pmatrix}" />

**После изменения искомой функции, потребуется перекомпиляция проекта**

#### Клиент общается с сервером посредство http-запросов:
 - Для отправки входных данных на сервер используется `HttpPost` запрос
   - Клиент отправляет на сервер данные о расположении расчётной области, параметрах дробления и дополнительных параметрах расчётной сессии: мю, гамма и выбраное краевое условие.
   - Сервер возвращает id расчётной сессии, значение относительной погрешности и количество итераций решателя СЛАУ
 - Для получения результата тестирования с сервера используется `HttpGet` запрос
   - Клиент отправляет на сервер id расчётной сессии.
   - Сервер возвращает JSON строку в которой содержится информация о результате данной расчётной сессии: список закодированных в Base64 графиков расчётной области, а так же списки $q$, $q\*$, $\||q - q\*||$ и $\frac{||q - q\*||}{||q\*||}$

