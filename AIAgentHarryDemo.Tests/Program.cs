using AIAgentHarryDemo;

var tests = new (string Name, Action Run)[]
{
    ("Format includes all filled contact fields in display order", ContactFormatterTests.FormatsAllFilledFieldsInOrder),
    ("Format trims whitespace around field values", ContactFormatterTests.TrimsFieldValues),
    ("Format omits empty contact fields", ContactFormatterTests.OmitsEmptyFields)
};

var failed = 0;
foreach (var test in tests)
{
    try
    {
        test.Run();
        Console.WriteLine($"PASS {test.Name}");
    }
    catch (Exception exception)
    {
        failed++;
        Console.WriteLine($"FAIL {test.Name}");
        Console.WriteLine(exception.Message);
    }
}

if (failed > 0)
{
    Console.WriteLine($"{failed} Test(s) failed.");
    return 1;
}

Console.WriteLine($"{tests.Length} Test(s) passed.");
return 0;

internal static class ContactFormatterTests
{
    public static void FormatsAllFilledFieldsInOrder()
    {
        var contact = new ContactDetails(
            "Herr",
            "Dirk",
            "Sandhorst",
            "Developer",
            "01234",
            "dirk@example.com",
            "Bitte zurueckrufen");

        var expected = string.Join(
            "\n",
            "Anrede: Herr",
            "Vorname: Dirk",
            "Name: Sandhorst",
            "Position: Developer",
            "Telefonnummer: 01234",
            "E-Mail-Adresse: dirk@example.com",
            "Bemerkungen: Bitte zurueckrufen");

        TestAssert.Equal(expected, ContactFormatter.Format(contact));
    }

    public static void TrimsFieldValues()
    {
        var contact = new ContactDetails(
            " Frau ",
            " Anna ",
            " Beispiel ",
            "",
            "",
            " anna@example.com ",
            "");

        var expected = string.Join(
            "\n",
            "Anrede: Frau",
            "Vorname: Anna",
            "Name: Beispiel",
            "E-Mail-Adresse: anna@example.com");

        TestAssert.Equal(expected, ContactFormatter.Format(contact));
    }

    public static void OmitsEmptyFields()
    {
        var contact = new ContactDetails(
            "",
            "Dirk",
            "",
            "",
            "",
            "dirk@example.com",
            "");

        var expected = string.Join(
            "\n",
            "Vorname: Dirk",
            "E-Mail-Adresse: dirk@example.com");

        TestAssert.Equal(expected, ContactFormatter.Format(contact));
    }
}

internal static class TestAssert
{
    public static void Equal(string expected, string actual)
    {
        if (expected == actual)
        {
            return;
        }

        throw new InvalidOperationException(
            $"Expected:\n{expected}\n\nActual:\n{actual}");
    }
}
