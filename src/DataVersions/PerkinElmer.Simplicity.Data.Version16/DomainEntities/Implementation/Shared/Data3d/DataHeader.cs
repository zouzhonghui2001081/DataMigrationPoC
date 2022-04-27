namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d
{
    public class DataHeader {
        public int Rows { get; set; } //Number of Spectra
        public int Cols { get; set; } //Number of Chromatograms
        public int Stride { get; set; } //Length of one chromatogram in bytes
        public int ItemSize { get; set; } //Bytes in one intensity point
        public int DataOffset { get; set; } //Offset of the Start of the first real (not auxiliary) spectrum in data 
    }
}