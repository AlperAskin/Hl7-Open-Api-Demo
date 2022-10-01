using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra.Mapping;
using Cassandra;

namespace Endava.Hl7.Fhir.Common.Contracts.Service { 

public class CassandraMappings : Mappings{

    
    public CassandraMappings()
    {
           
            // Define mappings in the constructor of your class
            // that inherits from Mappings
            For<Patient>()
           .TableName("Patient_Data")
           .PartitionKey(u => u.Id)
           .Column(u => u.Id, cm => cm.WithName("id"));

    }

}
}