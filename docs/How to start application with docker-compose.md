# How to start application with local database and docker-compose

1. Go to the './src' directory with ``cd ./src``

2. Execute command to prepare docker image ``docker build -t "finance-helper" -f ".\FinanceHelper.Api\Dockerfile" .``

3. Create your version or copy example of the [.env](Docker%20examples%2F.env) in ./src directory

4. Create your version or copy example of the [docker-compose.yaml](Docker%20examples%2Fdocker-compose.yaml) in ./src directory

5. Execute command to start application ``docker compose up -d``

6. If you used example of '.env' and 'docker-compose.yaml' files, application will be available at http://localhost:5719/swagger or http://127.0.0.1:5719/swagger.

<!--Environment-->
## Environment configuration

Before you start application you need to create environment variables file.

Environment variables is variables which application using while working. Environment variables file must be located in the same directory with the docker compose file and have name '.env'.

### Description of all variables

| Variable           | Description                                   |
|:-------------------|:----------------------------------------------|
| DB_NAME            | Database name                                 |
| DB_USER            | Database default user name                    |
| DB_PASSWORD        | Database default user password                |
| DB_HOST            | Database default host or container name       |
| DB_PUBLIC_PORT     | Database port to access from local network    |
| WEBAPI_PUBLIC_PORT | Application port to access from local network |
| CORS_ORIGINS       | Allowed origin for CORS requests              |
