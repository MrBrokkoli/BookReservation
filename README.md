BookReservation
===========

Запуск
-------------

Запуск проекта идет через `docker-compose`. `docker-compose` включает в себя запуск API и сервера SQL (начальные порты 1433)
Миграция базы срабатывает при запуске проекта, создание базы и таблиц произойдет автоматически
Настройки подключения в файле `appsettings.json`

Структура проекта
-----------------

В каталоге `BookReservation` находится серверная часть проекта
В каталоге `BookReservation.DataAccess` находится часть работы с базой