﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HumansVsZombies_Backend.Models
{
    public class Kill
    {
        //PK
        public int KillId { get; set; }
        [Required]
        public DateTime TimeOfDeath { get; set; }
        public string Story { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        //Relationship one-to-many
        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }
        [Required]
        public int KillerId { get; set; }
        public Player Killer { get; set; }
        [Required]
        public int VictimId { get; set; }
        public Player Victim { get; set; }
    }
}
