using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGL_HW6
{
    public class Queries
    {
        public const string CheckIfTableExistsQuery = $@"
IF (EXISTS (SELECT * 
                    FROM INFORMATION_SCHEMA.TABLES
                    WHERE TABLE_SCHEMA = 'dbo'
                    AND TABLE_NAME = @TableName))
SELECT 1
ELSE SELECT 0
";

        public const string CreatePuppiesTableQuery = $@"
CREATE TABLE [dbo].[Puppies](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [Name] [nvarchar](300) NOT NULL,
    [BirthDate] [datetime] NOT NULL,
    [LengthInCm] [float] NOT NULL,
    [HeightInCm] [float] NOT NULL,
     CONSTRAINT [PM_Puppies] PRIMARY KEY CLUSTERED
    (
        [Id] ASC 
    ) ON [PRIMARY]
)";

        public const string InsertPuppiesDataQuery = $@"
INSERT INTO [dbo].[Puppies]
(Name, BirthDate, LengthInCm, HeightInCm)
VALUES (@Name, @BirthDate, @LengthInCm, @HeightInCm)
";

        public const string DeletePupiesTableQuery = $@"DROP TABLE [dbo].[Puppies]"; 
        public const string CountPuppiesTableQuery = $@"
SELECT COUNT(1)
FROM [dbo].[Puppies]
";
    }
}
