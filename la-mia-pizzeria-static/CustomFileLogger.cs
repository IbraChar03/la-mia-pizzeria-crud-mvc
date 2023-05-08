namespace la_mia_pizzeria_static
{
    public class CustomFileLogger : ICustomLogger
    {
        public void WriteLog(string message)
        {
            File.AppendAllText("c:/ex-log.txt", "LOG " + message + "\n");
        }
    }
}
