using IoT.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Запуск додатка
var app = builder.Build();

// Ендпоінт для отримання місцезнаходження вантажу
app.MapGet("/getCurrentTransferLocation?id={transferId}", (int transferId) =>
{
  // Отримання фейкових даних про місцезнаходження вантажу
  var location = MockData.GetRandomTransferLocation();

  // Перетворення в JSON та відправлення відповіді
  return Results.Json(location);
});

// Ендпоінт для отримання стану вантажу
app.MapGet("/getCurrentTransferCondition?id={transferId}", (int transferId) =>
{
  // Отримання фейкових даних про стан вантажу
  var condition = MockData.GetRandomTransferCondition();

  // Перетворення в JSON та відправлення відповіді
  return Results.Json(condition);
});

app.Run();
