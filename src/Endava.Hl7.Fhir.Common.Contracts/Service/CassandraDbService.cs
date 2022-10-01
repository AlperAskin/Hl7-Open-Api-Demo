using Cassandra;
using Cassandra.Mapping;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Sprache;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Endava.Hl7.Fhir.Common.Contracts.CassandraDbService
{
    public class CassandraDbService
        {
        private const string USERNAME = "cassandra";
        private const string PASSWORD = "cassandra";
        private const string CASSANDRACONTACTPOINT = "127.0.0.1";  // DnsName  
        private static int CASSANDRAPORT = 9042;
        public IMapper Mapper;

        public CassandraDbService()
        {
            // Connect to cassandra cluster  (Cassandra API on Azure Cosmos DB supports only TLSv1.2)
            // options.SetHostNameResolver((ipAddress) => CASSANDRACONTACTPOINT);

           
            Cluster cluster = Cluster
                .Builder()
                .WithCredentials(USERNAME, PASSWORD)
                .WithPort(CASSANDRAPORT)
                .AddContactPoint(CASSANDRACONTACTPOINT)
                .Build()
            ;

            ISession session = cluster.Connect();
            session = cluster.Connect("test");
            Mapper = new Mapper(session);

            //  IEnumerable<Patient> patient = mapper.Fetch<Patient>("SELECT userid, name FROM users");
            //async Task<List<Patient>> GetFirst(IEnumerable<Patient> enumerable)
            //{
            // return await mapper.FirstOrDefault<Patient>("Select * from user where user_id = ?", 1);

        }

         
        
    }
    }

    

