namespace AIAgentHarryDemo
{
    public static class ContactFormatter
    {
        public static string Format(ContactDetails contact)
        {
            return string.Join(
                "\n",
                new[]
                {
                    ("Anrede", contact.Salutation),
                    ("Vorname", contact.FirstName),
                    ("Name", contact.LastName),
                    ("Position", contact.Position),
                    ("Telefonnummer", contact.Phone),
                    ("E-Mail-Adresse", contact.Email),
                    ("Bemerkungen", contact.Remarks)
                }
                .Select(line => (line.Item1, Value: Normalize(line.Item2)))
                .Where(line => !string.IsNullOrWhiteSpace(line.Value))
                .Select(line => FormatLine(line.Item1, line.Value)));
        }

        private static string Normalize(string? value)
        {
            return value?.Trim() ?? string.Empty;
        }

        private static string FormatLine(string label, string value)
        {
            return $"{label}: {value}";
        }
    }
}
