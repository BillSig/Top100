using ExcelEditorLibrary.Abstractions;

namespace ExcelEditorLibrary.Services
{
    public class FilePathMatchResult
    {
        public int MatchedCount { get; set; }
        public int UnmatchedCount { get; set; }
        public List<string> UnmatchedDescriptions { get; } = new();
    }

    /// <summary>
    /// A plain, UI-agnostic class. 
    /// It scans a folder once, then matches each record against the cached file list, reporting progress per record.
    /// </summary>
    public class FilePathMatcher
    {
        /// <summary>
        /// Updates the FilePath of each item by searching for its tokens inside
        /// the filenames found in <paramref name="folderPath"/> (recursively).
        /// </summary>
        /// <param name="items">Records to update. Only items implementing IHasSearchableFilePath are considered.</param>
        /// <param name="folderPath">Root folder to search for matching files.</param>
        /// <param name="progress">Reports the number of items processed so far (1-based).</param>
        public FilePathMatchResult UpdateFilePaths(IReadOnlyList<IHasSearchableFilePath> items, string folderPath, IProgress<int> progress = null)
        {
            if (items == null) 
                throw new ArgumentNullException(nameof(items));

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");
            }

            // Cache all files in the folder and its subfolders to avoid repeated disk access.
            var candidateFiles = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                .Select(path => new { Path = path, FileName = Path.GetFileName(path) })
                .ToList();

            var result = new FilePathMatchResult();

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                // Get search tokens for the current item, filtering out any empty or whitespace tokens.
                var tokens = item.GetSearchTokens()
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .ToList();

                var match = tokens.Count > 0
                    ? candidateFiles.FirstOrDefault(f => tokens.All(t =>
                        f.FileName.Contains(t, StringComparison.OrdinalIgnoreCase)))
                    : null;

                if (match != null)
                {
                    item.FilePath = match.Path;
                    result.MatchedCount++;
                }
                else
                {
                    result.UnmatchedCount++;
                    result.UnmatchedDescriptions.Add(string.Join(" - ", tokens));
                }

                progress?.Report(i + 1);
            }

            return result;
        }
    }
}