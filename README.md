# Conference Room Booking API

API для управління залами, бронюваннями та розрахунку вартості оренди

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
git clone <repository-url>
Set-Location conference-room-booking-api
```

### 2. Налаштувати підключення до SQL Server

Поточне підключення вказано в `ConferenceRoomBooking.API/Program.cs`:

```text
Server=localhost\SQLEXPRESS;Database=conferenceRoomBookingDb;Trusted_Connection=True;TrustServerCertificate=True;
```

### 3. Запустити застосунок

```powershell
dotnet run --project ConferenceRoomBooking.API --launch-profile https
```

За замовчуванням API буде доступне за адресами:

- `https://localhost:7109`
- `http://localhost:5297`

У режимі `Development` перейдіть у браузері за адресою:

```text
https://localhost:7109/swagger

```

## Приклади звітів

```http
GET /api/reports/Room-utilization?from=2026-09-01&to=2026-09-30
GET /api/reports/Revenue?from=2026-09-01&to=2026-09-30
GET /api/reports/Room-profitability?from=2026-09-01&to=2026-09-30
```
