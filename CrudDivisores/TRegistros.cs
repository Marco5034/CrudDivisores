using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDivisores
{
    [Table("registros")]

    public class TRegistros
    {
       
            [PrimaryKey]
            [AutoIncrement]
            [Column("id")]

            public int Id { get; set; }

            [Column("numero")]
            public int Numero { get; set; }

            [Column("divisores")]
            public string Divisores { get; set; }
        }
    }

