using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AIAgentHarryDemo
{
    internal static class ContactStore
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        private static readonly string ContactsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AIAgentHarryDemo",
            "contacts.json");

        internal static IReadOnlyCollection<Contact> Load()
        {
            if (!File.Exists(ContactsFilePath))
            {
                return Array.Empty<Contact>();
            }

            try
            {
                using var stream = File.OpenRead(ContactsFilePath);
                var contacts = JsonSerializer.Deserialize<List<Contact>>(stream, JsonOptions);
                return contacts is null ? Array.Empty<Contact>() : contacts;
            }
            catch (JsonException)
            {
                return Array.Empty<Contact>();
            }
            catch (IOException)
            {
                return Array.Empty<Contact>();
            }
            catch (UnauthorizedAccessException)
            {
                return Array.Empty<Contact>();
            }
        }

        internal static void Save(IEnumerable<Contact> contacts)
        {
            var directoryPath = Path.GetDirectoryName(ContactsFilePath);
            if (!string.IsNullOrEmpty(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using var stream = File.Create(ContactsFilePath);
            JsonSerializer.Serialize(stream, contacts, JsonOptions);
        }
    }
}
