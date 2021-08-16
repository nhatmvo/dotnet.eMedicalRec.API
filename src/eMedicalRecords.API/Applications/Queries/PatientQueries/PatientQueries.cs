using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace eMedicalRecords.API.Applications.Queries.PatientQueries
{
    public class PatientQueries : IPatientQueries
    {
        private readonly string _connectionString;

        public PatientQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString)
                ? connectionString
                : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<PatientDetails> GetPatientAsync(Guid patientId, Guid selectedElementId)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("selectedElementId", selectedElementId);
            dynamicParams.Add("patientId", patientId);
            var result = await connection.QueryAsync<dynamic>(@"
                SELECT p.id            AS patientid,
                       p.name          AS patientname,
                       p.date_of_birth AS dateofbirth,
                       p.phone_number  AS phonenumber,
                       p.gender        AS gender,
                       a.entrydate,
                       a.mainentryname,
                       a.mainentryvalue,
                       a.entryid
                FROM patient p
                         INNER JOIN document d ON p.id = d.patient_id
                         LEFT OUTER JOIN (SELECT de.document_id  AS documentid,
                                                  de.created_date AS entrydate,
                                                  de.id           AS entryid,
                                                  teb.name        AS mainentryname,
                                                  ded.values       AS mainentryvalue
                                           FROM document_entry de
                                                    INNER JOIN template t ON de.template_id = t.id
                                                    INNER JOIN template_element_base teb ON t.id = teb.template_id
                                                   LEFT OUTER JOIN document_entry_data ded
                                                                    ON teb.element_base_id = ded.element_id AND ded.entry_id = de.id
                                           WHERE teb.element_base_id = @selectedElementId) a ON d.id = a.documentid
                WHERE p.id = @patientId ORDER BY a.entrydate DESC;
                ", dynamicParams);

            return MapToPatientDetail(result);
        }

        public async Task<IEnumerable<PatientView>> GetPatientsAsync(string filter)
        {
            var availableFilters = new[] { "p.id::text", "p.patient_document_no", "p.identity_no", "p.name" };

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            foreach (var availableFilter in availableFilters)
            {
                var dynamicParameters = new DynamicParameters();
                var queryBuilder = new StringBuilder(
                    @"SELECT p.id AS patientid, p.name AS patientname, p.date_of_birth AS dateofbirth, p.phone_number AS phonenumber, p.gender as gender, d.id as documentid, 
                        count(de.id) AS totalentries, max(de.created_date) as latestexaminationdate
                    FROM patient p 
                    INNER JOIN document d ON p.id = d.patient_id
                    FULL OUTER JOIN document_entry de ON d.id = de.document_id 
                        GROUP BY (p.id, p.name, p.date_of_birth, p.phone_number, d.id) HAVING 1 = 1").Append(' ');

                queryBuilder.Append("AND " + availableFilter + " = @filter");
                dynamicParameters.Add("filter", filter);
                var result = await connection.QueryAsync<dynamic>(queryBuilder.ToString(), dynamicParameters);
                if (result.AsList().Count != 0)
                    return MapToPatientView(result);
            }
            return new List<PatientView>();
        }
        

        private PatientDetails MapToPatientDetail(dynamic result)
        {
            var patientDetail = new PatientDetails
            {
                Id = result[0].patientid,
                Name = result[0].patientname,
                DateOfBirth = result[0].dateofbirth,
                PhoneNumber = result[0].phonenumber,
                Gender = result[0].gender ? "male" : "female",
                PatientEntries = new List<PatientEntry>()
            };
            foreach (var row in result)
            {
                if (row.entryid == null) break;
                patientDetail.PatientEntries.Add(new PatientEntry
                {
                    EntryDate = row.entrydate,
                    MainEntryName = row.mainentryname,
                    MainEntryValue = row.mainentryvalue,
                    EntryId = row.entryid
                });
            }

            return patientDetail;
        }
        

        private IEnumerable<PatientView> MapToPatientView(dynamic result)
        {
            var patients = new List<PatientView>();
            foreach (var item in result)
            {
                patients.Add(new PatientView
                {
                    Name = item.patientname,
                    PatientId = item.patientid,
                    DayOfBirth = item.dateofbirth,
                    PhoneNumber = item.phonenumber,
                    NumberOfEntries = item.totalentries,
                    DocumentId = item.documentid,
                    LatestExaminationDate = item.latestexaminationdate,
                    Gender = item.gender ? "male" : "female"
                });
            }

            return patients;
        }
    }
}