sleep 10s # Espera iniciar
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P SqlServer2022! -d master -i /tmp/create-database.sql
sleep 5s # Espera criar o banco
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P SqlServer2022! -d master -i /tmp/init.sql