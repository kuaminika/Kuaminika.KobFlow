namespace Kuaniminka.KobFlow.ToolBox
{
    public struct KWriteResult
    {
        public int AffectedRowCount { get; internal set; }
        public long LastInsertedId { get; internal set; }
    }
}