namespace StrangeCSharpTricks.TaskListTimeOut
{
    public static class Utils
    {
        public static async Task BatchExecuteAsync<TSource>(
            IEnumerable<TSource> sources,
            Func<IEnumerable<TSource>, CancellationToken, IEnumerable<Task>> action,
            CancellationToken cancellationToken,
            int batchSize)
        {
            foreach (var chunk in sources.Chunk(batchSize))
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var tasks = action(chunk, cancellationToken);
                await Task.WhenAll(tasks);
            }
        }
    }
}