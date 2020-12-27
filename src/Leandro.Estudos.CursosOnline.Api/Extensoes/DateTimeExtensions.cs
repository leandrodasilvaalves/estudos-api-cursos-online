using System;

namespace Leandro.Estudos.CursosOnline.Api.Extensoes
{
  public static class DateTimeExtensions
  {
    public static long ToUnixEpocate(this DateTime date)
    {
      return (long)Math.Round(
            (date.ToUniversalTime() - new DateTimeOffset(
                                            year: 1970, month: 1, day: 1,
                                            hour: 0, minute: 0, second: 0,
                                            offset: TimeSpan.Zero))
                                            .TotalSeconds);
    }
  }
}