namespace ExcelEditorLibrary.Abstractions
{
    /// <summary>
    /// Implemented by models that can be matched against files on disk
    /// by searching for tokens (e.g. band name, song title) inside filenames,
    /// and that expose a FilePath to be updated with the matching result.
    /// </summary>
    public interface IHasSearchableFilePath
    {
        /// <summary>
        /// Gets the tokens that must all be present (case-insensitive) inside
        /// a candidate filename for it to be considered a match.
        /// </summary>
        IEnumerable<string> GetSearchTokens();

        /// <summary>
        /// Full path of the matched file. Set by the matching process.
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Called by the matcher after FilePath has been set to a matching file.
        /// Lets each model apply its own side effects (e.g. flag the item as locked).
        /// </summary>
        void OnFilePathMatched();
    }
}