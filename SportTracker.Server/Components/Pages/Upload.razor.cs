using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Components;
using SportTracker.Server.Models.SportEvents;
using System.Globalization;

namespace SportTracker.Server.Components.Pages
{
    public class SportEventUploadMap : ClassMap<SportEvent>
    {
        SportEventUploadMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.SportEventId).Ignore();
        }
    }

    public partial class Upload
    {
        [Inject]
        private ISportEventRepository SportEventRepository { get; set; } = null!;
        
        private string? errorMessage;
        private int? submitted = null;

        [SupplyParameterFromForm]
        public FileUpload UploadInput { get; set; } = new();

        void UploadFile()
        {
            submitted = 0;

            try
            {
                using (var reader = new StreamReader(UploadInput.File.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csv.Context.RegisterClassMap<SportEventUploadMap>();

                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        var record = csv.GetRecord<SportEvent>();

                        SportEventRepository.InsertEvent(record);

                        submitted++;
                    }

                    Console.WriteLine($"success. {submitted} items added");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
