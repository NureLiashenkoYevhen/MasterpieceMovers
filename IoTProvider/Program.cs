// Створення об'єкта "builder" для налаштування веб-застосунку.
var builder = WebApplication.CreateBuilder(args);

// Додавання служб контролерів до сервісного контейнера.
builder.Services.AddControllers();

// Додавання служб для генерації API-документації та розгортання точок доступу.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Побудова об'єкта "app" на основі налаштувань, визначених у "builder".
var app = builder.Build();

// Якщо застосунок працює в режимі розробки, то включаємо Swagger та його інтерфейс користувача.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Використання перенаправлення HTTPS для підвищення безпеки з'єднань.
app.UseHttpsRedirection();

// Використання авторизації для захисту ресурсів.
app.UseAuthorization();

// Встановлення маршрутів для контролерів.
app.MapControllers();

// Запуск застосунку.
app.Run();
