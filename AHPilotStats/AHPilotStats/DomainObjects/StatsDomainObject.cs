namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    public abstract class StatsDomainObject : DomainObject
    {
        [GridHelper("Tour")]
        abstract public string TourNumber { get; }
        
        [GridHelper("Details")]
        abstract public string TourIdentfier { get; }

        [GridHelper("Type")]
        abstract public string TourType { get; }

        [GridHelper("Kills")]
        abstract public int OverAllKills { get; }

        [GridHelper("Assists")]
        abstract public int OverAllAssists { get; }

        [GridHelper("Sorties")]
        abstract public int OverAllSorties { get; }

        [GridHelper("Landed")]
        abstract public int OverAllLanded { get; }

        [GridHelper("Bailed")]
        abstract public int OverAllBailed { get; }
       
        [GridHelper("Ditched")]
        abstract public int OverAllDitched { get; }
        
        [GridHelper("Captured")]
        abstract public int OverAllCaptured { get; }
        
        [GridHelper("Death")]
        abstract public int OverAllDeath { get; }
        
        [GridHelper(true)]
        abstract public int OverAllDeathPlus1 { get; }
        
        [GridHelper("Disco")]
        abstract public int OverAllDisco { get; }
        
        [GridHelper("Time")]
        abstract public string OverAllTime { get; }

        [GridHelper(true)]
        abstract public int OverAllTimeInSeconds { get; }

        [GridHelper(true)]
        abstract public double OverAllTimeInHours { get; }

        [GridHelper(true)]
        abstract public double HTCKillsPerDeath { get; }
    }
}
