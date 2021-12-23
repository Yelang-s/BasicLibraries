namespace BasicLibraries.VNAControl.Format
{
    public struct VNAConfigFormat
    {
        public int VNAID { get; set; }
        public string IP { get; set; }
        public VNAType Type { get; set; }
        public override string ToString()
        {
            return $"ID:{VNAID}\r\nIP:{IP}\r\nType:{Type}";
        }
    }
}
