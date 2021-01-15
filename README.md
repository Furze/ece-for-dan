# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)

# Connect to Azure instance of PostgreSQL

There are a few steps to this and there are some things you will need to do prior.

#### Pre Reqs (only required for non-ministry laptop) ####
- Log onto [Azure Portal](https://portal.azure.com)
- Go to the DevTest postgres server eg. **mataersdevtestpsqlserver**
- Navigate to Connection Security page
    - Add your client ip address in as a firewall rule (You may need to be on Green network for this to work within the ministry)
    - Don't forget to save it!!

### Connect to DB
- In pgAdmin create a new Server (or whatever equivalent in your preferred tool)
- General Tab - specify whatever name you want to be displayed
- Connection Tab -
  - Host name/address: enter the ***Server name*** you got from Azure Portal
      - e.g. for devtest this would be ***mataersdevtestpsqlserver.postgres.database.azure.com***
  - Port: keep as **5432**
  - Username: enter **AL PSQL ERS [DEVTEST | UATPREPROD] [READER | ADMIN]@{servername}**
      - e.g. for DEVTEST with read only access this would be **AL PSQL ERS DEVTEST READER@mataersdevtestpsqlserver**
      - ***and yes there are spaces in the AD group name..... don't ask!***
  - Password: see below on obtaining an azure session token

  Save and connect...

#### Obtaining an Azure session token
- Install the Azure CLI
- in a command window run the following commands to gain a session token. This is valid for 1 hour

    - `az login`
    - `az account get-access-token --resource-type oss-rdbms` and copy the value of "accessToken"


#### Notes on Connecting with PGAdmin
Most tools work well with this connection method.
It _is_ possible to connect using PgAdmin but you need a little trickery.
When adding the server the password field is restricted to 2000 characters.

Unfortunately the session token is over 5000. Once the Create Server screen is showing
- Select the _connection_ tab
- right click on the **Password** field and select _Inspect_.
- edit the `maxlength="2000"` to `maxlenghth="6000"` and hit enter

Now you are good to copy your session token in the password field.
Luckily, now that your server is created, a different pop up is used to prompt you to reenter your password when it fails.
This password text field does not need to be modified to hold the session token.
