using System.Collections.Generic;

namespace APISeed.DataLayer.Schema
{
    internal static class Scripts
    {
        internal static readonly List<string> V1 = new List<string>
        {
            @"
CREATE TABLE            _SchemaVersion
(
    Version INT PRIMARY KEY
);

INSERT INTO         _SchemaVersion
VALUES              (1);
"
        };

        internal static readonly List<string> V2 = new List<string>
        {
            @"

INSERT INTO         AspNetRoles
VALUES              (NEWID(), 'Administrator');

UPDATE              _SchemaVersion
SET                 Version = 2;
"
        };

        internal static readonly IReadOnlyList<IReadOnlyList<string>> UpdateScripts = new List<List<string>>()
        {
            V1,
            V2
        };
    }
}
