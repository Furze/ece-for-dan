# CLI
CLI tool to assist in creating and applying DB migrations for a Marten document store backed by a PostgreSQL database.

## Developer Workflow
1. **Run the Integration tests first!**. This starts the postgres docker container you need to make the patch file.
2. To create migration file, run the CLI with the `patch` argument passing the patch name and directory to write the migration file to.
    eg:
    `dotnet CLI.dll patch -pn AddCustomersDocument -md ./migrations` - optionally pass a connection string with the `-cs` flag

This will use the Marten CLI tool to create the correctly numbered and dated migrations script ('V1_20200620__AddCustomersDocument.sql') and put it in the ./migrations directory - check this in. 
It will also create the same named file (with 'drop_' prefix) in the ./drop/ sub-folder which contains a drop (down) migration file. Drop files will never be automatically applied. Manual intervention will be required.

## Upgrading an Existing DB
To patch an existing DB, run
`dotnet CLI.dll migrate -md ./migrations` - optionally pass a connection string with the `-cs` flag

This command will use Evolve to perform the patching.
https://evolve-db.netlify.app/


## Usage
 CLI:
   ECE API Migrations
 
 Usage:
   CLI [options] [command]
 
 Options:
   --version    Display version information
 
 Commands:  
 **apply**:                   Use Marten to apply all outstanding changes to the database based on the current Marten configuration  
 **assert**:                  Use Marten to assert that the existing database matches the current Marten configuration  
  **patch**:                  Use Marten to evaluate the current configuration against the database and write a patch and drop file if there are any differences  
 **erase**:                   Use Evolve to erase the database schema(s) if Evolve has created it or has found it empty  
 **info**:                    Use Evolve to display the details and status information about all the migrations  
 **migrate**:                 Use Evolve to apply the SQL migration file(s)  
 **repair**:                  Use Evolve to Correct checksums of already applied migrations, with the ones from actual migration scripts.  
 **migrate-reference-data**:  Use EF Core to apply reference data migrations
 **seed**:                    Seed reference data
 
 
 ## Patching JSONB Example
 
 ``` SQL
UPDATE public.mt_doc_taskmodel
 SET
     data = jsonb_set(data, '{Status}', '"PeerReview"', false),
     status = 'PeerReview'
 where status = 'InPeerReview';
```