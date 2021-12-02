using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public class Speaker
    {

        [Key]
        public int SpeakerId { get; set; }

        public String Name { get; set; }
        public String Topic { get; set; }


        public int SacramentPlannerId { get; set; }


        /*
         CREATE TABLE [dbo].[Speaker] (
    [SpeakerId]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (MAX) NULL,
    [Topic]              NVARCHAR (MAX) NULL,
    [SacramentPlannerId] INT            NULL,
    CONSTRAINT [PK_Speaker] PRIMARY KEY CLUSTERED ([SpeakerId] ASC),
    CONSTRAINT [FK_Speaker_SacramentPlanner_SacramentPlannerId] FOREIGN KEY ([SacramentPlannerId]) REFERENCES [dbo].[SacramentPlanner] ([SacramentPlannerId])
);

*/


    }
}
