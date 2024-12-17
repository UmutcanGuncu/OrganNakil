using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace OrganNakil.WebAPI.Configurations.ColumnWrites;

public class UsernameColumnWriter : ColumnWriterBase
{
    public UsernameColumnWriter() : base(NpgsqlDbType.Text)
    {
    }

    public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
    {
        if (logEvent.Properties.TryGetValue("username", out var username))
        {
            return username.ToString().Trim('"'); // Ekstra çift tırnakları kaldırmak için
        }

        return null; // Değer bulunamazsa null dönecek
    }
}