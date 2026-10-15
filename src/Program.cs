
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using SalonBeauty;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddSimpleConsole(options =>
    {
        options.SingleLine = true;
        options.TimestampFormat = "HH:mm:ss ";
    });

    builder.SetMinimumLevel(LogLevel.Information);
});

var logger = loggerFactory.CreateLogger<BookingService>();
var bookingService = new BookingService(logger);

var booking = new SalonBooking
{
    ClientName = "Анна Иванова",
    ServiceName = "Стрижка",
    VisitDate = DateTime.Now.AddDays(1),
    Price = 1500m,
    Status = "Новая"
};

var errors = new List<ValidationResult>();
var context = new ValidationContext(booking);

bool isValid = Validator.TryValidateObject(
    booking,
    context,
    errors,
    validateAllProperties: true);

if (!isValid)
{
    Console.WriteLine("Проверьте данные записи:");

    foreach (var error in errors)
        Console.WriteLine($"- {error.ErrorMessage}");

    return;
}

Console.WriteLine("Данные записи проверены.");

Console.WriteLine("\nПроверка 1: услуга недоступна.");
bookingService.CreateBooking(
    booking,
    serviceIsAvailable: false,
    timeIsBooked: false);

Console.WriteLine("\nПроверка 2: время занято.");
bookingService.CreateBooking(
    booking,
    serviceIsAvailable: true,
    timeIsBooked: true);

Console.WriteLine("\nПроверка 3: успешная запись.");
bookingService.CreateBooking(
    booking,
    serviceIsAvailable: true,
    timeIsBooked: false);

Console.WriteLine("\nНажмите Enter для выхода.");
Console.ReadLine();
