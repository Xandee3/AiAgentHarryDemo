using System.Windows.Controls;
using AIAgentHarryDemo;

var tests = new (string Name, Action Run)[]
{
    ("Main window exposes contact and help menu items", MainWindowUiTests.ExposesMenuItems),
    ("Main window adds contacts to contact list", MainWindowUiTests.AddsContactsToContactList),
    ("Contact dialog formats values entered into UI controls", ContactDialogUiTests.FormatsValuesEnteredIntoUiControls),
    ("Contact dialog omits empty UI fields", ContactDialogUiTests.OmitsEmptyUiFields)
};

var failed = 0;
foreach (var test in tests)
{
    try
    {
        StaTestRunner.Run(test.Run);
        Console.WriteLine($"PASS {test.Name}");
    }
    catch (Exception exception)
    {
        failed++;
        Console.WriteLine($"FAIL {test.Name}");
        Console.WriteLine(exception);
    }
}

if (failed > 0)
{
    Console.WriteLine($"{failed} UI test(s) failed.");
    return 1;
}

Console.WriteLine($"{tests.Length} UI test(s) passed.");
return 0;

internal static class MainWindowUiTests
{
    public static void ExposesMenuItems()
    {
        var window = new MainWindow();
        try
        {
            var contactMenuItem = Require<MenuItem>(window, "ContactMenuItem");
            var helpMenuItem = Require<MenuItem>(window, "HelpMenuItem");
            var contactListBox = Require<ListBox>(window, "ContactListBox");

            TestAssert.Equal("_Kontakt", contactMenuItem.Header?.ToString() ?? string.Empty);
            TestAssert.Equal("_Hilfe", helpMenuItem.Header?.ToString() ?? string.Empty);
            TestAssert.Equal(0, contactListBox.Items.Count);
        }
        finally
        {
            window.Close();
        }
    }

    public static void AddsContactsToContactList()
    {
        var window = new MainWindow();
        try
        {
            var contactListBox = Require<ListBox>(window, "ContactListBox");
            var contact = new ContactDetails(
                "Herr",
                "Dirk",
                "Sandhorst",
                "Entwickler",
                "01234",
                "dirk@example.com",
                "Bitte zurueckrufen");

            window.AddContact(contact);

            var expected = string.Join(
                "\n",
                "Anrede: Herr",
                "Vorname: Dirk",
                "Name: Sandhorst",
                "Position: Entwickler",
                "Telefonnummer: 01234",
                "E-Mail-Adresse: dirk@example.com",
                "Bemerkungen: Bitte zurueckrufen");

            TestAssert.Equal(1, contactListBox.Items.Count);
            TestAssert.Equal(expected, contactListBox.Items[0]?.ToString() ?? string.Empty);
        }
        finally
        {
            window.Close();
        }
    }

    private static T Require<T>(Control root, string name)
        where T : class
    {
        return root.FindName(name) as T
            ?? throw new InvalidOperationException($"UI-Element '{name}' wurde nicht gefunden.");
    }
}

internal static class ContactDialogUiTests
{
    public static void FormatsValuesEnteredIntoUiControls()
    {
        var dialog = new ContactDialog();
        try
        {
            SetText(dialog, "FirstNameTextBox", " Dirk ");
            SetText(dialog, "LastNameTextBox", " Sandhorst ");
            SetText(dialog, "PositionTextBox", " Entwickler ");
            SetText(dialog, "EmailTextBox", " dirk@example.com ");

            var expected = string.Join(
                "\n",
                "Vorname: Dirk",
                "Name: Sandhorst",
                "Position: Entwickler",
                "E-Mail-Adresse: dirk@example.com");

            TestAssert.Equal(expected, dialog.ContactText);
        }
        finally
        {
            dialog.Close();
        }
    }

    public static void OmitsEmptyUiFields()
    {
        var dialog = new ContactDialog();
        try
        {
            SetText(dialog, "FirstNameTextBox", "Anna");
            SetText(dialog, "LastNameTextBox", "");
            SetText(dialog, "PhoneTextBox", " ");

            TestAssert.Equal("Vorname: Anna", dialog.ContactText);
        }
        finally
        {
            dialog.Close();
        }
    }

    private static void SetText(ContactDialog dialog, string textBoxName, string text)
    {
        var textBox = dialog.FindName(textBoxName) as TextBox
            ?? throw new InvalidOperationException($"TextBox '{textBoxName}' wurde nicht gefunden.");
        textBox.Text = text;
    }
}

internal static class StaTestRunner
{
    public static void Run(Action test)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try
            {
                test();
            }
            catch (Exception exception)
            {
                failure = exception;
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();

        if (failure is not null)
        {
            throw failure;
        }
    }
}

internal static class TestAssert
{
    public static void Equal(int expected, int actual)
    {
        if (expected == actual)
        {
            return;
        }

        throw new InvalidOperationException($"Expected: {expected}, Actual: {actual}");
    }

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
