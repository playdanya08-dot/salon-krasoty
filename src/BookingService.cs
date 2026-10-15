
using Microsoft.Extensions.Logging;

namespace SalonBeauty;

public class TimeAlreadyBookedException : Exception
{
    public TimeAlreadyBookedException()
        : base("Это время уже занято. Выберите другое.")
    {
    }
}

public class ServiceUnavailableException : Exception
{
    public ServiceUnavailableException()
        : base("Выбранная услуга сейчас недоступна.")
    {
    }
}

public class BookingService
{
    private readonly ILogger<BookingService> _logger;

    public BookingService(ILogger<BookingService> logger)
    {
        _logger = logger;
    }

    public bool CreateBooking(
        SalonBooking booking,
        bool serviceIsAvailable,
        bool timeIsBooked)
    {
        try
        {
            if (!serviceIsAvailable)
                throw new ServiceUnavailableException();

            if (timeIsBooked)
                throw new TimeAlreadyBookedException();

            _logger.LogInformation(
                "Запись создана. Клиент: {ClientName}, услуга: {ServiceName}, дата: {VisitDate}",
                booking.ClientName,
                booking.ServiceName,
                booking.VisitDate);

            Console.WriteLine("Запись успешно оформлена.");
            return true;
        }
        catch (ServiceUnavailableException ex)
        {
            _logger.LogWarning(
                ex, "Услуга недоступна: {ServiceName}",
                booking.ServiceName);

            Console.WriteLine(ex.Message);
            return false;
        }
        catch (TimeAlreadyBookedException ex)
        {
            _logger.LogWarning(
                ex, "Время занято: {VisitDate}",
                booking.VisitDate);

            Console.WriteLine(ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, "Ошибка оформления записи клиента {ClientName}",
                booking.ClientName);

            Console.WriteLine(
                "Не удалось оформить запись. Попробуйте позже.");

            return false;
        }
        finally
        {
            _logger.LogInformation(
                "Попытка оформления записи завершена для клиента {ClientName}",
                booking.ClientName);
        }
    }
}
