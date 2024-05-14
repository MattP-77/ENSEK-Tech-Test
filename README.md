The TestHarness project is used to import the Test_Accounts. The file has already been imported so the records exist in the backup provided.

There are hard coded connection strings within the following classes:

CustomerSqlDataStore
MeterReadingSqlDataStore

EnsekConnectionString = "Data Source=[REPLACEINSTANCEHERE];Initial Catalog=ENSEK;Persist Security Info=True;Connect Timeout=6000000;MultipleActiveResultSets=true; User ID=DevelopmentUser; Password=DevelopmentUser;TrustServerCertificate=True"

Please ensure that your local sql instance is added here before attempting to run the solution.

Currently meter reading entries are limited to only accept values that are 5 in length as per the format NNNNN. For those entries that are < 5, perhaps apply 0 to the left to pad it out? wasn't sure if this would be acceptable so currently marking as invalid before persisting.

Swagger UI being used to interact with the API, this hasn't been published to Azure at this time.
