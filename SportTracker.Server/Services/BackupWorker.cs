using Amazon.S3;
using Amazon.S3.Model;

namespace SportTracker.Server.Services
{
    public class BackupWorkerService : BackgroundService
    {
        private readonly AmazonS3Client amazonS3Client = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await OnTimerFiredAsync(stoppingToken);

            var countdown = SecondsTilMidnight();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (countdown <= 0)
                {
                    try
                    {
                        await OnTimerFiredAsync(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        // todo better way to log backups
                        Console.WriteLine("Backup worker failed!");
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        countdown = SecondsTilMidnight();
                    }
                }
                await Task.Delay(countdown, stoppingToken);
            }
        }

        private static int SecondsTilMidnight()
        {
            return (int)(DateTime.Today.AddDays(1) - DateTime.Now).TotalSeconds;
        }

        private async Task<bool> OnTimerFiredAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Database backup started");

            var bucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME");
            var objectName = "SportTrackerServer.sqlite";
            var file = $"../DB/{objectName}";
            var backupFile = $"{file}.bak";

            File.Copy(file, $"{file}.bak", true);

            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = $"DB_backup/{objectName}",
                FilePath = backupFile,
            };

            var response = await amazonS3Client.PutObjectAsync(request, stoppingToken);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully uploaded {objectName} to {bucketName}.");
                return true;
            }
            else
            {
                Console.WriteLine($"Could not upload {objectName} to {bucketName}.");
                return false;
            }
        }
    }
}
