using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClanServer.Models
{
    public class JubeatScore
    {
        public int ID { get; set; }

        [Required]
        public JubeatProfile Profile { get; set; }
        public int ProfileID { get; set; }

        public long Timestamp { get; set; }

        public int MusicID { get; set; }

        [Range(0, 2)]
        public sbyte Seq { get; set; }

        public int Score { get; set; }

        public sbyte Clear { get; set; }

        public short NumPerfect { get; set; }

        public short NumGreat { get; set; }

        public short NumGood { get; set; }

        public short NumPoor { get; set; }

        public short NumMiss { get; set; }

        public bool IsHardMode { get; set; }

        public bool IsHazardMode { get; set; }

        [MinLength(30), MaxLength(30)]
        public byte[] Bar { get; set; }
    }
}
