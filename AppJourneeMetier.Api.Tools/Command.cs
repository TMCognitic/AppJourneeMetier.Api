namespace AppJourneeMetier.Api.Tools
{
    public class Command
    {
        internal string Query { get; init; }
        internal IDictionary<string, object> Parameters { get; init; }

        public Command(string query)
        {
            Query = query;
            Parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string parameterName, object? value)
        {
            Parameters.Add(parameterName, value ?? DBNull.Value);
        }
    }
}