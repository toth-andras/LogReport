# LogReport
#### Автор: Андраш Тот

Решение задачи со второго этапа отбора на [Kaspersky Safeboard 2023](https://safeboard.kaspersky.com/).

## Условие

### Легенда
В компании “Roga & Kopyta Digital” есть несколько сервисов. Для удобства поддержки эти сервисы пишут логи в виде текстовых файлов. Чтобы быстро работать с файлами логов они ротируются, то есть, когда размер файла лога превышает некоторый порог в S байт – он переименовывается с добавлением номера ротации в имя, а текущий лог направляется в новый файл. Для экономии места система хранит не больше N ротации для каждого сервиса.

### Формат логов
Имя текущего файла логов для сервиса: __<service_name>.log__.
Где `service_name` – имя сервиса.

Имя предыдущих файлов логов для сервиса: __<service_name>.<rotation_number>.log__.
Где `rotation_number` равный 1 – предыдущий файл логов, а равный N – самый старый файл. Например: AwesomeService.13.log.

### Формат записи лога:
`[Дата Время][Категория_записи] Текст_записи`.

Пример:
[23.03.2023 14:00:00.235][RequestHandler] New request with 42 items received from user vlad@redsquare.ru.


### Задача
Компания поставила вам задачу реализовать сервис, генерирующий отчёты по логам сервисов.

На вход сервису должен подаваться запрос на поиск по имени сервиса с поддержкой регулярных выражений. Запрос также содержит путь к директории с файлами логов.

В ответ сервис возвращает коллекцию отчётов, для всех сервисов, попадающих под условия поиска.
Структура отчёта:

Имя сервиса
Дата и время самой ранней записи в логах
Дата и время самой последней записи в логах
Количество записей в каждой категории
Количество ротаций.
Форма реализации сервиса: REST API сервис или консольное приложение.

### Требования со звёздочкой (опциональные/бонусные)
Асинхронная реализация, где запрос не блокирует клиента в ожидании, а получает идентификатор задачи, и предоставляется возможность периодически узнать статус готовности отчёта.
Возможность посмотреть статус системы – количество запущенных задач и их идентификаторы (при выполнении требования 1)
Загрузка логов в систему с анонимизацией персональных данных (адресов электронной почты: example@domain.com -> e*a*p*e@domain.com).


## Решение

Для достижения гибкости программы при сборе информации из логов отчеты представлены
следующими классами и интерфейсами:
- `ILogReport` - Базовый интерфейс для представления отчета о конктретном логе
- `IServiceReport` - Базовый интерфейс для представления отчета о конктретном сервисе
- `LogReport` - Класс, хранящий информацию, которую необходимо собирать из логов по условию задачи
- `ServiceReport` - Класс, являющийся отчетом о конкретном сервисе, хранит данные, которые необходимо собрать о сервисе
по условию задачи
- `EmptyReport` - Класс, представляющий пустой отчет (для нестандартных и аварийных ситуаций)

На схеме ниже представлены вышеописанные сущности и их связи:

<img width="876" alt="reports" src="https://github.com/toth-andras/LogReport/assets/83181732/bb2d0508-2dea-4050-835f-09f890529f61">

Сбор информации для отчетов и их формирование представлено следующими классами и интерфейсами:
- `ILogReportCreator` - Базовый интерфейс для представления класса, формирующего отчет об одном логе
- `IServiceReportCreator` - Базовый интерфейс для представления класса, формирующего отчет об одном сервисе
- `IUserRequestReportCreator` - Базовый интерфейс для представления класса, формирующего отчет на запрос пользователя
- `LogReportCreator` - Класс, формирующий отчет о логе по условию задания
- `ServiceReportCreator` - Класс, формирующий отчет о сервисе по условию задания
- `UserRequestReportCreator` - Класс, формирующий отчет на запрос пользователя по условию задания

<img width="1280" alt="creators" src="https://github.com/toth-andras/LogReport/assets/83181732/ee4a6613-b128-47ab-b4e1-fb2ce71a2bc9">

Для анонимизации персональных данных используется класс `EmailAnonymizerService`.

Дополнительно используется пакет  [OneOf](https://github.com/mcintyre321/OneOf).
