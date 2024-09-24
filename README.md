# README

# Contacts
Telegram: [@egor4yt](https://t.me/egor4yt)

<!--Starting application-->
## Starting application

1. Go to the './FinanceHelper' directory


2. Execute command to prepare docker image
  
    ```docker build -t "finance-helper" -f ".\FinanceHelper.Api\Dockerfile" .```


3. Configure [environment](#environment-configuration)


4. Configure [docker-compose](#docker-compose-file-configuration)


5. Execute command

   ```docker-compose up -d```


6. If you used example of '.env' file, application will be available at http://localhost:5719/swagger or http://127.0.0.1:5719/swagger.

<!--Docker compose file-->
## Docker compose file configuration
Example of the docker compose file (must be located in './FinanceHelper' directory):
```yaml
version: '3.9'

services:
# Database service
  database:
    image: postgres:latest
    ports:
      - "${DB_PUBLIC_PORT}:5432"
    volumes:
      - /var/lib/postgresql/data/
    environment:
      - POSTGRES_DB=${DB_NAME}
      - POSTGRES_USER=${DB_USER}
      - POSTGRES_PASSWORD=${DB_PASSWORD}
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U ${DB_USER} -d ${DB_NAME}"]
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 5s
  
# Web API service
  webapi:
    image: finance-helper
    ports:
      - "${WEBAPI_PUBLIC_PORT}:8080"
    depends_on:
      database:
        condition: service_healthy
    environment:
      - ConnectionStrings:DatabaseConnection=host=database;port=5432;database=${DB_NAME};username=${DB_USER};password=${DB_PASSWORD}
      # Swagger
      - SwaggerDocOptions:Title=Finance-Helper API
      - SwaggerDocOptions:Description=Application to help people manage their finances
      - SwaggerDocOptions:Organization=Ermakov Egor
      - Email:Organization=egor4yt@gmail.com
      - CorsOrigins=${CORS_ORIGINS}
```

<!--Environment-->
## Environment configuration

Before you start application you need to create environment variables file.
Environment variables is variables which application use while working. Environment variables file must be located in the same directory with the docker compose file and have name '.env'.

### Description of all variables

| Variable           | Description                                   |
|:-------------------|:----------------------------------------------|
| DB_NAME            | Database name                                 |
| DB_USER            | Database default user name                    |
| DB_PASSWORD        | Database default user password                |
| DB_PUBLIC_PORT     | Database port to access from local network    |
| WEBAPI_PUBLIC_PORT | Application port to access from local network |
| CORS_ORIGINS       | Allowed origin for CORS requests              |

### Example of the '.env' file

```text
DB_NAME=finance-helper
DB_USER=admin
DB_PASSWORD=admin
DB_PUBLIC_PORT=5718
WEBAPI_PUBLIC_PORT=5719
CORS_ORIGINS=http://localhost:2000
```

