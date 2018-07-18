using System;

namespace table
{
    public class Warranty1
    {
        public int ID { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNo { get; set; }

        public string ItemNo { get; set; }

        public int? LineNo { get; set; }

        public string Serial { get; set; }

        public string Description { get; set; }

        public int? Warranty { get; set; }

        public bool? Export { get; set; }

        public bool? ExportCrm { get; set; }

    }
}