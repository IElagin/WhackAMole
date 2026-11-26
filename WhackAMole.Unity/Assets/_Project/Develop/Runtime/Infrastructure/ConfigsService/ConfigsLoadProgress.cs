namespace _Project.Develop.Runtime.Infrastructure.ConfigsService
{
    public readonly struct ConfigsLoadProgress
    {
        public readonly int Loaded;
        public readonly int Total;

        public ConfigsLoadProgress(int loaded, int total)
        {
            Loaded = loaded;
            Total = total;
        }

        public float Percent => Total > 0 ? (float)Loaded / Total : 0f;
    }
}
