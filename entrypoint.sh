#!/bin/bash

# SQL Server'ın başlamasını bekle
for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "finman123*" -Q "SELECT 1" > /dev/null 2>&1
    if [ $? -eq 0 ]
    then
        echo "SQL Server is ready"
        break
    else
        echo "Waiting for SQL Server to start"
        sleep 1
    fi
done

# init.sql script'ini çalıştır
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "finman123*" -i /scripts/init.sql

# SQL Server'ı başlat (Ön planda çalışmaya devam et)
exec /opt/mssql/bin/sqlservr
