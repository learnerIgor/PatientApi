using Newtonsoft.Json;
using PatientConsoleApp;
using PatientConsoleApp.Enums;
using System.Text;
using System.Net;


var patients = new List<Patient>();

for (int i = 0; i < 100; i++)
{
    patients.Add(GenerateRandomPatient());
}

await AddPatientAsync(patients);

patients.Clear();

Console.WriteLine("Все пациенты были успешно добавлены");
Console.WriteLine("Нажмите Enter, чтобы выйти...");
Console.ReadLine();

// Генерация пациента
static Patient GenerateRandomPatient()
{
    var random = new Random();
    var genderValues = Enum.GetValues(typeof(Gender));
    var gender = (Gender)genderValues.GetValue(random.Next(genderValues.Length)); // Случайный выбор пола

    return new Patient
    {
        Id = Guid.NewGuid(),
        Gender = gender,
        BirthDate = DateTime.Now.AddYears(-random.Next(1, 100)),
        Active = true,
        Name = new Name
        {
            Id = Guid.NewGuid(),
            Use = $"Patient {random.Next(1, 1000)}",
            Family = $"Family{random.Next(1, 100)}",
            GivenNames = new List<GivenName>
            {
                new() {Id = Guid.NewGuid(), Value = $"GivenName{random.Next(1, 100)}" }
            }
        }
    };
}

async Task AddPatientAsync(IEnumerable<Patient> patients)
{
    using var semaphore = new SemaphoreSlim(10);
    var tasks = patients.Select(async patient =>
    {
        await semaphore.WaitAsync();
        try
        {
            await SendPatientToDbAsync(patient);
        }
        finally
        {
            semaphore.Release();
        }
    });

    await Task.WhenAll(tasks);
}

async Task SendPatientToDbAsync(Patient patient)
{
    using var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("http://localhost:8080/Patient");
    httpClient.Timeout = new TimeSpan(0, 0, 50);
    httpClient.DefaultRequestHeaders.ConnectionClose = true;

    var request = new HttpRequestMessage(HttpMethod.Post, "");
    var json = JsonConvert.SerializeObject(patient);
    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await httpClient.SendAsync(request);

    if (response.StatusCode == HttpStatusCode.Created)
    {
        Console.WriteLine($"Patient {patient.Name.Use} added successfully.");
    }
    else
    {
        Console.WriteLine($"Failed to add patient {patient.Name.Use}: {response.StatusCode}");
    }
}
