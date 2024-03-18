namespace uploader
{
    public interface IView
    {
        public string speed { get; set; }
        public float progress { get; set; }
        void speedCalculate(long bytes);
    }
}
