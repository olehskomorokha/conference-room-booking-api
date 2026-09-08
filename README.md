# Conference Room Booking API

API для управління залами, бронюваннями та розрахунку вартості оренди.

## Бізнес-завдання

Опис бізнес завдань:

- Забезпечити швидке бронювання кімнати на потрібну дату й часовий інтервал.
- Не допустити подвійного бронювання однієї кімнати в час, що перетинається.
- Допомогти клієнту вибрати кімнату, яка відповідає необхідній місткості та доступна у вказаний час.
- Автоматично розраховувати ціну бронювання з урахуванням базової вартості кімнати, додаткових послуг та тарифу.
- Надати адміністраторам дані для ухвалення рішень: завантаженість кімнат, виручку та рейтинг прибутковості.

## Технічні рішення

Було обрано 3-Tier архітектуру, оскільки вона дозволяє розділити застосунок на три незалежні рівні: Api, Business Logic та Data Access. Такий поділ спрощує підтримку, тестування й подальше розширення системи.

```text
ConferenceRoomBooking.API       HTTP API, контролери, Swagger, DI
ConferenceRoomBooking.Service   бізнес-логіка, DTO, мапери, винятки
ConferenceRoomBooking.Data      EF Core, сутності, репозиторії, міграції
```

### Правило конфлікту бронювань

Для однієї кімнати не можна створити бронювання, якщо інтервали перетинаються. Наприклад, за наявності бронювання `10:00–12:00` запит на `11:00–13:00` буде відхилено, а `12:00–14:00` — дозволено.

### Як реалізовано перевірку конфлікту бронювань

Під час створення бронювання репозиторій шукає бронювання тієї самої кімнати на ту саму дату. Конфлікт існує, коли початок нового інтервалу раніше за кінець існуючого **і** кінець нового інтервалу пізніше за початок існуючого:

```csharp
booking.ConferenceRoomId == model.ConferenceRoomId &&
booking.Date == model.Date &&
booking.StartTime < model.EndTime &&
booking.EndTime > model.StartTime
```

Якщо хоча б один запис відповідає цій умові, метод повертає `false`, а `BookingService` створює `BookingException` з кодом `Room_unavailable`.

Перевірка і додавання нового запису виконуються всередині транзакції з рівнем ізоляції `Serializable`. Це запобігає ситуації, коли два одночасні запити незалежно проходять перевірку та створюють бронювання на один часовий інтервал.

## Запуск проєкту

### 1. Клонувати репозиторій

```powershell
git clone git@github.com:olehskomorokha/conference-room-booking-api.git
```

### 2. Налаштувати підключення до SQL Server

Вказати ConnectionString в `ConferenceRoomBooking.API/Program.cs`:

```text
Server=YourSqlServer;Database=conferenceRoomBookingDb;Trusted_Connection=True;TrustServerCertificate=True;
```

### 3. Запустити застосунок

```powershell
dotnet run --project ConferenceRoomBooking.API --launch-profile https
```

За замовчуванням API буде доступне за адресами:

- `https://localhost:7109`
- `http://localhost:5297`

Також за адресою 

```text
https://localhost:7109/swagger

```

доступний Swagger з документацією

## Приклади звітів

```http
GET /api/reports/Room-utilization?from=2026-09-01&to=2026-09-30
GET /api/reports/Revenue?from=2026-09-01&to=2026-09-30
GET /api/reports/Room-profitability?from=2026-09-01&to=2026-09-30
```

## Приклади запитів API

У прикладах використовується адреса `https://localhost:7109`.

### 1. Додавання конференц-залу

```http
POST https://localhost:7109/api/ConferenceRoom
Content-Type: application/json

{
  "name": "Зал А",
  "capacity": 50,
  "basePricePerHour": 2000,
  "additionalServiceIds": [1, 2]
}
```

Успішна відповідь: `200 OK` з унікальним ID створеного залу, наприклад `4`.

### 2. Редагування інформації про зал

```http
PUT https://localhost:7109/api/ConferenceRoom/4
Content-Type: application/json

{
  "basePricePerHour": 2500,
  "additionalServicesIds": [1, 2, 3]
}
```

Успішна відповідь: `200 OK`.

> Зверніть увагу: у DTO для оновлення поле називається `additionalServicesIds`.

### 3. Видалення конференц-залу

```http
DELETE https://localhost:7109/api/ConferenceRoom/4
```

Успішна відповідь: `204 No Content`.

### 4. Пошук доступних залів

```http
POST https://localhost:7109/api/ConferenceRoom/GetAvailable
Content-Type: application/json

{
  "capacity": 50,
  "date": "2026-10-01",
  "from": "10:00:00",
  "to": "14:00:00"
}
```

Успішна відповідь: `200 OK` зі списком кімнат, які мають місткість щонайменше 50 осіб і вільні в зазначений інтервал.

### 5. Бронювання залу

```http
POST https://localhost:7109/api/Booking
Content-Type: application/json

{
  "conferenceRoomId": 1,
  "userName": "Ірина Петренко",
  "date": "2026-10-01",
  "startTime": "10:00:00",
  "endTime": "14:00:00",
  "additionalServiceIds": [1, 2]
}
```

Успішна відповідь: `200 OK` із загальною вартістю бронювання. Якщо час перетинається з існуючим бронюванням цієї кімнати, API поверне помилку з кодом `Room_unavailable`.
