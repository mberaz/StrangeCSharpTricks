namespace StrangeCSharpTricks.DictionaryIsTheNewIf.FileStorage
{
    public interface IFileStorageProxy
    {
        public FileStorageTypes StorageType();
        public string ReadFile(string path);
    }

    public class LocalDriveProxy : IFileStorageProxy
    {
        public string ReadFile(string path)
        {
            //read from local drive
            return null;
        }

        public FileStorageTypes StorageType()
        {
            return FileStorageTypes.LocalDrive;
        }
    }

    public class AzureProxy : IFileStorageProxy
    {
        public string ReadFile(string path)
        {
            //read from Azure blob storage
            return null;
        }

        public FileStorageTypes StorageType()
        {
            return FileStorageTypes.AzureBlobStorage;
        }
    }

    public class AwsProxy : IFileStorageProxy
    {
        public string ReadFile(string path)
        {
            //read from Aws S3 storage
            return null; ;
        }

        public FileStorageTypes StorageType()
        {
            return FileStorageTypes.AmazonS3;
        }
    }
}
