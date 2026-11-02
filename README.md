# WT-6 Minimal API

| Метод | URL | Назначение |
|---|---|---|
| GET | `/api/tasks` | Получить список задач (можно `?search=`) |
| GET | `/api/tasks/{id}` | Получить задачу по ID |
| POST | `/api/tasks` | Создать задачу |
| PUT | `/api/tasks/{id}` | Изменить задачу |
| DELETE | `/api/tasks/{id}` | Удалить задачу |